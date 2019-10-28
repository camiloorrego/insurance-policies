import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { AppService } from './app.service';
import { Observable } from 'rxjs';
import { DataProvider } from './providers/data.provider';

@Injectable()
export class AppGuard implements CanActivate {
  constructor(
    public auth: AppService,
    public router: Router,
    public dataProvider: DataProvider) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.auth.hasToken()) {
      return true;
    } else {
      this.router.navigate(['login']);
    }
  }
}
