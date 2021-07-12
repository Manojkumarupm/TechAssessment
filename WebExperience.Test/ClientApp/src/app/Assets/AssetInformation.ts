import { DatePipe } from "@angular/common";

export class AssetInformation
{
AssetId: string;
FileName: string;
MimeType: string;
Created_By: string;
Email: string;
Country: string;
Description: string;
Created_On: DatePipe;
}
export class AssetInformationResolved{
    AssetInfo : AssetInformation;
    error? : any;
}