using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public enum Gender
{
    Male,
    Female,
    NonBinary,
    Unknown
}

public class Patient : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}

