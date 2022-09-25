import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from "@angular/router";


@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
 
    private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | Promise<boolean> {
    const token = localStorage.getItem("token");
    if (!token) { 
      this.router.navigate(["login"]);
    }
    return true;
  }
}


@Injectable()
export class LoginGuard implements CanActivate {
  constructor(

    private router: Router) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | Promise<boolean> {
    const token = localStorage.getItem("token");
    if (token) {
      this.router.navigate(["employee/list"]);
    }
    return true;
  }
}


