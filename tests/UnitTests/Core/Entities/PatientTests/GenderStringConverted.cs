using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Core.Entities.PatientAggregate;
using Moq;

namespace UnitTests.Core.Entities.PatientTests;

public class GenderStringConverted
{
    [Fact]
    public void IsGenderStringConverted()
    {
        // Arrange
        var _patient = new PatientBuilder();
        var patientMock = new Mock<Patient>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Gender>());

        // Act
        var genderConverted = patientMock.Object.ConvertGenderString(_patient.TestGenderString);

        // Assert
        Assert.Equal(Gender.Male, genderConverted);
    }
}
