import { BaseService } from './../../../services/base.service';
import { Component, OnInit, Inject } from '@angular/core';
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
  public hidePassword = true;
  public isLoading = false;
  errorText = '';
  url = '';

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    public router: Router,
    public dataProvider: DataProvider,
    public baseService: BaseService,
    public translate: TranslateService) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.form = new FormGroup({
      userForm: new FormControl('', Validators.required),
      passwordForm: new FormControl('', Validators.required)
    });
  }

  login() {
    this.isLoading = true;
    const body = {
      username: this.f.userForm.value,
      password: this.f.passwordForm.value,
    };

    this.baseService.post(body, `${this.url}api/users/signin`).subscribe((r: any) => {
      this.dataProvider.token = r.access_token;
      this.router.navigate(['list-insurance-clients']);

    }, e => {
      if (e.error && e.error.error_code) {
        this.errorText = this.translate.instant(`error.${e.error.error_code}`);
      }

      if (this.errorText === `error.${e.error.error_code}`) {
        this.errorText = this.translate.instant(`error.common`);
      }

      this.isLoading = false;
    });

  }

  get f() {
    return this.form.controls;
  }

}
