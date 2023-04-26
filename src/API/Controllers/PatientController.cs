using Ardalis.GuardClauses;
using Core.Entities.PatientAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.ExceptionServices;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IAppLogger<Patient> _logger;

    public PatientController(IPatientService patientService, IAppLogger<Patient> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    // GET: Patient
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        // try-catch blocks are a necessary element in any practice, however they warrant a team discussion on
        // basic usage and where to place them within the various level of code. Ideally your POCOs do not require
        // a try-catch block and you can create them at the highest level while allowing information to bubble up.
        // Furthermore, artifacts within the code are crucial for monitoring, so using try-catch, attributes,
        // logging, and other tools should be discussed at the team level to handle appropriately
        try
        {
            _logger.Information("API.Controllers.PatientController: Get()");
            var patients = await _patientService.GetAllAsync();

            // Here I am using a Response class to manipulate the data from the server as to make it more readable and less cryptic with various numbers instead of strings
            // Again, this is another idea that should be discussed with the dev/product team on how to handle RESTful responses and their data. I lean towards making
            // things readable by a human, but that isn't always required
            var PatientItemResponse = new List<PatientItemResponse>();

            foreach (var patient in patients) 
            {
                PatientItemResponse.Add(new PatientItemResponse(patient.Id, patient.FirstName, patient.LastName, patient.DateOfBirth, GenderStringFormatter(patient.Gender)));
            }

            return Ok(PatientItemResponse);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Get() - Failed: {ex.Message}");
        }
        return BadRequest("Get() - Failed");
    }

    // GET: Patient/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        try
        {
            _logger.Information("API.Controllers.PatientController: Get(int id)");
            var patient = await _patientService.GetByIdAsync(id);

            return Ok(new PatientItemResponse(patient.Id, patient.FirstName, patient.LastName, patient.DateOfBirth, GenderStringFormatter(patient.Gender)));
        } 
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Get(int id) - Failed: {ex.Message}");
        }
        return BadRequest("Get(int id) - Failed");
    }

    // PUT: Patient/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, PatientItemRequest patientItemRequest)
    {
        try
        {
            // guarding against empty strings/inputs
            Guard.Against.NullOrEmpty(patientItemRequest.FirstName, nameof(patientItemRequest.FirstName));
            Guard.Against.NullOrEmpty(patientItemRequest.LastName, nameof(patientItemRequest.LastName));
            Guard.Against.NullOrEmpty(patientItemRequest.Gender, nameof(patientItemRequest.Gender));

            _logger.Information("API.Controllers.PatientController: Put(int id, PatientItemRequest patientItemRequest)");
            var existingItem = await _patientService.GetByIdAsync(id);

            if (existingItem == null)
            {
                _logger.Warning("API.Controllers.PatientController: Put, existing item was not found, put cancelled");
                return NotFound();
            }

            // At this part of the code could be a place to do shallow/deep cloning of the patient object to compare before and
            // after property values. This is helpful if you want to minimize SQL updates based on changed values only or if
            // you wish to do a data transfer operation of some type. For this example though, I am just going to use the
            // entity method to update the records since it has private sets.

            existingItem.UpdateRecord(id, patientItemRequest.FirstName, patientItemRequest.LastName, patientItemRequest.DateOfBirth, new Patient().ConvertGenderString(patientItemRequest.Gender));


            _logger.Information("API.Controllers.PatientController: Put(int id, PatientItemRequest patientItemRequest)");

            await _patientService.UpdatePatientAsync(existingItem);

            return NoContent();
        }
        // Another example of using built-in exception handling based on the specific operations. There is a catch with this
        // though, and that is this exception implies knowledge of our infrastrucutre implementation. With dependency injection,
        // you are not supposed to be concerned with the implementation of an interface at this layer, so this exception assumes
        // that it is supported through the infrastructure implementation. If we want to get very consistent, this would be
        // handled either in the repo layer, or an implementation specific to this project.
        catch (DbUpdateConcurrencyException)
        {
            if (!PatientExists(id).Result)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Put(int id, PatientItemRequest patientItemRequest) - Failed: {ex.Message}");
        }
        return BadRequest("Put(int id, PatientItemRequest patientItemRequest) - Failed");
    }

    // POST: Patient
    [HttpPost]
    public async Task<ActionResult> Post(PatientItemRequest patientItemRequest)
    {
        try
        {
            _logger.Information("API.Controllers.PatientController: Post(PatientItemRequest patientItemRequest)");

            var patient = await _patientService.CreatePatientAsync
            (
                new Patient
                (
                    patientItemRequest.FirstName,
                    patientItemRequest.LastName,
                    patientItemRequest.DateOfBirth,
                    patientItemRequest.Gender
                )
            );

            var PatientItemResponse = new PatientItemResponse(patient.Id, patient.FirstName, patient.LastName, patient.DateOfBirth, GenderStringFormatter(patient.Gender));

            return Ok(PatientItemResponse);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Post(string firstName, string lastName, DateTime dateOfBirth, string gender) - Failed: {ex.Message}");
        }
        return BadRequest("Post(Patient patient) - Failed");
    }


    // POST: Batch insert of patients
    [HttpPost("Batch")]
    public async Task<ActionResult> Batch([FromBody]IEnumerable<PatientItemRequest> patientItemRequests)
    {
        try
        { 
            _logger.Information("API.Controllers.PatientController: Post(string firstName, string lastName, DateTime dateOfBirth, string gender)");

            var dto = new List<Patient>();

            foreach (var patientItemRequest in patientItemRequests)
            {
                dto.Add(
                    new Patient
                    (
                        patientItemRequest.FirstName,
                        patientItemRequest.LastName,
                        patientItemRequest.DateOfBirth,
                        patientItemRequest.Gender
                    )
                );
            }

            var patients = await _patientService.BatchPatientsAsync(dto);

            var PatientItemResponse = new List<PatientItemResponse>();

            foreach (var patient in patients)
            {
                PatientItemResponse.Add(new PatientItemResponse(patient.Id, patient.FirstName, patient.LastName, patient.DateOfBirth, GenderStringFormatter(patient.Gender)));
            }

            return Ok(PatientItemResponse);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Post(List<PatientItemRequest> - Failed: {ex.Message}");
        }
        return BadRequest("Post(Patient patient) - Failed");
    }

    // DELETE: Patient/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            _logger.Information("API.Controllers.PatientController: Delete(int id)");

            var patient = await _patientService.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            await _patientService.DeletePatientAsync(patient);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Delete(int id) - Failed: {ex.Message}");
        }
        return BadRequest("Delete(int id) - Failed");
    }

    private async Task<bool> PatientExists(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private string GenderStringFormatter(Gender gender)
    {
        var genderString = gender switch
        {
            Gender.Male => "M",
            Gender.Female => "F",
            Gender.NonBinary => "NB",
            _ => "UK",
        };
        return genderString;
    }
}
