export class Patient {
    public firstName: string;
    public lastName: string;
    public dateOfBirth: Date;
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