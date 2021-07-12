import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs'; 
import { AssetInformation } from '../Assets/AssetInformation';
import { catchError, map, tap } from 'rxjs/operators';
import { summaryFileName } from '@angular/compiler/src/aot/util';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private url = ""; 
  private baseUrl = environment.AppURL; 
  private AllAssets = "Assets/getAssetsForPage"; 
  private OneAsset = "Assets/getAssetInformation"; 
  private AllAssetsCount = "Assets/getAllAssetsCount"; 
  constructor(public http: HttpClient) { }
  getAllAssets(pageNo, pageSize, sortOrder): Observable<any> {
    return this.http.get(this.baseUrl+this.AllAssets+'?pageNo=' + pageNo + '&pageSize=' + pageSize + '&sortOrder=' + sortOrder);
  }
  getOneAsset(AssetId): Observable<any> {
    if(AssetId === '0')
    {
      return of(this.InitializeAsset());
    }
    else
    {
      console.log('url: '+this.baseUrl+this.OneAsset+'?AssetId=' + AssetId);
      return this.http.get(this.baseUrl+this.OneAsset+'?AssetId=' + AssetId);
    }
    
  }
  getAllAssetsCount(): Observable<any> {
    return this.http.get(this.baseUrl+this.AllAssetsCount);
  } 
  createAsset(asset: AssetInformation):Observable<AssetInformation>{
    const reqHeader = new HttpHeaders()
    .set('Content-Type','application/json')
    .set('Accept','application/json');
    console.log('inside create asset');
    asset.Created_By='Admin';
    return this.http.post<AssetInformation>(this.baseUrl+"Assets/CreateAsset",JSON.stringify(asset),{
      headers: reqHeader,
    })
    .pipe(
      tap((data) => {
        console.log('Created Asset : '+JSON.stringify(data));
        if(data)
        {
          alert('Submitted new Asset into database');
        }
        else
        {
          alert('Not able to insert New Asset; Check with Admin');
        }
      }),
      catchError(this.handleError)
    );
  } 
  updateAsset(asset: AssetInformation):Observable<AssetInformation>{
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    asset.Created_By='Admin';
return this.http.put<AssetInformation>(this.baseUrl+"Assets/UpdateAsset",JSON.stringify(asset),{ headers }).pipe(
  tap((data) => {
    console.log('update Asset : '+ asset.FileName);
    if(data){
      alert(' Data updated in system');

    }
    else
    {
      alert(' Some error occured kindly try after sometime');
    }
  }),
  map(()=> asset),
  catchError(this.handleError));
  }
  handleError(err: any): Observable<never> {
    let errorMessage: string;
    if(err.error instanceof ErrorEvent)
    {
      errorMessage=`An Error occured : ${err.error.message}`;
    }
    else
    {
      errorMessage = `Backend returned code ${err.Status}: ${err.body.error}`;
    }
    return throwError(errorMessage);
  }
  deleteAsset(id: string): Observable<AssetInformation> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) }; 
    const url = this.baseUrl+"Assets/DeleteAsset?AssetId="+id;
    return this.http.delete<AssetInformation>(url,httpOptions)
      .pipe(
        tap(data => console.log('deleteAsset: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }
  private InitializeAsset():AssetInformation{
    return {
      AssetId: '0',
      FileName: null,
      MimeType: "Select MimeType",
      Created_By: null,
      Description: null,
      Email: null,
      Country: null,
      Created_On:null
  };
}
}
