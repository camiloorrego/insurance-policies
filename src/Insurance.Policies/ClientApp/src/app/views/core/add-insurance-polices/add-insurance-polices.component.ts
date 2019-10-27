import { BaseService } from 'src/app/services/base.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DataProvider } from 'src/app/providers/data.provider';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Router } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { valueSelected } from 'src/app/utils/common';

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
  risks = [];
  types = [];
  minDate: any;
  onSelectedDate: any;
  filteredPolicyTypes: Observable<any[]>;
  filteredRiskTypes: Observable<any[]>;
  editData: any = {};

  constructor(
    private formBuilder: FormBuilder,
    public dataProvider: DataProvider,
    public router: Router,
    public baseService: BaseService) {
  }

  ngOnInit() {
    this.createForm();
  }

  backPage() {
    this.router.navigate(['list-insurance-policies']);

  }

  loadData() {

    forkJoin(
      this.baseService.get('https://localhost:44347/api/PolicyTypes', true),
      this.baseService.get('https://localhost:44347/api/RiskTypes', true),
    ).pipe(map((allResponses) => {
      return {
        types: allResponses[0],
        risks: allResponses[1]
      };
    })).subscribe((response: any) => {
      this.risks = response.risks;
      this.types = response.types;



      this.f.policytype.setValidators(Validators.compose([Validators.required, valueSelected(this.types, 'id', 'id')]));
      this.f.risktype.setValidators(Validators.compose([Validators.required, valueSelected(this.risks, 'id', 'id')]));

      this.filteredPolicyTypes = this.f.policytype.valueChanges
        .pipe(
          startWith(this.editData.policytype ? this.editData.policytype : ''),
          map(value => this._filterTypes(value))
        );

      this.filteredRiskTypes = this.f.risktype.valueChanges
        .pipe(
          startWith(this.editData.risktype ? this.editData.risktype : ''),
          map(value => this._filterRisks(value))
        );



    });
  }

  createForm() {
    this.formAdd = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      policytype: ['', Validators.required],
      date: ['', Validators.required],
      terms: ['', Validators.compose([Validators.required, Validators.pattern(/^[0-9]*$/)]) ],
      cost: ['', Validators.compose([Validators.required, Validators.pattern(/^[0-9]*$/)])],
      risktype: ['', Validators.required]
    });

    this.loadData();
  }

  setValues() {

    // this.editData.date = this.item.daterequest;
    // this.editData.currency = this.currencies.find((e: any) => e.id === this.item.money);
    // this.editData.costCentre = this.costCentres.find((e: any) => e.id === this.item.costcenter);


    // this.f.date.setValue(new Date(this.editData.date));

    // this.f.costcentre.setValue(this.editData.costCentre);
    // this.f.phone.setValue(this.item.phone);
    // this.f.request.setValue(this.editData.request);
    // this.f.concept.setValue(this.editData.indicator);
    // this.f.currency.setValue(this.editData.currency);
    // this.f.shoppinggroup.setValue(this.editData.shoppinggroup);
    // this.f.comment.setValue(this.item.comments);



  }

  get f() { return this.formAdd.controls; }


  private _filterRisks(value: any): any[] {
    if (value && value.id) {
      return this.risks.filter(option => option.id === value.id);
    } else {
      const filterValue = value.toLowerCase();
      return this.risks.map((item) => {
        item.complete = `${item.id} - ${item.name}`;
        return item;
      }).filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  private _filterTypes(value: any): any[] {
    if (value && value.id) {
      return this.types.filter(option => option.id === value.id);
    } else {
      const filterValue = value.toLowerCase();
      return this.types.map((item) => {
        item.complete = `${item.id} - ${item.name}`;
        return item;
      }).filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  public displayFn(e?: any): string | undefined {
    return e ? `${e.id} - ${e.name}` : undefined;
  }
}
