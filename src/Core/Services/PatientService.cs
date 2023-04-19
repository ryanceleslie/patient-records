using Ardalis.GuardClauses;
using Core.Entities.PatientAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class PatientService : IPatientService
{
    // I want to call this out this proper name specifically. It's ok to give a generic name for
    // a dependency like this, but I am giving it a specific name since microservices are truly
    // independeny and as such their have individual repos that should be addressed with specific
    // names.
    private readonly IRepository<Patient> _patientRepository;
    private IAppLogger<Patient> _logger;

    public PatientService(IRepository<Patient> patientRepository, IAppLogger<Patient> logger)
    {
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        _logger.Information("Core.Services.PatientService: CreatePatientAsync()");

        // ensure that the strings are NOT null or empty, ideally this would be defined within the entity
        // but with primitive types and strings, it's ok to have here and check for empty strings.
        Guard.Against.NullOrEmpty(patient.FirstName, nameof(patient.FirstName));
        Guard.Against.NullOrEmpty(patient.LastName, nameof(patient.LastName));

        return await _patientRepository.AddAsync(patient);
    }

    public async Task<IEnumerable<Patient>> BatchPatientsAsync(IEnumerable<Patient> patients)
    {
        _logger.Information("Core.Services.PatientService: BatchPatientsAsync()");

        return await _patientRepository.AddRangeAsync(patients);

    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _logger.Information("Core.Services.PatientService: UpdatePatientAsync()");

        return await _patientRepository.UpdateAsync(patient);
    }

    public async Task DeletePatientAsync(Patient patient)
    {
        _logger.Information("Core.Services.PatientService: DeletePatientAsync()");

        await _patientRepository.DeleteAsync(patient);
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        _logger.Information("Core.Services.PatientService: GetByIdAsync()");

        return await _patientRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        _logger.Information("Core.Services.PatientService: GetAllAsync()");

        return await _patientRepository.GetAllAsync();
    }
}
