import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';


export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) { }

  ShowSpinner(spinnerType: SpinnerType){
    this.spinner.show(spinnerType);

    setTimeout(()=>this.HideSpinner(spinnerType), 3000);
  }
  HideSpinner(spinnerType: SpinnerType){
    this.spinner.hide(spinnerType);
  }
}

export enum SpinnerType{
  ball_spin_clockwise_fade_rotating = "s1",
  ball_climbing_dot = "s2",
  ball_atom = "s3"
}
