using Core.Entities.PatientAggregate;

namespace API.Controllers;

/// <summary>
/// The purpose of this object is to help map the API body responses as string formats before
/// passing them to other parts of the project. In all cases, a discussion should be had
/// regarding the nullability of the properties and if that should be allowed or not. In this
/// example, I am not allowing nulls and setting default values to brevity.
/// </summary>
public class PatientItemRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
}
