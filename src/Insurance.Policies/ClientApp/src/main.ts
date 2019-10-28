import 'hammerjs';
import { enableProdMode, ViewEncapsulation } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl() {

  let base: any;
  if (environment.production) {
    base = environment.url;
  } else {
    base = document.getElementsByTagName('base')[0].href;
  }


  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

platformBrowserDynamic(providers).bootstrapModule(AppModule, {
  defaultEncapsulation: ViewEncapsulation.None
})
  .catch(err => console.error(err));
