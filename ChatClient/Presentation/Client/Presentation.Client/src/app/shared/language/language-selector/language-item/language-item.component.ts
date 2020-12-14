import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Language } from '@chat-client/core/models';

@Component({
  selector: 'app-language-item',
  templateUrl: './language-item.component.html',
  styleUrls: ['./language-item.component.scss']
})
export class LanguageItemComponent {
  @Input() language!: Language;
  @Input() isSelected!: boolean;

  @Output() selectLanguage = new EventEmitter<void>();
}
