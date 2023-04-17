using Core.Entities.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(string firstName, string lastName, DateTime dateOfBirth, string gender);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
    Task<Patient> GetByIdAsync(int id);
    Task<List<Patient>> GetAllAsync();
}
