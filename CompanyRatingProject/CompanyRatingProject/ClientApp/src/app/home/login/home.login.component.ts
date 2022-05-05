import {Component, OnInit} from '@angular/core';
import { HomeDataService } from '../homedataservice';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {UserForLogin} from "../../DTO/UserForLogin";
import {AuthGuard} from "../../AuthGuard/AuthGuard";

@Component({
    selector: 'app-home-login',
    templateUrl:'home.login.component.html',
    providers:[HomeDataService,AuthGuard]
})
export class HomeLoginComponent implements OnInit {
    public loginForm: FormGroup;
    public errorMessage: string;
    public showError: boolean;
    private _returnUrl: '';

    constructor(private homedataservice: HomeDataService,private _router: Router,private _route:ActivatedRoute,private authGuard:AuthGuard) {
    }

    ngOnInit(): void {
        this.loginForm = new FormGroup({
            username: new FormControl("", [Validators.required]),
            password: new FormControl("", [Validators.required])
        })
        this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
    }
    public validateControl = (controlName: string) => {
        return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
    }
    public hasError = (controlName: string, errorName: string) => {
        return this.loginForm.controls[controlName].hasError(errorName)
    }
    public loginUser = (loginFormValue) => {
        this.showError = false;
        const login = {... loginFormValue };
        const userForAuth: UserForLogin = {
            login: login.username,
            password: login.password
        }
        this.homedataservice.Login(userForAuth).subscribe((res)=>{
            sessionStorage.setItem('username','test')
            this._router.navigate([this._returnUrl]);
            },
            (error:any) => {
                this.errorMessage = error.error;
                this.showError = true;})
    }
}