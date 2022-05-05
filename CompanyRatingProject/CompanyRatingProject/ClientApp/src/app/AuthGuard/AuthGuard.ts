import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router) { }
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean {
        console.log(sessionStorage)
            if (sessionStorage.getItem('username')) {
                return true
            }
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}