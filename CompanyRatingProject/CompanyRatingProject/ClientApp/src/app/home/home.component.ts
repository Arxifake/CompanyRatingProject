import {Component, OnInit} from '@angular/core';
import { HomeDataService } from './homedataservice';
import { CompanyDTO } from '../DTO/CompanyDTO';

@Component({
    selector: 'app-home',
    templateUrl:'home.component.html',
    providers:[HomeDataService]
})
export class HomeComponent implements OnInit{
    companies: CompanyDTO[];
    pageNumber:number;
    search:string;
    tableMode:boolean=true;
    
    constructor(private homedataservice:HomeDataService) {
    }
    ngOnInit() {
       this.loadCompanies(null,null);
    }
    loadCompanies(top:string,searchString:string){
        this.homedataservice.Get(top,searchString,this.pageNumber)
            .subscribe((data:CompanyDTO[])=>this.companies=data);
    }
}