import { Injectable } from '@angular/core';

@Injectable()
export class DataProvider {
  private TOKEN: string;


  get token(): string {
    return this.TOKEN ? this.TOKEN : '';
  }
  set token(value: string) {
    this.TOKEN = value;
  }
}
