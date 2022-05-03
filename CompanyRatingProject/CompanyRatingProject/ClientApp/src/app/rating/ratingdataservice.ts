import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { CompanyCreateDTO } from '../DTO/CompanyCreateDTO';
import {Observable} from "rxjs";
import {CompanyDTO} from "../DTO/CompanyDTO";
import {CompanyIdDTO} from "../DTO/CompanyIdDto";
import {RatingDTO} from "../DTO/RatingDTO";

@Injectable()
export class RatingDataService{
    private url = 'Rating/';
    constructor(private http:HttpClient) {
    }
    EditRating(id):Observable<any>{
        return this.http.get(this.url+'EditRating/'+id);
    }
    EditCompanyChange(body:RatingDTO){
        debugger;
        return this.http.post<RatingDTO>(this.url+'EditSubmit',body)
    }
}