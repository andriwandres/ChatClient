import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';
import { ChatMessagesStoreActions } from 'src/app/app-store/chat-messages-store';
import { ChatMessageDto } from 'src/models/messages/chat-message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  targetId: number;
  isGroupChat: boolean;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly store$: Store<AppStoreState.State>,
  ) { }

  ngOnInit(): void {
    this.targetId = +this.activatedRoute.snapshot.params['id'];
    this.isGroupChat = this.activatedRoute.snapshot.data['isGroupChat'];
  }

  onSendMessage(messageStr: string): void {
    const message: ChatMessageDto = {
      message: messageStr,
      targetId: this.targetId,
    };

    const action = this.isGroupChat
      ? ChatMessagesStoreActions.addGroupMessage({ message })
      : ChatMessagesStoreActions.addPrivateMessage({ message });

    this.store$.dispatch(action);
  }
}
