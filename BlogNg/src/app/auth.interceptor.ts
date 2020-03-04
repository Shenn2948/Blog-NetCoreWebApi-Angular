import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(req, next) {

    let token = JSON.parse(localStorage.getItem('token'));

    if (token == null) {
      token = '';
    }

    const authRequest = req.clone({

      headers: req.headers.set('Authorization', `Bearer ${token.token}`)
    });
    return next.handle(authRequest);
  }
}
