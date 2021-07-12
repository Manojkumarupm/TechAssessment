import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../service/api.service';
import { AssetInformation } from '../AssetInformation';
@Component({
  selector: 'app-asset-detail',
  templateUrl: './asset-detail.component.html',
  styleUrls: ['./asset-detail.component.scss']
})
export class AssetDetailComponent implements OnInit {
  Asset: AssetInformation;
errorMessage: any;
  constructor(private route: ActivatedRoute,
    private router: Router,private _service: ApiService) { }

  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      const id = param;
      this.getAsset(id);
    }
  }
  getAsset(id: string): void {
    this._service.getOneAsset(id).subscribe({
      next: product => this.Asset = product,
      error: err => this.errorMessage = err
    });
  }
  deleteAsset(): void {
    if (this.Asset.AssetId === "0") {
      // Don't delete, it was never saved.
    } else {
      if (confirm(`Really delete the Asset: ${this.Asset.FileName}?`)) {
        this._service.deleteAsset(this.Asset.AssetId)
          .subscribe({
            next: () => this.onSaveComplete(),
            error: err => this.errorMessage = err
          });
      }
    }
  }
  onBack(): void {
    this.router.navigate(['/welcome']);
  }
  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.router.navigate(['/Assets']);
  }
}
