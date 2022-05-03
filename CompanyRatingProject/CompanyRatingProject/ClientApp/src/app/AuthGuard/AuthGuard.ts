import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import {Subject} from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate {
    private _authChangeSub= new Subject<boolean>();
    public _authChanged=this._authChangeSub.asObservable();
    constructor(private router: Router) { }
    
    public sendAuthStateChange =(isAuth:boolean)=>{this._authChangeSub.next(isAuth);debugger;};
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this._authChanged) {
            
            return true;
        }
        
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}