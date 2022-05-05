import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
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
        var params= {top:top,searchString:searchString,pageNumber:pageNumber}
            //if(top!=null){params.set(top,top)}
            //if(searchString!=null){params.set(searchString,searchString)}
            //if(pageNumber!=null){params.set(pageNumber,pageNumber)}
            //debugger;
        return this.http.get(this.url+'Get',{params:params});
        if (top==null&&searchString==null&&pageNumber==null)
        {
            return this.http.get(this.url+'Get');
        }
        else 
        {
            if (top!=null)
            {
                if (pageNumber!=null)
                {
                    return this.http.get(this.url+'Get?top='+top+'&pageNumber='+pageNumber);
                }
                else return this.http.get(this.url+'Get?top='+top);
            }
            else
            {
                if (searchString!=null)
                {
                    if (pageNumber!=null)
                    {
                        return this.http.get(this.url+'Get?searchString='+searchString+'&pageNumber='+pageNumber);
                    }
                    else return this.http.get(this.url + 'Get?searchString='+searchString);
                }
                else return this.http.get(this.url+'Get?pageNumber='+pageNumber);
            }
        }
    }
    Login(body:UserForLogin)
    {
        return this.http.post<AuthResponse>(this.url+'login',body); 
    }
}