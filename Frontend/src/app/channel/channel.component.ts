import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../api.service';
import {interval, Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';
import {IndividualConfig, ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.scss']
})
export class ChannelComponent implements OnInit, OnDestroy {
  channelId: number = -1;
  channelName: string = "";
  isMessageScreen: boolean = true;
  messages: { id: number, text: string, created_at: string, fk_channel_id: number, fk_user_id: number }[] = [];
  newMessage: string = "";
  isAdmin: boolean = false;
  newUsername: string = "";

  private destroy$: Subject<void> = new Subject<void>();

  constructor(private route: ActivatedRoute, private router: Router, private _apiService: ApiService, private _toastr: ToastrService) {
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
    interval(10000) // VERY BAD
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getChannelData();
      });
  }

  ngOnDestroy(): void {
    // Unsubscribe and clean up to prevent memory leaks
    this.destroy$.next();
    this.destroy$.complete();
  }

  getChannelData() {
    this._apiService.channelGet(this.channelId).subscribe({
      next: (response: any) => {
        // console.log("Channel data get successful", response);
        this.channelName = response.channelName;
        this.messages = response.messages;
        this.isAdmin = response.isAdmin;
      }, error: (error: any) => {
        console.log("Channel data get failed", error);
      }
    });

    // TODO: 5. Display channel users
    // TODO: 6. Display channel settings if the user is an admin -- Done mostly
  }

  handleSendNewMessageKeyUp(event: any) {
    this.newMessage = event.target.value || "";
  }

  handleAddUserKeyUp(event: any) {
    this.newUsername = event.target.value || "";
  }

  handleAddUserSubmit(event: any) {
    event.preventDefault();
    if (!this.newUsername) return;
    this._apiService.channelPost(this.channelId, this.newUsername).subscribe({
      next: (response: any) => {
        console.log("User added successful");
        this.getChannelData();
        this.newUsername = "";
        this.isMessageScreen = true;
      }, error: (error: any) => {
        this._toastr.error("User doesn't exist", "User add failed");
        console.log("User add failed", error);
      }
    });
  }

  handleSendMessage(event: any) {
    if (!this.newMessage) return;
    this._apiService.messagePost(this.channelId, this.newMessage).subscribe({
      next: (response: any) => {
        console.log("Message post successful");
        this.messages.push(response);
        this.getChannelData();
        this.newMessage = "";
      }, error: (error: any) => {
        console.log("Message post failed", error);
      }
    });
  }

  handleBackClick(event: any) {
    event.preventDefault();
    this.router.navigate(["/overview"]);
  }

  returnToChannel(event: any) {
    event.preventDefault();
    this.isMessageScreen = true;
  }

  handleAddUserClick(event: any) {
    event.preventDefault();
    this.isMessageScreen = false;
  }
}
