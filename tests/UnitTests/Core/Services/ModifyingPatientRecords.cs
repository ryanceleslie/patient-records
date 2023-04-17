using Core.Entities.PatientAggregate;
using Core.Interfaces;
using Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit.Sdk;

namespace UnitTests.Core.Services;

public class ModifyingPatientRecords
{
    private readonly PatientBuilder _patient = new PatientBuilder();
    private readonly Mock<IRepository<Patient>> _mockPatientRepository = new(); // using the repo type's name to be consistent with usage

    [Fact]
    public async Task ShouldAddPatient()
    {
        // Arrange
        _mockPatientRepository.Setup(p => p);
        var patientService = new PatientService(_mockPatientRepository.Object);

        // Act
        await patientService.CreatePatientAsync(_patient.TestFirstName, _patient.TestLastname, _patient.TestDateOfBirth, _patient.TestGenderString);

        // Assert
        _mockPatientRepository.Verify(x => x.GetByIdAsync(_patient.TestId), Times.Once);
    }

    [Fact]
    public async Task ShouldUpdatePatient()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task ShouldDeletePatient()
    {
        throw new NotImplementedException();
    }
}
