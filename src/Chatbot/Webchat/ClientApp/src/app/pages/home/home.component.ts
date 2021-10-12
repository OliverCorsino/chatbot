import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatRoom } from '../../models/chat-room';
import { ChatroomService } from './../../services/chatroom.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  chatRooms: ChatRoom[] = [];

  constructor(private chatroomService: ChatroomService, private router: Router) { }

  ngOnInit(): void {
    this.chatroomService.getAll().subscribe((result: any) => {
      this.chatRooms = result;
    }, (error) => {
      console.log(error);
    });
  }

  joinChatRoom(chatroomId: string): void {
    this.router.navigate([`chat-rooms/${btoa(chatroomId)}`]);
  }
}
