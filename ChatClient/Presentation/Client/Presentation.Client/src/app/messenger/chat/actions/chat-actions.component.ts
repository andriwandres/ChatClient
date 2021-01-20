import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output
} from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-chat-actions',
  templateUrl: './chat-actions.component.html',
  styleUrls: ['./chat-actions.component.scss'],
})
export class ChatActionsComponent implements OnInit, OnDestroy {
  @Output() typingStart = new EventEmitter();
  @Output() typingStop = new EventEmitter();

  @Output() messageSent = new EventEmitter<string>();

  readonly messageControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/^.*[^\s].*$/)
  ]);

  private readonly destroy$ = new Subject();

  ngOnInit(): void {
    this.messageControl.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe((value: string) => {
        value ? this.typingStart.emit() : this.typingStop.emit();
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit(): void {
    this.messageSent.emit(this.messageControl.value);
  }
}
