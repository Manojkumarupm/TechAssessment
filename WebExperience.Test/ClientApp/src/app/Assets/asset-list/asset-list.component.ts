import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../service/api.service';
import { PaginationService } from '../../service/pagination.service'; 

@Component({
  selector: 'app-asset-list',
  templateUrl: './asset-list.component.html',
  styleUrls: ['./asset-list.component.scss']
})
export class AssetListComponent implements OnInit {

  collection = { count: 60, data: [] };
  config = {
    itemsPerPage: 10,
    currentPage: 1,
    totalItems : 100, 
  };

  public maxSize: number = 7;
  public directionLinks: boolean = true;
  public autoHide: boolean = false;
  public responsive: boolean = true;
  public labels: any = {
      previousLabel: 'Prev',
      nextLabel: 'Next',
      screenReaderPaginationLabel: 'Pagination',
      screenReaderPageLabel: 'page',
      screenReaderCurrentLabel: `You're on page`
  };
  companies = [];
  pageNo: any = 1;
  pageNumber: boolean[] = [];
  sortOrder: any = 'Country';
  
  pageField = [];
  exactPageList: any;
  paginationData: number;
  companiesPerPage: any = 100;
  totalCompanies: any;
  totalAssetsCount: any;
  currentPage: any;
  pageSize = 10;
  pageSizes = [10, 15, 20];
  constructor(public service: ApiService, public paginationService: PaginationService) { 
    //Create dummy data
    /*for (var i = 0; i < this.collection.count; i++) {
      this.collection.data.push(
        {
          id: i + 1,
          value: "items number " + (i + 1)
        }
      );
    }

    this.config = {
      itemsPerPage: 10,
      currentPage: 1,
      totalItems: this.collection.count,
      
    };*/
  }

  ngOnInit() {
    this.pageNumber[0] = true;
    this.paginationService.temppage = 0;
    this.collection.count=0;
    this.getAllAssetsCount();
    this.getAllCompanies();
  }

  getRequestParams(searchTitle, page, pageSize): any {
    // tslint:disable-next-line:prefer-const
    let params = {};

    if (searchTitle) {
      params[`title`] = searchTitle;
    }

    if (page) {
      params[`page`] = page - 1;
    }

    if (pageSize) {
      params[`size`] = pageSize;
    }

    return params;
  }
  handlePageChange(event): void {
    this.config.itemsPerPage = event;
    this.getAllCompanies();
  }

  handlePageSizeChange(event): void {
    this.config.itemsPerPage = event.target.value;
    this.config.currentPage = 1;
    this.getAllCompanies();
  }
  getAllCompanies() {
    this.service.getAllAssets(this.config.currentPage, this.config.itemsPerPage, this.sortOrder).subscribe((data: any) => {
      this.collection.data = data;
          })
  }
setOrder(order: string)
{
this.sortOrder=order;
this.getAllCompanies();
} 
  //Method For Pagination  
  totalNoOfPages() {  
  
    this.paginationData = Number(this.totalAssetsCount / this.companiesPerPage);  
    let tempPageData = this.paginationData.toFixed();  
    if (Number(tempPageData) < this.paginationData) {  
      this.exactPageList = Number(tempPageData) + 1;  
      this.paginationService.exactPageList = this.exactPageList;  
    } else {  
      this.exactPageList = Number(tempPageData);  
      this.paginationService.exactPageList = this.exactPageList  
    }  
    this.paginationService.pageOnLoad();  
    this.pageField = this.paginationService.pageField;  
  
  }  
  showCompaniesByPageNumber(page, i) {
    this.companies = [];
    this.pageNumber = [];
    this.pageNumber[i] = true;
    this.pageNo = page;
    this.getAllCompanies();
  }

  getAllAssetsCount() {
    this.service.getAllAssetsCount().subscribe((res: any) => {
      this.config.totalItems = res;
      this.collection.count = res;
      this.totalNoOfPages()
    })
  }
  pageChanged(event){
    this.config.currentPage = event;
    this.getAllCompanies();
  }
}  
