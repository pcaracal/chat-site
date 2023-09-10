import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.scss']
})
export class ChannelComponent implements OnInit {
  channelId: number = -1;

  constructor(private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    const isLoggedIn = sessionStorage.getItem("Authorization") !== null;
    if (!isLoggedIn) {
      this.router.navigate(["/"]);
    }

    this.route.params.subscribe(params => {
      this.channelId = params['id'];
    });
  }

  getChannelData() {
    // TODO: 1. See if channel exists and user has access to it
    // TODO: 2. Get channel data if yes, else redirect to overview
    // TODO: 3. Display channel data
    // TODO: 4. Display channel messages
    // TODO: 5. Display channel users
    // TODO: 6. Display channel settings if user is admin
  }
}
