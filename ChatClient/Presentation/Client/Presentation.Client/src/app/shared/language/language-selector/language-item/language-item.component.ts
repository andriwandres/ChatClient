import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-language-item',
  templateUrl: './language-item.component.html',
  styleUrls: ['./language-item.component.scss']
})
export class LanguageItemComponent {
  @Input() language!: string;
  @Input() isSelected!: boolean;

  @Output() selectLanguage = new EventEmitter<void>();

  onSelectLanguage(): void {
    this.selectLanguage.emit();
  }
}
