import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-slogan',
  templateUrl: './slogan.component.html',
  styleUrls: ['./slogan.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SloganComponent {}
