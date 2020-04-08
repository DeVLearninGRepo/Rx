import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoggerService {

  constructor() { }

  public logComponentInitInfo(componentName: string, backgroundColor: string) {
    console.debug('%c%s', 'background-color: ' + backgroundColor + '; color: #ffffff; padding: 20px 20px', componentName);
  }

  public logDebug(message: any, backgroundColor: string) {
    console.debug('%c%s', 'background-color: ' + backgroundColor + '; color: #ffffff; padding: 10px 10px', message);
  }
  
}
