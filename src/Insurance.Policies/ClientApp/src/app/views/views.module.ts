import { AppPipesModule } from './../modules/app-pipes/app-pipes.module';
import { AppTranslateModule } from './../modules/app-translate/app-translate.module';
import { AppAngularMaterialModule } from './../modules/app-angular-material/app-angular-material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './auth/login/login.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ListInsuranceClientsComponent } from './core/list-insurance-clients/list-insurance-clients.component';
import { AddInsurancePolicesComponent } from './core/add-insurance-polices/add-insurance-polices.component';
import { ListInsurancePoliciesComponent } from './core/list-insurance-policies/list-insurance-policies.component';
import { ToastrModule } from 'ngx-toastr';
import { AddInsuranceClientsComponent } from './core/add-insurance-clients/add-insurance-clients.component';

@NgModule({
  declarations: [LoginComponent,
    ListInsuranceClientsComponent,
    AddInsurancePolicesComponent,
    ListInsurancePoliciesComponent,
    AddInsuranceClientsComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    AppAngularMaterialModule,
    AppTranslateModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      progressBar: true,
      timeOut: 5000
    }),
    AppPipesModule
  ],
  entryComponents: [AddInsuranceClientsComponent]
})
export class ViewsModule { }
