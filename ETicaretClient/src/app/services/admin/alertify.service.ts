import { Injectable } from '@angular/core';
declare var alertify : any

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }
  message(message: string, options: Partial<AlertifyOptions>){
    alertify.set('notifier','position', options.positon);
    alertify.set('notifier','delay', options.delay);
    const msg = alertify[options.messageType](message);
    if(options.dismissOthers)
      msg.dismissOthers();

  }
  dismiss(){
    alertify.dismissAll();
  }
}

export class AlertifyOptions{
  messageType: MessageType = MessageType.Success;
  positon: Position = Position.Top_center;
  delay: number = 3;
  dismissOthers: boolean = false;
}

export enum MessageType{
  Error = "error",
  Message = "message",
  Notify = "notify",
  Success = "success",
  Warnign = "warning"
}

export enum Position{
  Top_right = "top-right",
  Top_center = "top-center",
  Top_left = "top-left",
  Bottom_right = "bottom-right",
  Bottom_center = "bottom-center",
  Bottom_left = "bottom-left"
}