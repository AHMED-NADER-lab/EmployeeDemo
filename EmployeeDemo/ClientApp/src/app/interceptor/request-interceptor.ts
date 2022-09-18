import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Injectable } from '@angular/core';


import { Router } from '@angular/router';
@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,

  ) {
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
 var   headers = new HttpHeaders()
     
    if (!request.url.includes("Login") && !request.url.includes("Resgister")) {
      const token = localStorage.getItem("token");
      headers = new HttpHeaders()
        .set('content-type', 'application/json')
        .set('Access-Control-Allow-Origin', '*')
        .set('Authorization', `Bearer ${token}`);
    } else {
      headers = new HttpHeaders()
        .set('content-type', 'application/json')
        .set('Access-Control-Allow-Origin', '*');
    }
    const authReq = request.clone({
      headers: headers
    });
    return next.handle(authReq)
      .pipe(
        catchError((error) => {
          debugger;
          
            let errorMessage = '';
          
            if (error.status == 400)
              if (error.error && error.error.ExceptionType && error.error.ExceptionType == "Custom") {
                errorMessage = error.error.Message;
              } else {
                var decoder = new TextDecoder()
                const responseString = decoder.decode(error.error);
                const errorObject = JSON.parse(responseString);
                errorMessage = errorObject.Message;
            }
          if (error.status == 401) {

            localStorage.clear()
            this.router.navigate(["login"]);
            
            }
            if (errorMessage) {
              alert(errorMessage)
            }
           
            return throwError(error);
          

        })
      )
  }
}
