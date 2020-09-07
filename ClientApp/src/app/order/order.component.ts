import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  access_token: any;
  ordered: any;
  cartData: any;
  orderTime: any;
  temporarySum: any = 0;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, cookieService: CookieService, private router: Router){

    this.access_token = cookieService.get("access_token");
    if(this.access_token){
      this.http.get(baseUrl + 'api/orders/get-order-current', {
        headers: new HttpHeaders({'Authorization': 'Bearer ' +  this.access_token}
        )}
      ).subscribe(result => {
        this.ordered = result;
      }, error=> console.error(error));
    }

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    this.orderTime = dd + '/' + mm + '/' + yyyy;

    this.http.get(baseUrl + 'api/carts/get-cart').subscribe(result => {
      this.cartData = result;
      if(this.cartData){
        var arr: cart[] = this.cartData;
        arr.forEach(item=> {
          this.temporarySum += item.totalPrice;
        });
      }
    }, error => console.error(error));
  }

  orderBtn(event){
    if(this.access_token != ''){
      this.http.post(getBaseUrl() + 'api/orders/add-order', {comment : event.target.comment.value}, {
        headers: new HttpHeaders({'Authorization': 'Bearer ' +  this.access_token})
      }).subscribe(result=>{
        alert("Bạn đã đặt hàng thành công");
        this.router.navigate(['/']);
      }, error => console.log(error));
    }
    else{
      alert("Bạn cần đăng nhập để mua hàng");
    }
  }

  ngOnInit() {
  }
  
}
interface cart{
  productId: number,
  productName: string,
  picture: string,
  amount: number,
  coupon: string,
  totalPrice: number
}