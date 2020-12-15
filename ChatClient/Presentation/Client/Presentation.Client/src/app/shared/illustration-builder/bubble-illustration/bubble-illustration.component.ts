import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-bubble-illustration',
  templateUrl: './bubble-illustration.component.html',
  styleUrls: ['./bubble-illustration.component.scss']
})
export class BubbleIllustrationComponent {
  @Input() avatar!: string;
  @Input() reactions: string[] = [];
  @Input() side: 'left' | 'right' = 'left';
}
