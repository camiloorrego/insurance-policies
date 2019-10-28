import { Injectable } from '@angular/core';
import { DataProvider } from './providers/data.provider';
import { isEmpty } from './utils/common';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(
    public dataProvider: DataProvider
  ) { }

  public hasToken(): boolean {
    return !isEmpty(this.dataProvider.token);
  }

}

