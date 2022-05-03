import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { UserForLogin } from '../DTO/UserForLogin';
import {Observable} from "rxjs";
import {Subject} from "rxjs";
import {AuthResponse} from "../DTO/AuthResponse";

@Injectable()
export class HomeDataService{
    private url = 'Home/';
    constructor(private http:HttpClient) {
    }
    Get(top,searchString,pageNumber):Observable<any>{
        if (searchString!=null){
            return this.http.get(this.url + 'Get?searchString='+searchString);
        }
        if (top==null&&pageNumber==null){
            return this.http.get(this.url+'Get');
        }
        if (top==null&&pageNumber!=null){
            return this.http.get(this.url+'Get?pageNumber'+pageNumber);
        }
        if (top!=null&&pageNumber==null){
            return this.http.get(this.url+'Get?top='+top);
        }
        else {
            return this.http.get(this.url+'Get?top='+top+'&pageNumber'+pageNumber);
        }
    }
    Login(body:UserForLogin)
    {
        return this.http.post<AuthResponse>(this.url+'login',body); 
    }
}