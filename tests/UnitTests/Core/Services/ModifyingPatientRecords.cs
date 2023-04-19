using Core.Entities.PatientAggregate;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.DataProtection.Repositories;
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
    private readonly Mock<IAppLogger<Patient>> _mockLogger = new();

    [Fact]
    public async Task ShouldAddPatient()
    {
        // I know there are better tests for inserts, but callbacks should be sufficient here
        var patients = new List<Patient>();

        _mockPatientRepository
            .Setup(p => p.AddAsync(It.IsAny<Patient>()))
            .Callback((Patient p) => patients.Add(p));
    }

    [Fact]
    public async Task ShouldUpdatePatient()
    {
        // Arrange
        _mockPatientRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>()));
        var patientService = new PatientService(_mockPatientRepository.Object, _mockLogger.Object);

        // Act
        await patientService.UpdatePatientAsync(It.IsAny<Patient>());

        // Assert
        _mockPatientRepository.Verify(v => v.UpdateAsync(It.IsAny<Patient>()), Times.Once);
    }

    [Fact]
    public async Task ShouldDeletePatient()
    {
        // Arrange
        _mockPatientRepository.Setup(p => p.GetByIdAsync(It.IsAny<int>()));
        var patientService = new PatientService(_mockPatientRepository.Object, _mockLogger.Object);

        // Act
        await patientService.DeletePatientAsync(It.IsAny<Patient>());

        // Assert
        _mockPatientRepository.Verify(v => v.DeleteAsync(It.IsAny<Patient>()), Times.Once);
    }
}
