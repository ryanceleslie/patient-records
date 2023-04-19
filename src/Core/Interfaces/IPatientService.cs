using Core.Entities.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IPatientService
{
    Task<Patient> CreatePatientAsync(Patient patient);

    // A minor point, I named this method "Batch" instead of "Bulk" specifically because this isn't a bulk of
    // data, rather it's a bulk of data AND processing particularly the changeing of the gender from string to
    // enum and it's processing a save.
    Task<IEnumerable<Patient>> BatchPatientsAsync(IEnumerable<Patient> patients);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
    Task<Patient> GetByIdAsync(int id);
    Task<IEnumerable<Patient>> GetAllAsync();
}
