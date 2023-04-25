export class PatientData {
  public async convertHeaders(data: string){
    
    data.replace("First Name", "firstName");
    data.replace("Last Name", "lastName");
    data.replace("Birthday", "dateOfBirth");
    data.replace("Gender", "gender");

    return data;
  }
}