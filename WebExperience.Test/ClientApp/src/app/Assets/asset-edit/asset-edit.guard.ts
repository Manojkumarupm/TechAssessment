import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AssetEditComponent } from './asset-edit.component';

@Injectable({
  providedIn: 'root'
})
export class AssetEditGuard implements CanDeactivate<AssetEditComponent> {
  canDeactivate(component: AssetEditComponent): Observable<boolean> | Promise<boolean> | boolean {
    if (component.AssetForm.dirty) {
      const FileName = component.AssetForm.get('FileName').value || 'New Product';
      return confirm(`Navigate away and lose all changes to ${FileName}?`);
    }
    return true;
  }
}
