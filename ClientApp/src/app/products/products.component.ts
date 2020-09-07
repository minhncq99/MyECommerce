import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productData: any = {
    picture : ''
  };

  id: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private routeActive: ActivatedRoute){
    this.routeActive.queryParams.subscribe(params => {
      this.id = params['id'];
    });

    this.http.get(getBaseUrl() + 'api/products/get-by-id?id=' + this.id).subscribe(result => {
      this.productData = result;
      console.log(this.productData);
    });
  }

  onSubmit(event){
    let amout = event.target.amout.value;
    if(amout > 0){
      let body : any = {
        ProductId: this.productData.productId,
        Amount: Number(amout)
      };

      console.log(body);

      this.http.post(getBaseUrl() + 'api/carts/add-item', body).subscribe(result => {
        if(result['success']){
          alert("Đã thêm sản phẩm vào giỏ hàng!");
        } else {
          alert("Thêm sản phẩm không thành công");
        }
      }, error => console.error(error));
    }else{
      alert("Số lượng sản phẩn mua không thể nhỏ hơn 0");
    }
  }

  ngOnInit() {
    
  }

}
