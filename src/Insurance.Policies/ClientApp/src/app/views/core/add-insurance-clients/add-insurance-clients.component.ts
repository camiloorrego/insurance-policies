import { TranslateService } from '@ngx-translate/core';
import { BaseService } from './../../../services/base.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-insurance-clients',
  templateUrl: './add-insurance-clients.component.html',
  styleUrls: ['./add-insurance-clients.component.scss']
})
export class AddInsuranceClientsComponent implements OnInit {
  id: number;
  url = '';
  data = [];
  policies = [];
  policeControl = new FormControl('', Validators.required);
  constructor(
    @Inject(MAT_DIALOG_DATA) public params: any,
    public dialogRef: MatDialogRef<AddInsuranceClientsComponent>,
    @Inject('BASE_URL') baseUrl: string,
    public baseService: BaseService,
    public toastr: ToastrService,
    public translate: TranslateService,
    public router: Router
  ) {
    this.id = params.id;
    this.data = params.data;
    this.url = baseUrl;
  }

  ngOnInit() {
    this.getPolicies();
  }

  save() {
    const items = [];

    for (const iterator of this.policeControl.value) {
      items.push({ clientId: this.id, policeId: iterator.policeId });
    }

    this.baseService.post(items, `${this.url}api/ClientsPolicies`, true).subscribe((r: any) => {
      this.toastr.success(this.translate.instant('common.saved'));
      this.dialogRef.close(true);

    }, e => {
      if (e.status === 401) {
        this.toastr.error(this.translate.instant('error.sessionexpired'));
        this.router.navigate(['']);
        return;
      }
      this.toastr.error(this.translate.instant('error.common'));
    });
  }

  getPolicies() {

    this.baseService.get(`${this.url}api/policies`, true).subscribe((r: any) => {

      const id = this.data.map((c: any) => c.policeId);



      const e = r.filter((item) => !id.includes(item.policeId));

      this.policies = e;

    }, e => {
      if (e.status === 401) {
        this.toastr.error(this.translate.instant('error.sessionexpired'));
        this.router.navigate(['']);
        return;
      }
      this.toastr.error(this.translate.instant('error.common'));
    });
  }
}
