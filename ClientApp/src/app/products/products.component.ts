import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { ActivatedRoute } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  productData: any = {
    picture: ''
  };

  commentList: any;

  evaluteList: any;

  id: number;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private routeActive: ActivatedRoute,
    private cookieSvc: CookieService) {}

  ngOnInit() {
    this.routeActive.queryParams.subscribe(params => {
      this.id = params['id'];
    });

    this.http.get(getBaseUrl() + 'api/products/get-by-id?id=' + this.id)
    .subscribe(result => {
      this.productData = result;
    });

    this.http.get(getBaseUrl() + 'api/comments/get-by-product-id?productId=' + this.id)
    .subscribe(result => {
      this.commentList = result;
    });

    this.http.get(getBaseUrl() + 'api/evaluates/get-by-product-id', {
      params: {
        productId: String(this.id)
      }
    }).subscribe(result => {
      this.evaluteList = result;
    });
  }

  onSubmit(event) {
    const amout = event.target.amout.value;
    if (amout > 0) {
      const body = {
        ProductId: this.productData.productId,
        Amount: Number(amout)
      };


      this.http.post(getBaseUrl() + 'api/carts/add-item', body)
      .subscribe(result => {
        if (result['success']) {
          alert('Đã thêm sản phẩm vào giỏ hàng!');
        } else {
          alert('Thêm sản phẩm không thành công');
        }
      }, error => console.error(error));
    } else {
      alert('Số lượng sản phẩn mua không thể nhỏ hơn 0');
    }
  }

  addComment(content: string) {
    const access_token = this.cookieSvc.get('access_token');
    if (access_token !== '') {
      const body = {
        Content: content,
        ProductId: Number(this.id)
      };
      const header = new HttpHeaders({'Authorization': 'Bearer ' +  access_token});

      this.http.post(getBaseUrl() + 'api/comments/create', body,
      { headers: new HttpHeaders({'Authorization': 'Bearer ' +  access_token})})
      .subscribe(() => {
        alert('Thêm bình luận thành công');
        this.http.get(getBaseUrl() + `api/comments/get-by-product-id?productId=${ this.id }`)
        .subscribe(res => {
          this.commentList = res;
        });
      });
    } else {
      alert('Bạn cần đăng nhập để có thể bình luận về sản phẩm');
    }
  }

  addEvalute(evaluteStar: number) {
    if (evaluteStar < 0 || evaluteStar > 5) {
      alert('Số sao không hợp lệ');
    } else {
      const access_token = this.cookieSvc.get('access_token');
      if (access_token !== '') {
        const body = {
          numberStar: Number(evaluteStar),
          ProductId: Number(this.id)
        };
        const header = new HttpHeaders({'Authorization': 'Bearer ' +  access_token});

        this.http.post(getBaseUrl() + 'api/evaluates/create', body,
        { headers: new HttpHeaders({'Authorization': 'Bearer ' +  access_token})})
        .subscribe(() => {
          alert('Thêm đánh giá thành công');

          this.http.get(getBaseUrl() + 'api/evaluates/get-by-product-id', {
            params: {
              productId: String(this.id)
            }
          }).subscribe(result => {
            this.evaluteList = result;
          });
        }, () => {
          alert('Không thể đánh giá 2 lần cùng 1 sản phẩm');
        });
      } else {
        alert('Bạn cần đăng nhập để có thể đánh giá về sản phẩm');
      }
    }
  }
}
