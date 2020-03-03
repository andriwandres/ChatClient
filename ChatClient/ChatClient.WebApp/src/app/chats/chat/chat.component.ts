import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  targetId: number;
  isGroupChat: boolean;

  contactName: string;

  constructor(private readonly activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.targetId = +this.activatedRoute.snapshot.params['id'];
    this.isGroupChat = this.activatedRoute.snapshot.data['isGroupChat'];
  }
}
