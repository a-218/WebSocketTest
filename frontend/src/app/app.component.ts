import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FormControl, ReactiveFormsModule } from "@angular/forms";
import { BaseDto, ServerEchoClientDto } from '../BaseDto';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';


  messages: string[] =[];

  ws = new WebSocket("ws://localhost:8181");

  messageContent = new FormControl('');

  constructor() {
    this.ws.onmessage = message => {

      // evaluate incoming event
      this.ws.onmessage = message => {
        const messageFromServer = JSON.parse(message.data) as BaseDto<any>;
        //@ts-ignore
        this[messageFromServer.eventType].call(this, messageFromServer);
      }
      this.messages.push(message.data)
    }
  }

  ServerEchoClient(dto: ServerEchoClientDto) {
    this.messages.push(dto.echoValue!);
  }

  sendMessage() {
    var object = {
      eventType: "ClientEchoServer",
      messageContent: this.messageContent.value!
    }
    this.ws.send(JSON.stringify(object));
  }

}



