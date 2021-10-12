import { ChatRoom } from '../models/chat-room';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatroomService {

  private readonly chatRoomApiUrl = 'api/chat-rooms/';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll() {
    return this.http.get<ChatRoom[]>(`${this.baseUrl}${this.chatRoomApiUrl}`);
  }
}
