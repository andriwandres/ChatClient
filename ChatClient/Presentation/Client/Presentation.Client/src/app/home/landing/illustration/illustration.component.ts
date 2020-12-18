import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-illustration',
  templateUrl: './illustration.component.html',
  styleUrls: ['./illustration.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LandingIllustrationComponent {}
