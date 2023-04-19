namespace API.Controllers;

/// <summary>
/// Similar to the Request, this object is meant to handle the response at the controller level to display
/// something a little different than the entity. In simplicity sake, it's a ViewModel, but I call it a
/// response to denote it pairs with the Request object
/// </summary>
public class PatientItemResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;

    public PatientItemResponse(int id, string firstName, string lastName, DateTime dateOfBirth, string gender)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}
