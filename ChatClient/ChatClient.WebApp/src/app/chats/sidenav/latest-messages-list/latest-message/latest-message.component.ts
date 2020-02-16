import { ChangeDetectionStrategy, Component, HostBinding, Input } from '@angular/core';
import { LatestMessage } from 'src/models/messages/latest-message';

@Component({
  selector: 'app-latest-message',
  templateUrl: './latest-message.component.html',
  styleUrls: ['./latest-message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LatestMessageComponent {
  @Input() latestMessage: LatestMessage;
  @Input() isActive = false;

  @HostBinding() tabindex = 0;
}
