//TODO fix decorators, they don't work
import { JsonObject, JsonProperty } from "json2typescript";

@JsonObject("Patient")
export class Patient {
    @JsonProperty("First Name")
    public firstName: string;
    
    @JsonProperty("Last Name")
    public lastName: string;
    
    @JsonProperty("Birthday")
    public dateOfBirth: Date;
    
    @JsonProperty("Gender")
    public gender: string;

    /**
     *
     */
    constructor(first: string, last: string, dob: Date, gender: string) {
        this.firstName = first;
        this.lastName = last;
        this.dateOfBirth = dob;
        this.gender = gender;
    }
}