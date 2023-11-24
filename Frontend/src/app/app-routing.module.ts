import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {OverviewComponent} from "./overview/overview.component";
import {ChannelComponent} from "./channel/channel.component";

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'overview', component: OverviewComponent},
  {path: 'channel/:id', component: ChannelComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
