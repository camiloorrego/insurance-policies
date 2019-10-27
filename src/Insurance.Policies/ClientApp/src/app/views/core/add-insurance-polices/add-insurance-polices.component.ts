import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable, forkJoin } from 'rxjs';
import { DataProvider } from 'src/app/providers/data.provider';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Router } from '@angular/router';

export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'YYYY',
  },
};

@Component({
  selector: 'app-add-insurance-polices',
  templateUrl: './add-insurance-polices.component.html',
  styleUrls: ['./add-insurance-polices.component.scss'],
  providers: [
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class AddInsurancePolicesComponent implements OnInit {
  public formAdd: FormGroup;
  @Input() item: any;
  @Output() next: EventEmitter<any> = new EventEmitter();
  @Output() complete: EventEmitter<any> = new EventEmitter();
  currencies = [];
  costCentres = [];
  indicators = [];
  requests = [];
  shoppinggroups = [];
  users = [];
  minDate: any;
  onSelectedDate: any;
  filteredCostCentre: Observable<any[]>;
  editData: any = {};

  constructor(private formBuilder: FormBuilder, public dataProvider: DataProvider, public router: Router) {

  }

  ngOnInit() {
    this.createForm();

    setTimeout(() => {
      this.loadData();
    });
  }

  backPage() {
    this.router.navigate(['list-insurance-policies']);

  }

  loadData() {

    // forkJoin(
    //   this.generalService.getCurrencies(),
    //   this.generalService.getCostCentres(),
    //   this.generalService.getIndicators(),
    //   this.generalService.getShoppingGroups(),
    //   this.generalService.getRequests(),
    // ).pipe(map((allResponses) => {
    //   return {
    //     currencies: allResponses[0],
    //     costCentres: allResponses[1],
    //     indicators: allResponses[2],
    //     shoppinggroups: allResponses[3],
    //     requests: allResponses[4],
    //   };
    // })).subscribe((response: any) => {
    //   this.currencies = response.currencies;
    //   this.costCentres = response.costCentres;
    //   this.indicators = response.indicators;
    //   this.shoppinggroups = response.shoppinggroups;
    //   this.requests = response.requests;

    //   if (this.item) {
    //     this.setValues();
    //   }

    // this.f.costcentre.setValidators(Validators.compose([Validators.required, valueSelected(this.costCentres, 'id', 'id')]));
    // this.f.concept.setValidators(Validators.compose([Validators.required, valueSelected(this.indicators, 'id', 'id')]));
    // this.f.request.setValidators(Validators.compose([Validators.required, valueSelected(this.requests, 'id', 'id')]));

    // this.filteredCostCentre = this.f.costcentre.valueChanges
    //   .pipe(
    //     startWith(this.editData.costCentre ? this.editData.costCentre : ''),
    //     map(value => this._filter(value))
    //   );

    // this.filteredConcept = this.f.concept.valueChanges
    //   .pipe(
    //     startWith(this.editData.indicator ? this.editData.indicator : ''),
    //     map(value => this._filterIndicators(value))
    //   );

    // this.filteredRequest = this.f.request.valueChanges
    //   .pipe(
    //     startWith(this.editData.request ? this.editData.request : ''),
    //     map(value => this._filterRequests(value))
    //   );

    // });
  }

  loadUsers(costcentreId: number) {
    // this.generalService.getUsers(costcentreId).subscribe((resp: any) => {
    //   this.users = resp;
    //   this.f.user.setValidators(Validators.compose([Validators.required, valueSelected(this.users, 'id', 'id')]));

    //   if (this.item) {
    //     this.editData.user = this.users.find((e: any) => e.id === this.item.personrequesting);
    //     this.f.user.setValue(this.editData.user);
    //     this.dataProvider.loading = false;
    //   }

    //   this.filteredUser = this.f.user.valueChanges
    //     .pipe(
    //       startWith(this.editData.user ? this.editData.user : ''),
    //       map(value => this._filterUsers(value))
    //     );
    //   this.complete.emit();
    // });
  }

  createForm() {
    this.formAdd = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      policytype: ['', Validators.required],
      date: ['', Validators.required],
      terms: ['', Validators.required],
      cost: ['', null],
      risktype: ['', Validators.required]
    });
  }

  add() {
    this.next.emit();
  }

  setValues() {

    this.editData.date = this.item.daterequest;
    this.editData.currency = this.currencies.find((e: any) => e.id === this.item.money);
    this.editData.costCentre = this.costCentres.find((e: any) => e.id === this.item.costcenter);
    this.editData.indicator = this.indicators.find((e: any) => e.id === this.item.indicator);
    this.editData.request = this.requests.find((e: any) => e.id === this.item.purchaserequest);
    this.editData.shoppinggroup = this.shoppinggroups.find((e: any) => e.id === this.item.shoppinggroup);


    this.f.date.setValue(new Date(this.editData.date));

    this.f.costcentre.setValue(this.editData.costCentre);
    this.f.phone.setValue(this.item.phone);
    this.f.request.setValue(this.editData.request);
    this.f.concept.setValue(this.editData.indicator);
    this.f.currency.setValue(this.editData.currency);
    this.f.shoppinggroup.setValue(this.editData.shoppinggroup);
    this.f.comment.setValue(this.item.comments);

    this.f.negotiationtype.disable();

    if (this.editData.typenegotiation.detail) {
      // this.f.initialdate.setValue(new Date(this.item.startdate));
      this.f.periods.setValue(this.item.numberperiods);

      //  this.f.initialdate.disable();
      this.f.periods.disable();
    }

  }

  get f() { return this.formAdd.controls; }

  private _filter(value: any): any[] {
    if (value && value.id) {
      this.loadUsers(value.id);
      return this.costCentres.filter(option => option.id === value.id);
    } else {
      this.f.user.reset();
      this.users = [];
      const filterValue = value.toLowerCase();
      return this.costCentres.filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  private _filterUsers(value: any): any[] {
    if (value && value.id) {
      return this.users.filter(option => option.id === value.id);
    } else {
      const filterValue = value.toLowerCase();
      return this.users.filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  private _filterIndicators(value: any): any[] {
    if (value && value.id) {
      return this.indicators.filter(option => option.id === value.id);
    } else {
      const filterValue = value.toLowerCase();
      return this.indicators.filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  private _filterRequests(value: any): any[] {
    if (value && value.id) {
      return this.requests.filter(option => option.id === value.id);
    } else {
      const filterValue = value.toLowerCase();
      return this.requests.filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  public displayFn(e?: any): string | undefined {
    return e ? `${e.id} - ${e.name}` : undefined;
  }
}
