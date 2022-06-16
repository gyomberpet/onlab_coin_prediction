import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { IUser } from '../../../models/user';
import { BalanceService } from '../../../services/balance.service';
import { CoinGeckoService } from '../../../services/coin-gecko.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  userId = 1;

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  user: IUser;
  fee = 1.5;
  balance = 0;

  constructor(private fb: FormBuilder, private userService: UserService,
    private coinGeckoService: CoinGeckoService, private balanceService: BalanceService) { }

  /**
   * Build the form.
   */
  ngOnInit() {
    this.firstFormGroup = this.fb.group({
      inputMoney: ['', Validators.required],
    });
  }

  /**
   * Add the given value to the user's balance.
   * @param value
   */
  depositConfirm(value: number) {
    this.userService.addAssetToUser(this.userId, {
      coin: {
        coinId: "tether"
      },
      amount: value
    }).subscribe(result => {
      this.balanceService.getBalance(1).subscribe(balance =>
        this.balance = balance,
        error => console.log(error)
      );
    }), error => console.log(error);
  }

  get inputMoney() {
    return this.firstFormGroup['controls'].inputMoney as FormControl;
  }

}
