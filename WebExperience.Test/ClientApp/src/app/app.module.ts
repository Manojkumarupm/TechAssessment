import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AssetListComponent } from './Assets/asset-list/asset-list.component';
import { PaginationService } from './service/pagination.service';
import { ApiService } from './service/api.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { AssetEditComponent } from './Assets/asset-edit/asset-edit.component';
import { AssetDetailComponent } from './Assets/asset-detail/asset-detail.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { AssetModule } from './Assets/asset/asset.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    AssetListComponent,
    AssetEditComponent,
    AssetDetailComponent,
    WelcomeComponent,
    
  ],
  imports: [
    BrowserModule,ReactiveFormsModule,
    HttpClientModule,NgxPaginationModule,
    RouterModule.forRoot([
      { path: 'welcome', component: WelcomeComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
    AssetModule
  ],
  providers: [ApiService, PaginationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
