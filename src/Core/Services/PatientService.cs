﻿using Ardalis.GuardClauses;
using Core.Entities.PatientAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public PatientService(IRepository<Patient> patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient> CreatePatientAsync(string firstName, string lastName, DateTime dateOfBirth, string gender)
    {
        // ensure that the strings are NOT null or empty, ideally this would be defined within the entity
        // but with primitive types and strings, it's ok to have here and check for empty strings
        Guard.Against.NullOrEmpty(firstName, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        Guard.Against.NullOrEmpty(gender, nameof(gender));

        var convertedGender = new Patient().ConvertGenderString(gender);

        var patient = new Patient(firstName, lastName, dateOfBirth, convertedGender);

        return await _patientRepository.AddAsync(patient);
    }
    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        return await _patientRepository.UpdateAsync(patient);
    }

    public async Task DeletePatientAsync(Patient patient)
    {
        await _patientRepository.DeleteAsync(patient);
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        return await _patientRepository.GetByIdAsync(id);
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _patientRepository.GetAllAsync();
    }
}
