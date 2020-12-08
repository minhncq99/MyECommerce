import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { HorizontalBarComponent } from './horizontal-bar/horizontal-bar.component';

@NgModule({
  declarations: [NavbarComponent, HorizontalBarComponent],
  imports: [
    CommonModule
  ],
  exports: [NavbarComponent, HorizontalBarComponent]
})
export class NavbarModule { }
