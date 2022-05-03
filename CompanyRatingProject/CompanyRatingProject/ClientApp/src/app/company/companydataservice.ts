import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { CompanyCreateDTO } from '../DTO/CompanyCreateDTO';
import {Observable} from "rxjs";
import {CompanyDTO} from "../DTO/CompanyDTO";
import {CompanyIdDTO} from "../DTO/CompanyIdDto";
import {RatingDTO} from "../DTO/RatingDTO";

@Injectable()
export class CompanyDataService{
    private url = 'Company/';
    constructor(private http:HttpClient) {
    }
    GetCompanyById(id):Observable<any>{
        return this.http.get(this.url+'Get/'+id)
    }
    RateCompany(body:RatingDTO){
        
        return this.http.post(this.url+'Rate',body)
    }
    GetCompanyInfo(id):Observable<any>{
        return this.http.get(this.url+'GetInfo/'+id);
    }
    AddCompany(body:CompanyCreateDTO){
        return this.http.post<CompanyCreateDTO>(this.url+'Create',body)
    }
    EditCompanyChange(body:CompanyDTO){
        debugger;
        return this.http.post<CompanyDTO>(this.url+'EditChange',body)
    }
    DeleteCompany(id:CompanyIdDTO){
        debugger;
        return this.http.post<string>(this.url+'DeleteCompany',id)
    }
}