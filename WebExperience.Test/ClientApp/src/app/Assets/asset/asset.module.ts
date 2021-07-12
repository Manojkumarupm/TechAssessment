import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AssetListComponent } from '../asset-list/asset-list.component';
import { AssetEditComponent } from '../asset-edit/asset-edit.component';
import { AssetDetailComponent } from '../asset-detail/asset-detail.component';
import { ReactiveFormsModule } from '@angular/forms';
 

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule, 
    RouterModule.forChild([
    {path:'Assets',
  component: AssetListComponent},
  {path:'Assets/:id',
  component: AssetDetailComponent},
  { path: 'Assets/:id/edit',
component: AssetEditComponent,
}
    ])
  ]
})
export class AssetModule { }
