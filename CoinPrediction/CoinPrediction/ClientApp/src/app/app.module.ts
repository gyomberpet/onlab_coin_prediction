import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { MatTableModule } from '@angular/material/table';
import { CoinsListComponent } from './components/coins-list/coins-list.component';
import { MatIconModule } from '@angular/material/icon';
import { AccountComponent } from './components/account/account.component';
import { CoinComponent } from './components/coin/coin.component';
import { CoinChartComponent } from './components/coin-chart/coin-chart.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from "@angular/material/dialog";
import { SimulationEditComponent } from './components/simulation-edit/simulation-edit.component';
import { MatInputModule } from '@angular/material/input';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    CoinsListComponent,
    AccountComponent,
    CoinComponent,
    CoinChartComponent,
    SimulationEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    MatTableModule,
    MatIconModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    MatButtonToggleModule,
    MatFormFieldModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'coins-list', component: CoinsListComponent },
      { path: 'coin/:id', component: CoinComponent },
      { path: 'account', component: AccountComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
