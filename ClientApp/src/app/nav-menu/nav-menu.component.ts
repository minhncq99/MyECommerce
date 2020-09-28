import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { getBaseUrl } from 'src/main';
import { Router } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  
  businessData: any;
  productGroupsData: any;
  userData: any;

  searchForm: FormGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private cookieService: CookieService, private router: Router, private formBuilder: FormBuilder ){
    this.searchForm = this.formBuilder.group({
      searchKeyWord: new FormControl('', [Validators.required])
    });
    
    http.get(baseUrl + 'api/business/get-all').subscribe(result => {
      this.businessData = result;
    }, error => console.error(error));

    http.get(baseUrl + 'api/productgroups/get-all').subscribe(result => {
      this.productGroupsData = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
    const access_token = this.cookieService.get('access_token');
    if(access_token != ''){
      console.log(access_token);
      this.http.get(getBaseUrl() + 'api/customers/current-customer', {
        headers: new HttpHeaders({'Authorization': 'Bearer ' +  access_token})
      }).subscribe(result => {
        this.userData = result;
      }, error => console.error(error));
    }
  }
  
  Logout(){
    this.cookieService.set('access_token', '');
    window.location.href = '';
  }

  ngOnSearch() {
    this.router.navigate(['search'], {queryParams: { keys : this.searchForm.controls.searchKeyWord.value }});
  }
}

