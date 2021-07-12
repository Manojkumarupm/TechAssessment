import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChildren,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
  FormControlName,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/service/api.service';
import { AssetInformation } from '../AssetInformation';
import { fromEvent, merge, Observable, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { countries } from 'src/app/data/country-data-store';
import { mimes } from 'src/app/data/mimes';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-asset-edit',
  templateUrl: './asset-edit.component.html',
  styleUrls: ['./asset-edit.component.scss'],
})
export class AssetEditComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];
  AssetForm: FormGroup;
  errorMessage: any;
  asset: any;
  pageTitle: string;
  fileToUpload: File | null = null;
  displayMessage: { [key: string]: string } = {};
  public mimes:any = mimes;
  private sub: Subscription;
  private validationMessages: { [key: string]: { [key: string]: string } };
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private api: ApiService,
  ) {}
  ngOnDestroy(): void {}
createFormGroup(): FormGroup{
  return new FormGroup({
    AssetId: new FormControl(),
      FileName: new FormControl(),
      MimeType: new FormControl(),
      Email: new FormControl(),
      Description: new FormControl(),
      Country: new FormControl(),
      Created_On: new FormControl(),
  });
}
  ngOnInit(): void {
    this.AssetForm = this.createFormGroup();
    this.AssetForm = this.fb.group({
      AssetId: [''],
      FileName: ['', [Validators.required]],
      MimeType: ['', Validators.required],
      Email: ['', [Validators.required,Validators.email]],
      Description: ['', Validators.required],
      Country: ['', Validators.required],
      Created_On: [''],
    });
    this.sub = this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      this.getAsset(id);
    });
  }
  ngAfterViewInit(): void {}
  getAsset(id: string): void {
    this.api.getOneAsset(id).subscribe({
      next: (asset: AssetInformation) => this.displayAsset(asset),
      error: (err) => (this.errorMessage = err),
    });
  }
  changeMimeType(e) {
    console.log('Mime : '+e.target.value)
    this.MimeType.setValue(e.target.value, {
      onlySelf: true
    })
  }
  get MimeType() {
    return this.AssetForm.get('MimeType');
  }
  displayAsset(Asset: AssetInformation): void {
    if (this.AssetForm) {
      this.AssetForm.reset();
    }
    this.asset = Asset;
    console.log(' data : ' + JSON.stringify(this.asset));
    if (this.asset.AssetId === '0') {
      this.pageTitle = 'Add Product';
    } else {
      this.pageTitle = `Edit Product: ${this.asset.FileName}`;
    }

    // Update the data on the form
    this.AssetForm.patchValue({
      AssetId: this.asset.AssetId,
      FileName: this.asset.FileName,
      MimeType: this.asset.MimeType,
      Email: this.asset.Email,
      Country: this.asset.Country,
      Description: this.asset.Description,
    });
    console.log(this.asset.MimeType);
      this.AssetForm.controls["MimeType"].setValue(this.asset.MimeType,{onlySelf:true});
  }
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.AssetForm.get('FileName').setValue(this.fileToUpload.name);
    this.AssetForm.get('MimeType').setValue(this.fileToUpload.type);
    console.log('File '+ this.fileToUpload.name + ' type: ' + this.fileToUpload.type);
}
uploadFileToActivity() {
  /*this.fileToUpload.postFile(this.fileToUpload).subscribe(data => {
    // do something, if upload success
    }, error => {
      console.log(error);
    });*/
}
  saveAsset(): void {
    if (this.AssetForm.valid) {
      if (this.AssetForm.dirty) {
        const p = { ...this.asset, ...this.AssetForm.value };
console.log('asset : ' + JSON.stringify(this.asset));
        if (p.AssetId === "0") {
          this.api.createAsset(p).subscribe({
            next: () => this.onSaveComplete(),
            error: (err) => (this.errorMessage = err),
          });
        } else {
          this.api.updateAsset(p).subscribe({
            next: () => this.onSaveComplete(),
            error: (err) => (this.errorMessage = err),
          });
        }
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = 'Please correct the validation errors.';
    }
  }
  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.AssetForm.reset();
    this.router.navigate(['/Assets']);
  }
  
}
