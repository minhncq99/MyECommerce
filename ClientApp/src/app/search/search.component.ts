import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['../product-groups/product-groups.component.css']
})
export class SearchComponent implements OnInit {

  req = {
    name: '',
    page: 1,
    size: 8
  };

  productsData: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.req.name = params.keys;
    })

    this.http.get(this.baseUrl + 'api/products/search', {
      params: {
        name: this.req.name,
        page: this.req.page.toString(),
        size: this.req.size.toString()
      }
    }).subscribe(result => {
      this.productsData = result;
    }, error => console.log(error));
  }

  PreviousBtn() {
    if (this.req.page > 1) {
      this.req.page -= 1;
      this.http.get(this.baseUrl + 'api/products/search', {
        params: {
          name: this.req.name,
          page: this.req.page.toString(),
          size: this.req.size.toString()
        }
      }).subscribe(result => {
        this.productsData = result;
        console.log(this.productsData);
      }, error => console.log(error));
    }
    else {
      alert("Không có trang nào ở phía trước")
    }
  }

  NextBtn() {
    this.req.page += 1;
    this.http.get(this.baseUrl + 'api/products/search', {
      params: {
        name: this.req.name,
        page: this.req.page.toString(),
        size: this.req.size.toString()
      }
    }).subscribe(result => {
      this.productsData = result;
    }, error => console.log(error));
  }
}
