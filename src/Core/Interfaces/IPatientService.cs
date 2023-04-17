using Core.Entities.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IPatientService
{
    Task<Patient> CreatePatient(string firstName, string lastName, DateTime dateOfBirth, string gender);
    Task<Patient> UpdatePatient(Patient patient);
    Task<Patient> DeletePatient(Patient patient);
    Task<Patient> GetById(int id);
    Task<List<Patient>> GetAll();
}
