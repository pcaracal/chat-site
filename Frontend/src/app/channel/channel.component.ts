import {Component, OnInit, OnDestroy, ViewChild, ElementRef} from '@angular/core';
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
  // @ts-ignore
  @ViewChild('messageContainer') private _MessageContainer: ElementRef;

  scrollToBottom(): void {
    try {
      setTimeout(() => {
        this._MessageContainer.nativeElement.scrollTop = this._MessageContainer.nativeElement.scrollHeight;
      }, 500);
    } catch (err) {
    }
  }


  channelId: number = -1;
  channelName: string = "";
  isMessageScreen: boolean = true;
  messages: { id: number, text: string, created_at: string, fk_channel_id: number, fk_user_id: number }[] = [];
  newMessage: string = "";
  isAdmin: boolean = false;
  newUsername: string = "";
  users: Map<number, string> = new Map<number, string>();
  isEditingChannelName: boolean = false;
  newChannelName: string = "";

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

    this._apiService.userGet().subscribe({
      next: (response: any) => {
        response.forEach((user: any) => {
          this.users.set(user.id, user.username);
        });
        console.log("Cat", this.users);
      }, error: (error: any) => {
        console.log("User get failed", error);
      }
    });

    this.getChannelData();
    interval(500) // TODO: Replace this junk with a websocket
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
    const oldMessages = this.messages;
    this._apiService.channelGet(this.channelId).subscribe({
      next: (response: any) => {
        // console.log("Channel data get successful", response);
        this.channelName = response.channelName;
        this.messages = response.messages;
        this.isAdmin = response.isAdmin;
        if (oldMessages.length !== this.messages.length) {
          this.scrollToBottom();
        }
      }, error: (error: any) => {
        console.log("Channel data get failed", error);
      }
    });
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
        this._toastr.success("User added", "User add successful");
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
        this.scrollToBottom();
        this.getChannelData();
        this.newMessage = "";
      }, error: (error: any) => {
        console.log("Message post failed", error);
        this._toastr.error("Message post failed", "Unknown error");
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

  handleChannelNameKeyUp(event: any) {
    this.newChannelName = event.target.value.trim() || "";
  }

  handleChannelNameSubmit(event: any) {
    event.preventDefault();
    if (!this.newChannelName) return;
    if (this.newChannelName === this.channelName) {
      this.isEditingChannelName = false;
      return;
    }

    this._apiService.channelPatch(this.channelId, this.newChannelName).subscribe({
      next: (response: any) => {
        console.log("Channel name patch successful");
        this.getChannelData();
        this.isEditingChannelName = false;
        this._toastr.success("Channel name changed", "Success");
      },
      error: (error: any) => {
        this._toastr.error("Channel name already exists", "Error");
      }
    });
  }

  handleChannelNameClick(event: any) {
    event.preventDefault();
    this.newChannelName = this.channelName;
    this.isEditingChannelName = true;
  }

  handleChannelNameCancel(event: any) {
    event.preventDefault();
    this.newChannelName = this.channelName;
    this.isEditingChannelName = false;
  }
}
