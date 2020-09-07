import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { CookieService } from 'ngx-cookie-service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  
  constructor(private http: HttpClient, private cookieService: CookieService, private formbuilder: FormBuilder){
    
  }

  ngOnInit() {
    this.registerForm = this.formbuilder.group({
      customerId: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      birthdate: new FormControl('', [Validators.required]),
      phonenumber: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      accountnumber: new FormControl('', [Validators.required]),
    })
  }

  onSubmit(){
    if(this.registerForm.controls.password.value != this.registerForm.controls.confirm.value){
        this.registerForm.setErrors(Validators.nullValidator)
        console.log(this.registerForm.value);
    }
    else{
      console.log(this.registerForm.value);

      this.http.post(getBaseUrl() + 'api/customers/create', this.registerForm.value).subscribe(result => {
        let user : any  = {
          username: this.registerForm.controls.customerId.value,
          password: this.registerForm.controls.password.value
        };
    
        this.http.post(getBaseUrl() + 'api/logins/customer', user).subscribe(result => {
         var temp : any = result;
          this.cookieService.set('access_token', temp.token);
          window.location.href = '';
        }, error => console.error(error));
      })
    }
  }

}
