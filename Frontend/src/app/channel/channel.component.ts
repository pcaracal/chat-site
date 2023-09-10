import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ApiService} from "../api.service";

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.scss']
})
export class ChannelComponent implements OnInit {
  channelId: number = -1;
  channelName: string = "";
  isMessageScreen: boolean = true;
  messages = [
    {
      user: "cat1",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
    {
      user: "cat2",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
    {
      user: "cat3",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
    {
      user: "cat2",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
    {
      user: "cat2",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
    {
      user: "cat1",
      text: "Hello cat!",
      created_at: "2021-05-01 12:00:00"
    },
  ];

  constructor(private route: ActivatedRoute, private router: Router, private _apiService: ApiService) {
  }

  ngOnInit(): void {
    const isLoggedIn = sessionStorage.getItem("Authorization") !== null;
    if (!isLoggedIn) {
      this.router.navigate(["/"]);
    }

    this.route.params.subscribe(params => {
      this.channelId = params['id'];
    });

    this.getChannelData();
  }

  getChannelData() {
    this._apiService.channelGet(this.channelId).subscribe({
      next: (response: any) => {
        console.log("Channel data get successful", response);
        this.channelName = response.channelName;
      }, error: (error: any) => {
        console.log("Channel data get failed", error);
      }
    });


    // TODO: 1. See if channel exists and user has access to it
    // TODO: 2. Get channel data if yes, else redirect to overview
    // TODO: 3. Display channel data
    // TODO: 4. Display channel messages
    // TODO: 5. Display channel users
    // TODO: 6. Display channel settings if user is admin
  }
}
