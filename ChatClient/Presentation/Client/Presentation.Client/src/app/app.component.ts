import { Component, OnInit } from '@angular/core';
import { HttpTransportType, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { RootStoreFacade } from './store/facade';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'chat-client';

  constructor(private readonly storeFacade: RootStoreFacade) {}

  ngOnInit(): void {
    this.storeFacade.createHub();
  }
}