import {Component, OnInit} from '@angular/core';
import { HomeDataService } from './homedataservice';
import {PaginationDto} from "../DTO/PaginationDto";

@Component({
    selector: 'app-home',
    templateUrl:'home.component.html',
    providers:[HomeDataService]
})
export class HomeComponent implements OnInit{
    page:PaginationDto;
    search:string;
    tableMode:boolean=true;
    
    constructor(private homedataservice:HomeDataService) {
    }
    ngOnInit() {
       this.loadCompanies('','',1);
    }
    loadCompanies(top:string,searchString:string,pageNumber:number){
        if (top==null)top='';
        if (searchString==null)searchString='';
        if (pageNumber==null)pageNumber=1;
        this.homedataservice.Get(top,searchString,pageNumber)
            .subscribe((data:PaginationDto)=>console.log(this.page=data));
    }
    readSessionStorageValue(key) {
        return sessionStorage.getItem(key);
    }
}