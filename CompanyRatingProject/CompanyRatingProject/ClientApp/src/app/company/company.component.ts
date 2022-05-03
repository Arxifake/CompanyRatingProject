import {Component, OnInit} from '@angular/core';
import { CompanyDataService } from './companydataservice';
import { CompanyRateModel } from '../DTO/ComapnyRateModel';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {RatingDTO} from "../DTO/RatingDTO";


@Component({
    selector: 'app-company',
    templateUrl:'company.component.html',
    providers:[CompanyDataService]
})
export class CompanyComponent implements OnInit{
    id:string;
    companyrate:CompanyRateModel;
    rateCompany:FormGroup;
    
    constructor(private route: ActivatedRoute,
                private router: Router
                ,private companydataservice:CompanyDataService)
    {}
    ngOnInit() {
        this.route.paramMap.subscribe(params=>this.id=params.get('id'))
       this.loadCompany();
    }
    loadCompany(){
        this.rateCompany=new FormGroup({
            Grade1: new FormControl("",[Validators.required]),
            Grade2: new FormControl("",[Validators.required]),
            Grade3: new FormControl("",[Validators.required]),
            Grade4: new FormControl("",[Validators.required]),
            Grade5: new FormControl("",[Validators.required]),
            Text: new FormControl("",[Validators.required])
        })
        this.companydataservice.GetCompanyById(this.id)
            .subscribe((data:CompanyRateModel)=>this.companyrate=data);
        
        
    }
    public sendRateCompany=(rateValue)=>
    {
        const rate={...rateValue};
        const newRate:RatingDTO={
            grade1:rate.Grade1,
            grade2:rate.Grade2,
            grade3:rate.Grade3,
            grade4:rate.Grade4,
            grade5:rate.Grade5,
            text:rate.Text,
            companyId:this.companyrate.company.id
        }
        this.companydataservice.RateCompany(newRate).subscribe(x=>this.router.navigate(['company/'+this.id]));
    }
}