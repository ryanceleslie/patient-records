using Core.Entities.PatientAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // logging, and other tools are always helpful.
        try
        {
            _logger.Information("API.Controllers.PatientController: Get()");
            var patients = await _patientService.GetAllAsync();

            return Ok(patients);
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
            var result = await _patientService.GetByIdAsync(id);

            return Ok(result);
        } 
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Get(int id) - Failed: {ex.Message}");
        }
        return BadRequest("Get(int id) - Failed");
    }

    // PUT: Patient/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, Patient patient)
    {
        try
        {
            if (id != patient.Id)
            {
                _logger.Warning("API.Controllers.PatientController: Put(int id, Patient patient) - IDs don't match");

                return BadRequest("IDs don't match");
            }

            _logger.Information("API.Controllers.PatientController: Put(int id, Patient patient)");

            await _patientService.UpdatePatientAsync(patient);

            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PatientExists(id).Result)
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Put(int id, Patient patient) - Failed: {ex.Message}");
        }
        return BadRequest("Put(int id, Patient patient) - Failed");
    }

    // POST: Patient
    [HttpPost]
    public async Task<ActionResult> Post(string firstName, string lastName, DateTime dateOfBirth, string gender)
    {
        try
        {
            _logger.Information("API.Controllers.PatientController: Post(Patient patient)");

            var patient = await _patientService.CreatePatientAsync(firstName, lastName, dateOfBirth, gender);

            return CreatedAtAction("Get", new { id = patient.Id }, patient);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"API.Controllers.PatientController: Post(Patient patient) - Failed: {ex.Message}");
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
}
