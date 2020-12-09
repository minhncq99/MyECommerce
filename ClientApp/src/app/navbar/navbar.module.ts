import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { HorizontalBarComponent } from './horizontal-bar/horizontal-bar.component';
import { BusinessComponent } from './horizontal-bar/business/business.component';
import { ProductGroupsComponent } from './horizontal-bar/business/product-groups/product-groups.component';

@NgModule({
  declarations: [NavbarComponent, HorizontalBarComponent, BusinessComponent, ProductGroupsComponent],
  imports: [
    CommonModule
  ],
  exports: [NavbarComponent, HorizontalBarComponent]
})
export class NavbarModule { }
