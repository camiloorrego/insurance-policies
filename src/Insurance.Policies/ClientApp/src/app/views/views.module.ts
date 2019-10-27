import { AppTranslateModule } from './../modules/app-translate/app-translate.module';
import { AppAngularMaterialModule } from './../modules/app-angular-material/app-angular-material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './auth/login/login.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    AppAngularMaterialModule,
    AppTranslateModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class ViewsModule { }
