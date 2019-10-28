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
    translate.setDefaultLang('es');
    translate.use('es');
    this.dateAdapter.setLocale('es');
  }
}
