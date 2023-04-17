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
    public Task<Patient> CreatePatient(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
    {
        throw new NotImplementedException();
    }
    public Task<Patient> UpdatePatient(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> DeletePatient(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<List<Patient>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Patient> GetById(string id)
    {
        throw new NotImplementedException();
    }

}
