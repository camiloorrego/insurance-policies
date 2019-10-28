import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatRadioChange, DateAdapter, MatSelectChange } from '@angular/material';
import { BaseService } from 'src/app/services/base.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataProvider } from 'src/app/providers/data.provider';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-list-insurance-clients',
  templateUrl: './list-insurance-clients.component.html',
  styleUrls: ['./list-insurance-clients.component.scss']
})
export class ListInsuranceClientsComponent implements OnInit {
  displayedColumns = ['id', 'name', 'buttons'];
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  url = '';
  clients = [];
  constructor(
    public baseService: BaseService,
    public dialog: MatDialog,
    public router: Router,
    public route: ActivatedRoute,
    public data: DataProvider,
    public translate: TranslateService,
    private dateAdapter: DateAdapter<Date>,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.getClients();
  }

  selected(e: MatSelectChange) {
    console.log(e);
    this.getRegisters(e.value);
  }

  getClients() {

    this.baseService.get(`${this.url}api/clients`, true).subscribe((r: any) => {
      this.clients = r;
    }, e => console.log(e));
  }

  getRegisters(id: number) {

    this.baseService.get(`${this.url}api/ClientsPolicies/${id}`, true).subscribe((r: any) => {

    }, e => console.log(e));
  }

  changeLan(e: MatRadioChange) {
    this.translate.use(e.value);
    this.dateAdapter.setLocale(e.value);

  }

  add() {
    // this.router.navigate(['add-insurance-policies']);
  }

  addPolicy() {
    this.router.navigate(['list-insurance-policies']);
  }

}
