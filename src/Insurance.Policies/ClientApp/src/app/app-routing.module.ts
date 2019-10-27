import { ListInsuranceClientsComponent } from './views/core/list-insurance-clients/list-insurance-clients.component';
import { LoginComponent } from './views/auth/login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'list-insurance-clients', component: ListInsuranceClientsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
