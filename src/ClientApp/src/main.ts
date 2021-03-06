import 'hammerjs';
import { enableProdMode, ViewEncapsulation } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl() {

  const base = environment.url;
  // if (environment.production) {
  //   base = environment.url;
  // } else {
  //   base = document.getElementsByTagName('base')[0].href;
  // }
  return base;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];


platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
