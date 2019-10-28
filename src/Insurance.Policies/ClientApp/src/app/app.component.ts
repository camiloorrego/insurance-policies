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
    this.dataProvider.token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VybmFtZSI6ImFkbWluIiwiVXNlcklkIjoiMSIsIm5iZiI6MTU3MjIxODU0MSwiZXhwIjoxNTcyMjI1NzQxLCJpYXQiOjE1NzIyMTg1NDEsImlzcyI6IkdBUCIsImF1ZCI6IkdBUCJ9.3LMkAVCCd3WNr2dKGdziP_Hzq7J9CDPGdnhsuY39sz8';
    translate.setDefaultLang('es');
    translate.use('es');
    this.dateAdapter.setLocale('es');
  }
}
