import { ListInsurancePoliciesComponent } from './views/core/list-insurance-policies/list-insurance-policies.component';
import { AddInsurancePolicesComponent } from './views/core/add-insurance-polices/add-insurance-polices.component';
import { ListInsuranceClientsComponent } from './views/core/list-insurance-clients/list-insurance-clients.component';
import { LoginComponent } from './views/auth/login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppGuard } from './app.guard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'list-insurance-clients', component: ListInsuranceClientsComponent, canActivate: [AppGuard] },
  { path: 'add-insurance-policies', component: AddInsurancePolicesComponent, canActivate: [AppGuard] },
  { path: 'list-insurance-policies', component: ListInsurancePoliciesComponent, canActivate: [AppGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
