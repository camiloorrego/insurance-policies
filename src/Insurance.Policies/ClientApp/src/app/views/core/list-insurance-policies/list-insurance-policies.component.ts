import { TranslateService } from '@ngx-translate/core';
import { DialogService } from './../../../controls/dialog/dialog.service';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatRadioChange } from '@angular/material';
import { BaseService } from 'src/app/services/base.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataProvider } from 'src/app/providers/data.provider';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-insurance-policies',
  templateUrl: './list-insurance-policies.component.html',
  styleUrls: ['./list-insurance-policies.component.scss']
})
export class ListInsurancePoliciesComponent implements OnInit {
  displayedColumns = [
    'name',
    'policyType',
    'effectiveDate',
    'terms',
    'riskType',
    'coverage',
    'cost',
    'description',
    'buttons'
  ];
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  url = '';


  constructor(
    public baseService: BaseService,
    public router: Router,
    public data: DataProvider,
    @Inject('BASE_URL') baseUrl: string,
    public dialog: DialogService,
    public traslate: TranslateService,
    public toastr: ToastrService
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.getPolicies();
    this.data.item = null;
  }

  confirm(id: number) {
    this.dialog.confirm(this.traslate.instant('common.confirm'), this.delete.bind(this, id), '');
  }

  delete(id: number) {
    this.baseService.delete(`${this.url}api/policies/${id}`, true).subscribe((r: any) => {
      this.toastr.success(this.traslate.instant('common.deleted'));
      this.getPolicies();

    }, e => {
      this.toastr.error(this.traslate.instant('error.common'));
    });
  }

  edit(row: any) {
    this.data.item = row;
    this.router.navigate(['add-insurance-policies']);
  }

  getPolicies() {

    this.baseService.get(`${this.url}api/policies`, true).subscribe((r: any) => {
      this.dataSource = new MatTableDataSource(r);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;

    }, e => console.log(e));
  }

  backPage() {
    this.router.navigate(['list-insurance-clients']);

  }

  add() {
    this.router.navigate(['add-insurance-policies']);
  }
}
