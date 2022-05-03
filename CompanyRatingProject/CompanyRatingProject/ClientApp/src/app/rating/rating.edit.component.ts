import {Component, OnInit} from '@angular/core';
import { CompanyRateModel } from '../DTO/ComapnyRateModel';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {RatingDTO} from "../DTO/RatingDTO";
import {RatingDataService} from "./ratingdataservice";


@Component({
    selector: 'app-rating',
    templateUrl:'rating.component.html',
    providers:[RatingDataService]
})
export class RatingEditComponent implements OnInit{
    id:string;
    rate:RatingDTO;
    rateCompany:FormGroup;
    
    constructor(private route: ActivatedRoute,
                private router: Router
                ,private ratingdataservice:RatingDataService)
    {}
    ngOnInit() {
        this.route.paramMap.subscribe(params=>this.id=params.get('id'))
       this.loadRate();
    }
    loadRate(){
        this.rateCompany=new FormGroup({
            grade1: new FormControl("",[Validators.required]),
            grade2: new FormControl("",[Validators.required]),
            grade3: new FormControl("",[Validators.required]),
            grade4: new FormControl("",[Validators.required]),
            grade5: new FormControl("",[Validators.required]),
            text: new FormControl("",[Validators.required])
        })
        this.ratingdataservice.EditRating(this.id)
            .subscribe((data:RatingDTO)=>{
                this.rate=data;
                for (let controlsKey in this.rateCompany.controls) {
                    this.rateCompany.get(controlsKey).setValue(data[controlsKey]);
                    }
            }
            );
        
        
    }
    public ChangeRateCompany=(rateValue)=>
    {
        const rate={...rateValue};
        const newRate:RatingDTO={
            grade1:rate.grade1,
            grade2:rate.grade2,
            grade3:rate.grade3,
            grade4:rate.grade4,
            grade5:rate.grade5,
            text:rate.text,
            companyId:this.rate.companyId,
            id:this.rate.id,
            userId:this.rate.userId
        }
        this.ratingdataservice.EditCompanyChange(newRate).subscribe(x=>this.router.navigate(['company/'+this.rate.companyId]));
    }
}