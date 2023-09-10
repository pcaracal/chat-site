import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../api.service';
import {interval, Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';

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

  private destroy$: Subject<void> = new Subject<void>();

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
    interval(500)
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
      }, error: (error: any) => {
        console.log("Channel data get failed", error);
      }
    });

    // TODO: 5. Display channel users
    // TODO: 6. Display channel settings if the user is an admin
  }

  handleSendNewMessageKeyUp(event: any) {
    this.newMessage = event.target.value || "";
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
}
