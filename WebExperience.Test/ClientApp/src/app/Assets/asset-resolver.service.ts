import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ApiService } from '../service/api.service';
import { AssetInformationResolved } from './AssetInformation';

@Injectable({
  providedIn: 'root'
})
export class AssetResolverService implements Resolve<AssetInformationResolved> {

  constructor(private _assetService: ApiService) { }
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<AssetInformationResolved>  {
    const id = route.paramMap.get('id');
    console.log("inside resolver : "+id);
  
  return this._assetService.getOneAsset(id).pipe(
    map((AssetInfo) => ({ AssetInfo })),
    catchError((error) => {
      const message= `Retrieval error ${error}`;
      console.error(message);
     return of({ AssetInfo: null, error: message});
    })
  );
}
}
