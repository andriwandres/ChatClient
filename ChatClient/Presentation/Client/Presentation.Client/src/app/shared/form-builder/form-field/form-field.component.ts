import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss']
})
export class FormFieldComponent implements OnInit {
  @Input() control!: FormControl;
  @Input() placeholder!: string;

  @Input() type = 'text';
  @Input() hint?: string;
  @Input() prefixIcon?: string;
  @Input() suffixIcon?: string;

  @Output() suffixClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void {

  }

}
