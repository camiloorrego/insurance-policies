import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatRadioChange, DateAdapter, MatSelectChange } from '@angular/material';
import { BaseService } from 'src/app/services/base.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataProvider } from 'src/app/providers/data.provider';
import { AddInsuranceClientsComponent } from '../add-insurance-clients/add-insurance-clients.component';
import { SelectionModel } from '@angular/cdk/collections';
import { DialogService } from 'src/app/controls/dialog/dialog.service';

@Component({
  selector: 'app-list-insurance-clients',
  templateUrl: './list-insurance-clients.component.html',
  styleUrls: ['./list-insurance-clients.component.scss']
})
export class ListInsuranceClientsComponent implements OnInit {
  displayedColumns = [
    'select',
    'client',
    'name',
    'description'
  ];
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  url = '';
  clients = [];
  id: number;
  selection = new SelectionModel<any>(true, []);

  constructor(
    public baseService: BaseService,
    public dialog: MatDialog,
    public router: Router,
    public route: ActivatedRoute,
    public data: DataProvider,
    public translate: TranslateService,
    private dateAdapter: DateAdapter<Date>,
    @Inject('BASE_URL') baseUrl: string,
    public traslate: TranslateService,
    public toastr: ToastrService,
    public dialogService: DialogService
  ) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.getClients();
    this.id = 0;
  }

  confirm(id: number) {
    this.dialogService.confirm(this.traslate.instant('common.confirm'), this.delete.bind(this, id), '');
  }

  delete(id: number) {

    const items = this.selection.selected.map((item) => item.id);

    this.baseService.post(items, `${this.url}api/ClientsPolicies/delete`, true).subscribe((r: any) => {
      this.toastr.success(this.traslate.instant('common.deleted'));
      this.getRegisters(this.id);

    }, e => {
      this.toastr.error(this.traslate.instant('common.error'));
    });
  }

  selected(e: MatSelectChange) {
    this.getRegisters(e.value);
    this.id = e.value;
  }

  getClients() {

    this.baseService.get(`${this.url}api/clients`, true).subscribe((r: any) => {
      this.clients = r;
    }, e => console.log(e));
  }

  getRegisters(id: number) {

    this.baseService.get(`${this.url}api/ClientsPolicies/${id}`, true).subscribe((r: any) => {
      this.dataSource = new MatTableDataSource(r);
    }, e => console.log(e));
  }

  changeLan(e: MatRadioChange) {
    this.translate.use(e.value);
    this.dateAdapter.setLocale(e.value);
  }

  add() {
    const dialogRef = this.dialog.open(AddInsuranceClientsComponent,
      {
        minWidth: '50%',
        disableClose: true,
        data: {
          id: this.id,
          data: this.dataSource.data,
        }
      });

    dialogRef.afterClosed().subscribe(async (result: any) => {
      if (result) {
        this.getRegisters(this.id);
      }
    });
  }

  addPolicy() {
    this.router.navigate(['list-insurance-policies']);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

}
