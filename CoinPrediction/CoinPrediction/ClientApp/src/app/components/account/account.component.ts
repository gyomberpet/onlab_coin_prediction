import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  fee = 1.5;
  balance = 0;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.firstFormGroup = this.fb.group({
      inputMoney: ['', Validators.required],
    });
    
  }

  depositConfirm(value: number) {
    this.balance += value;
  }

  get inputMoney() {
    return this.firstFormGroup['controls'].inputMoney as FormControl;
  }

}
