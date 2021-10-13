import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ChatMessage } from './../../models/chat-message';
import { ConnectedUser } from './../../models/connected-user';
import { AuthService } from './../../services/auth.service';
import { MessengerService } from './../../services/messenger.service';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.css']
})
export class MessengerComponent implements OnInit {

  @ViewChild('chat', { static: false }) private chatElement: ElementRef;
  users: ConnectedUser[] = [];
  connectedUsersSubscription: Subscription;
  currentMessages: ChatMessage[] = [];
  currentMessagesSubscription: Subscription;
  newMessageSubscription: Subscription;
  currentUsername: string;
  message = new FormControl('');

  @HostListener('window:unload', ['$event']) unloadHandler(event) {
    this.disconnectUser();
  }

  disconnectUser() {
    this.connectedUsersSubscription.unsubscribe();
    this.currentMessagesSubscription.unsubscribe();
    this.messengerService.disconnectUser();
  }

  constructor(
    private messengerService: MessengerService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['sign-in']);
    }

    this.currentUsername = this.authService.getCurrentUser().username;
    this.suscribeToEvents();
  }

  suscribeToEvents() {
    this.connectedUsersSubscription = this.messengerService.connectedUsers.subscribe((connectedUsers: ConnectedUser[]) => {
      if (connectedUsers !== undefined) {
        this.users = connectedUsers;
      }
    });

    this.currentMessagesSubscription = this.messengerService.currentMessages.subscribe((currentMessages: ChatMessage[]) => {
      if (currentMessages !== undefined) {
        this.currentMessages = currentMessages;
        this.chatScrollToBottom();
      }
    });

    this.newMessageSubscription = this.messengerService.newMessage.subscribe((newMessage: ChatMessage) => {
      if (newMessage !== undefined) {
        this.currentMessages.push(newMessage);
        this.chatScrollToBottom();
      }
    });
  }

  checkIsEnterKey(event) {
    const enterKeyCode = 13;
    if (event.keyCode === enterKeyCode) {
      this.send();
    }
  }

  send() {
    if (this.message.value.trim() === '') {
      return;
    }

    const newMessage: ChatMessage = {
      username: this.currentUsername,
      sentOnUtc: new Date(),
      message: this.message.value
    };

    this.messengerService.sendNewMessage(newMessage).then(() => {
      this.message.setValue('');
    }).catch((error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  getMessageStyleClassByUserName(username: string) {
    if (username === this.currentUsername) {
      return 'sent-message';
    } else {
      return 'incomming-message';
    }
  }

  private chatScrollToBottom() {
    setTimeout(() => {
      this.chatElement.nativeElement.scrollTop = this.chatElement.nativeElement.scrollHeight;
    }, 100);
  }
}
