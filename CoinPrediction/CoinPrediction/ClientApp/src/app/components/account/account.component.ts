import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { IUser } from '../../../models/user';
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
    private coinGeckoService: CoinGeckoService) { }

  ngOnInit() {
    this.firstFormGroup = this.fb.group({
      inputMoney: ['', Validators.required],
    });
    this.userService.getUserById(this.userId).subscribe(result => {
      this.user = result;
      var coinIds = this.user.userAssets.map(a => a.coin.coinId);
      this.coinGeckoService.getMarkets(Currency.USD, coinIds).subscribe(result => {
        this.calculateBalance(result);
      }), error => console.log(error);
    }), error => console.log(error);
  }

  calculateBalance(markets: ICoinMarket[]) {  
    this.user.userAssets.forEach(asset => {
      var coin: ICoinMarket | undefined = markets.find(m => m.id == "bitcoin");
      if (!coin) {
        coin = {
          id: '',
          symbol: '',
          name: '',
          currentPrice: 0
        };       
      }
      var price = coin.currentPrice;
      this.balance += price * asset.amount;
    })
    console.log(this.balance);
  }

  depositConfirm(value: number) {
    this.userService.addAssetToUser(this.userId, {
      coin: {
        coinId: "tether"
      },
      amount: value
    }).subscribe(result => { }), error => console.log(error);
  }

  get inputMoney() {
    return this.firstFormGroup['controls'].inputMoney as FormControl;
  }

}
