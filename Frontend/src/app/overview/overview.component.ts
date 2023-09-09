import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {ApiService} from "../api.service";

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  isLoggedIn: boolean = false;

  constructor(private apiService: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.isLoggedIn = sessionStorage.getItem("Authorization") !== null;
    if (!this.isLoggedIn) {
      this.router.navigate(["/"]);
    }
  }
}
