using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using System.Reflection;

namespace Core.Entities.PatientAggregate;

public enum Gender
{
    Male,
    Female,
    NonBinary,
    Unknown
}

/// <summary>
/// I created this as a root entity to denote that while this programming exercise is simple,
/// this entity is likely to be an aggregate for a bounded context in the overall domain. This
/// will help future-proof it to a varying degree of feature growth and following single
/// responsibility principles of SOLID
/// </summary>
public class Patient : BaseEntity, IAggregateRoot
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

    /// <summary>
    /// This is a bit of overkill, but I wanted to show an example where my entity expect an enum
    /// while the input is likely to be a string. This follow DDD principles where an entity in
    /// the core defines primitive and complex types that all implementations must follow. Normally,
    /// a string value for this property is sufficient, but it needs to follow a certain practice,
    /// so implementing this practice through enums and conversation pushes the burden to implementation
    /// and should encourge open/closed principles of SOLID.
    /// </summary>
    /// <param name="genderString"></param>
    /// <returns></returns>
    public Gender ConvertGenderString(string genderString)
    {
        switch (genderString)
        {
            case "M":
                Gender = Gender.Male;
                break;

            case "F":
                Gender = Gender.Female;
                break;

            case "NB":
                Gender = Gender.NonBinary;
                break;

            // Using this as an example for an option of unknown or if empty/null to select
            // "unkown", this will prevent unknown nulls in the data context
            case "UK":
            default:
                Gender = Gender.Unknown;
                break;
        }

        return Gender;
    }
}

