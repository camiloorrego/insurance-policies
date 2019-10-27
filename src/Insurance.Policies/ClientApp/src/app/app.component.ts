import { DataProvider } from './providers/data.provider';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { Component } from '@angular/core';
import { DateAdapter } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(public translate: TranslateService, public dataProvider: DataProvider, private dateAdapter: DateAdapter<Date>) {
    // tslint:disable-next-line:max-line-length
    this.dataProvider.token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwiVXNlcklkIjoiMSIsIm5iZiI6MTU3MjIxMTI3NiwiZXhwIjoxNTcyMjE4NDc2LCJpYXQiOjE1NzIyMTEyNzYsImlzcyI6IkdBUCIsImF1ZCI6IkdBUCJ9.bgxDodi41bqkagVhA1ajWurLBopem5Cs_2idlQvPWTE';
    translate.setDefaultLang('es');
    translate.use('es');
    this.dateAdapter.setLocale('es');
  }
}
