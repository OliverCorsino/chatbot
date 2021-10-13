import { HttpErrorResponse } from '@angular/common/http';
import { EventEmitter, Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { tokenGetter } from '../functions/token-getter';
import { ConnectedUser } from '../models/connected-user';
import { ChatMessage } from './../models/chat-message';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MessengerService {
  private connection: signalR.HubConnection;
  connectedUsers = new EventEmitter<ConnectedUser[]>();
  currentMessages = new EventEmitter<ChatMessage[]>();
  newMessage = new EventEmitter<ChatMessage>();

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private authService: AuthService) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}hub/messenger`, { accessTokenFactory: tokenGetter })
      .build();

    this.startConnection();
  }

  sendNewMessage(message: ChatMessage) {
    return this.connection.invoke('SendMessage', message);
  }

  disconnectUser() {
    const username = this.authService.getCurrentUser().username;

    this.connection.invoke('DisconnectUser', username).then(() => {
      this.authService.logout();
    }).catch((error) => {
      console.log(error);
    });
  }

  private startConnection() {
    this.connection.serverTimeoutInMilliseconds = 36000000;
    this.connection.keepAliveIntervalInMilliseconds = 1800000;

    this.connection.start().then(() => {
      this.getConnectedUsers();
      this.getCurrentMessages();
      this.getMessage();
    }).catch((error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  private getConnectedUsers() {
    this.connection.on('UpdateUsersConnected', (response: ConnectedUser[]) => {
      this.connectedUsers.emit(response);
    });
  }

  private getCurrentMessages() {
    this.connection.on('CurrentMessages', (messages: ChatMessage[]) => {
      this.currentMessages.emit(messages);
    });
  }

  private getMessage() {
    this.connection.on('NewMessage', (message: ChatMessage) => {
      this.newMessage.emit(message);
    });
  }
}
