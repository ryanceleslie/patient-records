export class ConvertTextService {
    public async csvToJson(csvText: string){
        
        // this may cause some troubles depending on how the csv file was initially created. The \n
        // operand should work fine, but some programs add \r as well. Ideally there would be checks
        // in place or requirements of the csv file built to a standard.
        var lines = csvText.split("\r\n");
    
        var result: Array<any> = [];
    
        var headers = lines[0].split(",");

        // using a for-loop for this as we don't want to loop through each line, rather each line 
        // after the header so the loop starts at index 1, and we subtract 1 from the length so it
        // doesn't return an empty object literal since it'll be out-of-bounds of the length
        for (var i = 1; i < lines.length - 1; i++) {
            // Normally, I would prefer to use a strongly type class here, but in this specific
            // instance if we're to focus on the scope of this process, this oject will be a JSON
            // object based on a string, so in essence this class can be reused for other conversions 
            // in the program where the JSON object will become a different type.
            let obj: any = new Object();
            var currentValue = lines[i].split(",");

            for (var j = 0; j < headers.length; j++) {
                //TODO get this out of here
                switch (headers[j]) {
                    case "First Name":
                    case "firstName":
                        headers[j] = "firstName";
                        break;
                    case "Last Name":
                    case "lastName":
                        headers[j] = "lastName";
                        break;
                    case "Birthday":
                    case "dateOfBirth":
                        headers[j] = "dateOfBirth";
                        currentValue[j] = new Date(currentValue[j]).toJSON();
                        break;
                    case "Gender":
                    case "gender":
                        headers[j] = "gender";
                        break;
                    default:
                        let errorObj: any = new Object();
                        errorObj["Error"] = "Upload failed for reason: Bad header - '" + headers[j].toString() + "'";
                        return errorObj;
                }

                obj[headers[j]] = currentValue[j];
            }    
    
            result.push(obj);
        }

    
        return result; //return as JSON object
    }
}