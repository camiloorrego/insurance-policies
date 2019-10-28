import { DialogService } from './controls/dialog/dialog.service';
import { DialogComponent } from './controls/dialog/dialog.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ViewsModule } from './views/views.module';
import { DataProvider } from './providers/data.provider';
import { AppAngularMaterialModule } from './modules/app-angular-material/app-angular-material.module';
import { AppGuard } from './app.guard';

@NgModule({
  declarations: [
    AppComponent,
    DialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ViewsModule,
    AppAngularMaterialModule
  ],
  providers: [DataProvider, DialogService, AppGuard],
  entryComponents: [DialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
