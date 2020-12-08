import { Component, OnInit } from '@angular/core';

import { Business, ProductGroups } from '../../app-models';
import { RequestService } from '../../request.service';

@Component({
  selector: 'app-horizontal-bar',
  templateUrl: './horizontal-bar.component.html',
  styleUrls: ['./horizontal-bar.component.css']
})
export class HorizontalBarComponent implements OnInit {

  businessList: Array<Business>;
  productGroupList: Array<ProductGroups>;

  display = true;

  constructor(
    private requestSvc: RequestService
  ) { }

  ngOnInit(): void {
    this.getData();
  }


  changeDisplay(): void {
    this.display = !this.display;
  }

  filterProductGroups(business: Business): Array<ProductGroups> {
    return this.productGroupList
      .filter((productGroups) => productGroups.businessId === business.businessId);
  }

  getData(): void {
    this.requestSvc.getRequest('api/business/get-all')
    .subscribe((result) => {
      this.businessList = result;
    });

    this.requestSvc.getRequest('api/productgroups/get-all')
    .subscribe((result) => {
      this.productGroupList = result;
    });
  }

}
