import {Component, OnInit} from '@angular/core';
import { CompanyDataService } from '../companydataservice';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {CompanyDTO} from "../../DTO/CompanyDTO";
import {CompanyCreateDTO} from "../../DTO/CompanyCreateDto";


@Component({
    selector: 'app-company-add',
    templateUrl:'company.add.component.html',
    providers:[CompanyDataService]
})
export class CompanyAddComponent implements OnInit{
    public addForm:FormGroup;
    private returnUrl:'';
    constructor(private route: ActivatedRoute,
                private router: Router
                ,private companydataservice:CompanyDataService)
    {}
    ngOnInit():void {
        this.addForm=new FormGroup({
            name: new FormControl("",[Validators.required]),
            desc: new FormControl("",[Validators.required])
        })
        this.returnUrl=this.route.snapshot.queryParams['returnUrl']||'/';
    }
    public validateControl = (controlName: string) => {
        return this.addForm.controls[controlName].invalid && this.addForm.controls[controlName].touched
    }
    public hasError = (controlName: string, errorName: string) => {
        return this.addForm.controls[controlName].hasError(errorName)
    }
    public addCompany=(companyValue)=>{
        const company ={...companyValue};
        const newCompany: CompanyCreateDTO={
            name:company.name,
            description:company.desc
        }
        this.companydataservice.AddCompany(newCompany).subscribe(x=>this.router.navigate([this.returnUrl]));
    }
}