import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatRadioChange } from '@angular/material';
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
  constructor(
    public baseService: BaseService,
    public dialog: MatDialog,
    public router: Router,
    public route: ActivatedRoute,
    public data: DataProvider
  ) {

  }

  ngOnInit() {
    this.getWfs();
  }

  getWfs() {

    // this.baseService.get(`${environment.urlApi}api/workflows`).subscribe((r: any) => {
    //   this.dataSource = new MatTableDataSource(r);
    //   this.dataSource.paginator = this.paginator;
    //   this.dataSource.sort = this.sort;

    // }, e => console.log(e));
  }

  changeLan(e: MatRadioChange) {
    console.log(e);

  }

  add() {
    // const dialogRef = this.dialog.open(WorkFlowComponent,
    //   {
    //     width: '95%',
    //     disableClose: true
    //   });

    // dialogRef.afterClosed().subscribe(async (result: any) => {
    //   if (result) {
    //     this.getWfs();
    //   }
    // });
  }

}
