import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['../product-groups/product-groups.component.css']
})
export class HomeComponent {
  productsData : any;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private routeActive: ActivatedRoute){
    this.http.get(baseUrl + 'api/products/get-new?size=4')
      .subscribe(result => {
        this.productsData = result;
      }, error => console.error(error));
  }
}
