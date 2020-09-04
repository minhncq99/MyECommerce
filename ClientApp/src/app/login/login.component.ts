import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  access_token: any;

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  ngOnInit() {
  }

  onSubmit(){
    let user : any  = {
      username: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value
    };

    this.http.post(getBaseUrl() + 'api/logins/customer', user).subscribe(result => {
      this.access_token = result;
      this.cookieService.set('access_token', this.access_token.token);
      window.location.href = '';
    }, error => console.error(error));
  }
}
