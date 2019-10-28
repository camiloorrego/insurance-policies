import { TranslateService } from '@ngx-translate/core';
import { BaseService } from 'src/app/services/base.service';
import { Component, OnInit, Input, Output, EventEmitter, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DataProvider } from 'src/app/providers/data.provider';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { Router } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { valueSelected } from 'src/app/utils/common';
import { PolicyModel } from 'src/app/models/insurance.model';
import { ToastrService } from 'ngx-toastr';

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
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ]
})
export class AddInsurancePolicesComponent implements OnInit {
  @ViewChild('formDirective') formValues; // Added this
  public formAdd: FormGroup;
  risks = [];
  types = [];
  minDate = new Date();
  filteredPolicyTypes: Observable<any[]>;
  filteredRiskTypes: Observable<any[]>;
  editData: any = {};
  url = '';
  isLoading = false;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder,
    public dataProvider: DataProvider,
    public router: Router,
    public translate: TranslateService,
    public baseService: BaseService,
    public toastr: ToastrService) {
    this.url = baseUrl;
  }

  ngOnInit() {
    this.createForm();
  }

  backPage() {
    this.router.navigate(['list-insurance-policies']);

  }

  loadData() {

    forkJoin(
      this.baseService.get(`${this.url}api/PolicyTypes`, true),
      this.baseService.get(`${this.url}api/RiskTypes`, true),
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

      if (this.dataProvider.item) {
        this.setValues();
      }
    }, e => {
      if (e.status === 401) {
        this.toastr.error(this.translate.instant('error.sessionexpired'));
        this.router.navigate(['']);
        return;
      }
      this.toastr.error(this.translate.instant('error.common'));

    });
  }

  add() {
    this.isLoading = true;

    const body: PolicyModel = {
      Name: this.f.name.value,
      Description: this.f.description.value,
      PolicyTypeId: this.f.policytype.value.id,
      EffectiveDate: this.f.date.value,
      Terms: this.f.terms.value,
      Cost: this.f.cost.value,
      RiskTypeId: this.f.risktype.value.id,
    };

    let meth: any;

    if (this.dataProvider.item) {
      body.PoliceId = this.editData.policeId;
      meth = this.baseService.put(body, `${this.url}api/Policies`, true);
    } else {
      meth = this.baseService.post(body, `${this.url}api/Policies`, true);
    }

    meth.subscribe((r: any) => {

      this.toastr.success(this.translate.instant('common.saved'));
      this.formValues.resetForm();
      this.formAdd.reset();
      this.isLoading = false;

    }, (e: any) => {

      if (e.status === 401) {
        this.toastr.error(this.translate.instant('error.sessionexpired'));
        this.router.navigate(['']);
        return;
      }


      let error: string;
      let message: string;
      if (e.error && e.error.error_code) {
        error = this.translate.instant(`error.${e.error.error_code}`);
        message = this.translate.instant(`common.coverage`) + ' ' + e.error.error_message;
      }

      if (error === `error.${e.error.error_code}`) {
        error = this.translate.instant(`error.common`);
      }

      this.isLoading = false;

      this.toastr.error(`${error} ${message ? message : ''}`);
    });

  }

  createForm() {
    this.formAdd = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      policytype: ['', Validators.required],
      date: ['', Validators.required],
      terms: ['', Validators.compose([Validators.required, Validators.pattern(/^[0-9]*$/)])],
      cost: ['', Validators.compose([Validators.required, Validators.pattern(/^[0-9]*$/)])],
      risktype: ['', Validators.required]
    });

    this.loadData();
  }

  setValues() {

    this.editData = {};

    const item = this.dataProvider.item;
    this.editData.policeId = item.policeId;
    this.editData.policyType = this.types.find((e: any) => e.id === item.policyTypeId);
    this.editData.riskType = this.risks.find((e: any) => e.id === item.riskTypeId);

    this.f.date.setValue(new Date(item.effectiveDate));

    this.f.name.setValue(item.name);
    this.f.description.setValue(item.description);
    this.f.policytype.setValue(this.editData.policyType);
    this.f.terms.setValue(item.terms);
    this.f.cost.setValue(item.cost);
    this.f.risktype.setValue(this.editData.riskType);



  }

  get f() { return this.formAdd.controls; }


  private _filterRisks(value: any): any[] {
    if (value && value.id) {
      return this.risks.filter(option => option.id === value.id);
    } else {
      const filterValue = value ? value.toLowerCase() : '';
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
      const filterValue = value ? value.toLowerCase() : '';
      return this.types.map((item) => {
        item.complete = `${item.id} - ${item.name} - ${item.coverage}%`;
        return item;
      }).filter(option => option.name.toLowerCase().includes(filterValue)).slice(0, 20);
    }
  }

  public displayFn(e?: any): string | undefined {
    return e ? `${e.id} - ${e.name}${e.coverage ? ' - ' + e.coverage + '%' : ''}` : undefined;
  }
}
