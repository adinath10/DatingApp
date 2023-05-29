import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})

// Control Value Accessor interface gives us the power to leverage the Angular forms API, and 
// create a connection between it and the DOM element. 
// The major benefits we gain from doing that, is that 
// we get all the default validations you'd get with any element, in order to track the validity, and it's value.
export class DateInputComponent implements ControlValueAccessor {

  @Input() label!: string;
  @Input() maxDate : Date | undefined;
  bsConfig : Partial<BsDatepickerConfig> | undefined;

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMMM YYYY'
    }
   }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}

  ngOnInit(): void {
  }

  get control(): FormControl{
    return this.ngControl.control as FormControl;
  }

}
