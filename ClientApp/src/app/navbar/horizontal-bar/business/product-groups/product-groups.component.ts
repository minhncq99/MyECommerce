import { Component, Input, OnInit } from '@angular/core';

import {  ProductGroups } from '../../../../app-models';

@Component({
  selector: 'app-product-groups',
  templateUrl: './product-groups.component.html',
  styleUrls: ['./product-groups.component.css']
})
export class ProductGroupsComponent implements OnInit {

  @Input() productGroup: Array<ProductGroups>;

  constructor() { }

  ngOnInit() {
  }

}
