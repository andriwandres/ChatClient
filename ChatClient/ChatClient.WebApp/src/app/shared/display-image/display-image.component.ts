import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-display-image',
  templateUrl: './display-image.component.html',
  styleUrls: ['./display-image.component.scss']
})
export class DisplayImageComponent {
  @Input() image: Blob;
  @Input() isOnline = false;
  @Input() showOnlineIndicator: boolean;
}
