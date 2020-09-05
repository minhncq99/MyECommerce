import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { getBaseUrl } from 'src/main';

@Component({
  selector: 'app-product-groups',
  templateUrl: './product-groups.component.html',
  styleUrls: ['./product-groups.component.css']
})
export class ProductGroupsComponent implements OnInit {

  productGroupName : any;

  params = {
    id: 1,
    page: 1,
    size: 8
  }
  
  productsData : any;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private routeActive: ActivatedRoute){}

  ngOnInit() {
    this.routeActive.queryParams.subscribe(paramsreq => {
      this.params.id = paramsreq['id']

      this.http.get(getBaseUrl() + 'api/productgroups/get-by-id?id='+this.params.id).subscribe(result => {
        this.productGroupName = result['name'];
      });

      this.http.get(getBaseUrl() + 'api/products/get-by-product-group?id='+this.params.id+'&page='+this.params.page+'&size='+this.params.size)
      .subscribe(result => {
        this.productsData = result;
      }, error => console.error(error));
    });
  }

  PreviousBtn(){
    if(this.params.page > 1){
      this.params.page -= 1;
      this.http.get(getBaseUrl() + 'api/products/get-by-product-group?id='+this.params.id+'&page='+this.params.page+'&size='+this.params.size)
      .subscribe(result => {
        this.productsData = result;
      }, error => console.error(error));
    }
    else{
      alert("Không có trang nào ở phía trước")
    }
  }

  NextBtn(){
    this.params.page += 1;
      this.http.get(getBaseUrl() + 'api/products/get-by-product-group?id='+this.params.id+'&page='+this.params.page+'&size='+this.params.size)
      .subscribe(result => {
        this.productsData = result;
      }, error => console.error(error));
  }

}
