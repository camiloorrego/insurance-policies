import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataProvider } from 'src/app/providers/data.provider';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public form: FormGroup;
  public legacyForm: FormGroup;
  public invalid = false;
  public isError = false;
  public isAccessSchedule = false;
  public hidePassword = true;
  public contentLogin: any;
  public env = environment;
  public isLoading = false;


  constructor(
    public router: Router, public dataProvider: DataProvider, public translate: TranslateService) {




  }

  ngOnInit() {
    this.form = new FormGroup({
      userForm: new FormControl('', Validators.required),
      passwordForm: new FormControl('', Validators.required)
    });

  }





  login() {

  }




}
