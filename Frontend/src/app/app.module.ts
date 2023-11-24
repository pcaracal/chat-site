import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LoginComponent} from './login/login.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {OverviewComponent} from './overview/overview.component';
import {ChannelComponent} from './channel/channel.component';
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule, NoopAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OverviewComponent,
    ChannelComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
