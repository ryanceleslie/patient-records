export class FileUploadService {
    public importCsvFromFileUpload(csvText: string): Array<any> {
        
        const propertyNames = csvText.slice(0, csvText.indexOf('\n')).split(',');
        const patients = csvText.slice(csvText.indexOf('\n'), 1).split('\n');

        let patientArray: any[] = [];
        patients.forEach((patientData) => {
            let data = patientData.split(',');

            //TODO make this a patient object isntead of any
            //TODO add some validation as well
            let patient: any = new Object();

            for (let i=0; i < propertyNames.length; i++) {
                const propertyName: string = propertyNames[i];

                let propertyValue: any = data[i];

                patient[propertyName] = propertyValue;
            }

            patientArray.push(patient);
        });

        return patientArray;
    }
}