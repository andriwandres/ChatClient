import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.scss']
})
export class ChatMessagesComponent implements OnInit, OnDestroy {

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {

  }
}
