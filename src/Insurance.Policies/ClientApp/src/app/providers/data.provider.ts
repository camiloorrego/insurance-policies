import { Injectable } from '@angular/core';

@Injectable()
export class DataProvider {
  // private TOKEN: string;


  // get token(): string {
  //   return this.TOKEN ? this.TOKEN : '';
  // }
  // set token(value: string) {
  //   this.TOKEN = value;
  // }

  get item(): any {
    const value = JSON.parse(sessionStorage.getItem(`ITEM`));
    return value || null;
  }

  set item(value: any) {
    sessionStorage.setItem(`ITEM`, JSON.stringify(value));
  }

  get token(): any {
    const value = JSON.parse(sessionStorage.getItem(`TOKEN`));
    return value || null;
  }

  set token(value: any) {
    sessionStorage.setItem(`TOKEN`, JSON.stringify(value));
  }
}
