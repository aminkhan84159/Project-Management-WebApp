import { Directive, ElementRef, HostListener, Input, Output, EventEmitter } from '@angular/core';

@Directive({
  selector: '[appClickedOutside]'
})
export class ClickedOutsideDirective {

  constructor(private elementRef: ElementRef) {}

  @Output() public cliclkedOutside = new EventEmitter();

  @HostListener('document:click', ['$event.target'])
  public onClick(target: any){

    const clilkInside = this.elementRef.nativeElement.contains(target);

    if(!clilkInside){
      this.cliclkedOutside.emit(target)
    }
  }

}
