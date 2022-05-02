import { Component, Inject, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Frequency } from '../../../models/enums/frequency';
import { BalanceService } from '../../../services/balance.service';

@Component({
  selector: 'app-simulation-edit',
  templateUrl: './simulation-edit.component.html',
  styleUrls: ['./simulation-edit.component.css']
})
export class SimulationEditComponent implements OnInit {

  form: FormGroup;
  intervals: string[] = [
    "1 hour",
    "1 minute",
  ];
  balance = 0;
  maxDate = new Date(2022, 4, 18);
  minDate = new Date(this.maxDate.getFullYear() - 5, 0, 1);

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

  submit(): void {
    var frequency;
    switch (this.frequency.value) {
      case "1 hour": frequency = Frequency.HOURLY; break;
      case "1 minute": frequency = Frequency.MINTUTELY; break;
    }

    var result = {
      start: this.start.value,
      end: this.end.value,
      frequency: frequency,
      trainTestSplit: this.trainTestSplit.value,
      inputMoney: this.inputMoney.value
    };

    this.dialogRef.close(result);
  }

  formatLabel(value: number) {
    return value + '%';
  }

  get range() {
    return this.form['controls'].range as FormGroup;
  }

  get start() {
    return this.range['controls'].start as FormControl;
  }

  get end() {
    return this.range['controls'].end as FormControl;
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
