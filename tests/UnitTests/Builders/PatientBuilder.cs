using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.PatientAggregate;

namespace UnitTests.Builders;

/// <summary>
/// This builder is a helper class for creating patient mock data for unit testing. Since the 
/// patient data contains primative types, normally I'd use Moq and IsAny<string> for this,
/// but I wanted to show an example of more complex entity properties.
/// </summary>
public class PatientBuilder
{
    private Patient _patient;

    public string TestFirstName => "John";
    public string TestLastname => "Cena";
    public DateTime TestDateOfBirth = new DateTime(1977, 4, 23);
    public Gender TestGender => Gender.Male;
    public string TestGenderString => "M";
    public Patient TestPatient { get; }

    public PatientBuilder()
    {
        TestPatient = new Patient(TestFirstName, TestLastname, TestDateOfBirth, TestGender);
        _patient = WithDefaultEntityValues();
    }

    // The following methods are not being used but shared as an example of various scaffolding
    // for unit testing. I generally prefering mocking of data, but these could also be used
    // to test permutations of setting entity values.
    public Patient Build()
    {
        return _patient;
    }

    public Patient WithDefaultEntityValues()
    {
        _patient = new Patient(TestFirstName, TestLastname, TestDateOfBirth, TestGender);
        return _patient;
    }

    public Patient WithEmptyValues()
    {
        _patient = new Patient("", "", DateTime.UtcNow, Gender.Unknown);
        return _patient;
    }
}
