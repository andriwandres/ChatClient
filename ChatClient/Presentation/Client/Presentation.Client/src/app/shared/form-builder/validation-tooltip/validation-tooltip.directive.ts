import { Overlay, OverlayPositionBuilder, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Directive, ElementRef, HostListener, OnDestroy, OnInit } from '@angular/core';
import { ValidationTooltipComponent } from './validation-tooltip.component';

@Directive({
  selector: '[appValidationTooltip]'
})
export class ValidationTooltipDirective implements OnInit, OnDestroy {
  private overlayRef!: OverlayRef;

  constructor(
    private readonly overlay: Overlay,
    private readonly positionBuilder: OverlayPositionBuilder,
    private readonly elementRef: ElementRef
  ) { }

  ngOnInit(): void {
    const positionStrategy = this.positionBuilder.
      flexibleConnectedTo(this.elementRef)
        .withPositions([{
          originX: 'end',
          originY: 'center',
          overlayX: 'start',
          overlayY: 'center',
          offsetX: 20
        }]);

    this.overlayRef = this.overlay.create({ positionStrategy });
  }

  @HostListener('focus')
  onFocus(): void {
    if (this.overlayRef && !this.overlayRef.hasAttached()) {
      this.overlayRef.attach(new ComponentPortal(ValidationTooltipComponent));
    }
  }

  @HostListener('blur')
  onBlur(): void {
    this.closeTooltip();
  }

  ngOnDestroy(): void {
    this.closeTooltip();
  }

  private closeTooltip(): void {
    if (this.overlayRef) {
      this.overlayRef.detach();
    }
  }
}
