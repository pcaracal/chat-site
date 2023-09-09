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

  public channels?: { id: number, name: string, created_at: string, fk_admin_id: number }[];

  constructor(private apiService: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.isLoggedIn = sessionStorage.getItem("Authorization") !== null;
    if (!this.isLoggedIn) {
      this.router.navigate(["/"]);
    }
    this.getChannels();
  }

  getChannels() {
    this.apiService.overviewGet().subscribe({
      next: (response: any) => {
        console.log("Overview successful", response);
        this.channels = response.channels;
      },
      error: (error: any) => {
        console.log("Overview failed", error);
      }
    });
  }
}
