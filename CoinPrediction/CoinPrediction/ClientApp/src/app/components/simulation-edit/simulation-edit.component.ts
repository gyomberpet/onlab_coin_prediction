import { Component, Inject, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BalanceService } from '../../../services/balance.service';

@Component({
  selector: 'app-simulation-edit',
  templateUrl: './simulation-edit.component.html',
  styleUrls: ['./simulation-edit.component.css']
})
export class SimulationEditComponent implements OnInit {

  form: FormGroup;
  intervals: string[] = [
    "1 day",
    "1 hour",
    "1 minute",
  ];
  balance = 0;

  constructor(private fb: FormBuilder,
    public dialogRef: MatDialogRef<SimulationEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private balanceService: BalanceService
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      range: this.fb.group({
        start: ['', Validators.required],
        end: ['', Validators.required],
      }),
      frequency: ['', Validators.required],
      trainTestSplit: ['', [Validators.required, Validators.min(5), Validators.max(95)]],
      inputMoney: ['', [Validators.required, Validators.min(15), Validators.max(this.balance)]],
    });
    this.balanceService.getBalance(1).subscribe(balance => {
      this.balance = balance;
      this.inputMoney.setValidators([Validators.required, Validators.min(15), Validators.max(this.balance)]);
    }, error => console.log(error)
    );
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  formatLabel(value: number) {
    return value + '%';
  }

  get range() {
    return this.form['controls'].range as FormGroup;
  }

  get inputMoney() {
    return this.form['controls'].inputMoney as FormControl;
  }

  get frequency() {
    return this.form['controls'].frequency as FormControl;
  }

  get trainTestSplit() {
    return this.form['controls'].trainTestSplit as FormControl;
  }
}
