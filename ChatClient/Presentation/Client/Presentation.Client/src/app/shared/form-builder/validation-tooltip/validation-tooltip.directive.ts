import { Overlay, OverlayPositionBuilder, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { ComponentRef, Directive, ElementRef, HostListener, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { RuleMappings } from './mapping';
import { ValidationTooltipComponent } from './validation-tooltip.component';

@Directive({
  selector: '[appValidationTooltip]'
})
export class ValidationTooltipDirective implements OnInit, OnChanges, OnDestroy {
  @Input() validationErrors!: ValidationErrors | null;
  @Input() validationRuleMappings!: RuleMappings;

  private overlayRef!: OverlayRef;
  private componentRef!: ComponentRef<ValidationTooltipComponent>;

  constructor(
    private readonly overlay: Overlay,
    private readonly positionBuilder: OverlayPositionBuilder,
    private readonly elementRef: ElementRef
  ) { }

  ngOnChanges(): void {
    if (this.componentRef) {
      this.componentRef.instance.errors = this.validationErrors || {};
      this.componentRef.instance.ruleMappings = this.validationRuleMappings;
    }
  }

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

  ngOnDestroy(): void {
    this.closeTooltip();
  }

  @HostListener('blur')
  onBlur(): void {
    this.closeTooltip();
  }

  @HostListener('focus')
  onFocus(): void {
    if (this.overlayRef && !this.overlayRef.hasAttached()) {
      this.componentRef = this.overlayRef.attach(new ComponentPortal(ValidationTooltipComponent));

      this.ngOnChanges();
    }
  }

  private closeTooltip(): void {
    if (this.overlayRef) {
      this.overlayRef.detach();
    }
  }
}
