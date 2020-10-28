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

  OrderData: any;
  ListOrderDetail: any;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private cookieService: CookieService,
    private router: Router) {}

  minBtn(item) {
    if (item.amount < 2) {
      alert('Số lượng sản phẩm không thể nhỏ hơn 0');
    } else {
      const body = {
        ProductId: item.productId,
        Amount: item.amount - 1
      };
      this.http.put(this.baseUrl + 'api/carts/update-item', body).subscribe(() => {

        this.http.get(this.baseUrl + 'api/carts/get-cart').subscribe(result => {
          this.cartData = result;
          if(this.cartData){
            this.temporarySum = 0;
            const arr: Cart[] = this.cartData;
            arr.forEach(Item => {
              this.temporarySum += Item.totalPrice;
            });
          }
        }, error => console.error(error));
      }, error => console.log(error));
    }
  }

  addBtn(item) {
    const body = {
      ProductId: item.productId,
      Amount: item.amount + 1
    };

    this.http.put(this.baseUrl + 'api/carts/update-item', body).subscribe(() => {
      this.http.get(this.baseUrl + 'api/carts/get-cart').subscribe(result => {
        this.cartData = result;
        if(this.cartData){
          this.temporarySum = 0;
          const Arr: Cart[] = this.cartData;
          Arr.forEach(Item => {
            this.temporarySum += item.totalPrice;
          });
        }
      }, error => console.error(error));
    }, error => console.error(error));
  }

  deleteBtn(item) {
    this.http.delete(this.baseUrl + 'api/carts/delete-item?id=' + item.productId).subscribe(result => {
      console.log(result);
      this.http.get(this.baseUrl + 'api/carts/get-cart').subscribe(Result => {
        this.cartData = Result;
        if (this.cartData) {
          this.temporarySum = 0;
          const arr: Cart[] = this.cartData;
          arr.forEach( Item => {
            this.temporarySum += Item.totalPrice;
          });
        }
      }, error => console.error(error));
    }, error => console.error(error));
  }

  orderBtn(comment: string) {
    console.log(this.access_token);
    const body = {
      comment : comment
    };
    if (this.access_token !== '') {
      this.http.post(getBaseUrl() + 'api/orders/add-order', body, {
        headers: new HttpHeaders({'Authorization': 'Bearer ' +  this.access_token}),
        responseType: 'text'
      })
      .subscribe(() => {
        alert('Bạn đã đặt hàng thành công');
        this.router.navigate(['/']);
      }, error => console.log(error));
    } else {
      alert('Bạn cần đăng nhập để mua hàng');
    }
  }

  ShowDetail(OrderId: number) {
    this.ordered.forEach(element => {
      if (element.orderId === OrderId) {
        this.OrderData = element;
      }
    });


    this.http.get(this.baseUrl + `api/orders/get-by-order-id?id=${ OrderId }`, {
      headers: new HttpHeaders({'Authorization': 'Bearer ' +  this.access_token})
    }).subscribe(result => {
      this.ListOrderDetail = result;
      console.log(this.OrderData);
      console.log(this.ListOrderDetail);
    });
  }

  ngOnInit() {
    this.access_token = this.cookieService.get('access_token');
    if (this.access_token) {
      this.http.get(this.baseUrl + 'api/orders/get-order-current', {
        headers: new HttpHeaders({'Authorization': 'Bearer ' +  this.access_token}
        )}
      ).subscribe(result => {
        this.ordered = result;
      }, error => console.error(error));
    }

    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const yyyy = today.getFullYear();

    this.orderTime = dd + '/' + mm + '/' + yyyy;

    this.http.get(this.baseUrl + 'api/carts/get-cart').subscribe(result => {
      this.cartData = result;
      if (this.cartData) {
        const arr: Cart[] = this.cartData;
        arr.forEach(item => {
          this.temporarySum += item.totalPrice;
        });
      }
    }, error => console.error(error));
  }

}
interface Cart {
  productId: number;
  productName: string;
  picture: string;
  amount: number;
  coupon: string;
  totalPrice: number;
}
