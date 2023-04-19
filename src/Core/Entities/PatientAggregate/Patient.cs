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
    public string FirstName { get; private set; } = string.Empty; // As a practice, I tend to avoid nulls as much as possible, and since strings can be null, I am defining the value instead of making it a nullable type
    public string LastName { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }

    // This empty constructor is used for accessing the below internal method when converting
    // the gender option outside of the entity.
    public Patient()
    {
        
    }
    public Patient(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }

    // Created an overload method to allow additional instantiation of this object with different
    // type of parameters
    public Patient(string firstName, string lastName, DateTime dateOfBirth, string gender)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = ConvertGenderString(gender);
    }

    /// <summary>
    /// This is a bit of overkill, but I wanted to show an example where my entity expects an enum
    /// while the input is likely to be a string. This follows DDD principles where an entity in
    /// the core defines primitive and complex types that all implementations must follow. Normally,
    /// a string value for this property is sufficient, but it needs to follow a certain practice,
    /// so implementing this practice pushes the burden to implementation and should encourge 
    /// open/closed principles of SOLID.
    /// </summary>
    /// <param name="genderString"></param>
    /// <returns></returns>
    public Gender ConvertGenderString(string genderString)
    {
        Gender = genderString switch
        {
            "M" or "Male" => Gender.Male,
            "F" or "Female" => Gender.Female,
            "NB" or "NonBinary" or "Non-Binary" or "Non Binary" => Gender.NonBinary,
            // Using this as an example for an option of unknown or if empty/null to select
            // "unkown", this will prevent nulls in the data context
            "UK" or "Unkown" or _ => Gender.Unknown,
        };
        return Gender;
    }

    /// <summary>
    /// To maintain the consistent of the DDD patterns and best practices, entities have a private set,
    /// which means we cannot set these property values outside of this class in other areas of the code.
    /// And to avoid multiple forms of constructors, it's a better practice to have entity methods to
    /// handle these operations.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="gender"></param>
    public void UpdateRecord(int id, string firstName, string lastName, DateTime dateOfBirth, Gender gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}

