import { flatten } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';

import { Business, ProductGroups } from '../../../app-models';

@Component({
  selector: 'app-business',
  templateUrl: './business.component.html',
  styleUrls: ['./business.component.css']
})
export class BusinessComponent implements OnInit {

  @Input() business: Business;
  @Input() productGroup: Array<ProductGroups>;

  showProductGroups = false;

  constructor() { }

  ngOnInit(): void {
  }

  showBtn(): void {
    this.showProductGroups = !this.showProductGroups;
  }
}
