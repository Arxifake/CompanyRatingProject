import {Component, OnInit} from '@angular/core';
import { CompanyDataService } from '../companydataservice';
import { Router, ActivatedRoute } from '@angular/router';
import {CompanyDTO} from "../../DTO/CompanyDTO";
import {FormControl, FormGroup, Validators} from "@angular/forms";


@Component({
    selector: 'app-company-edit',
    templateUrl:'company.edit.component.html',
    providers:[CompanyDataService]
})
export class CompanyEditComponent implements OnInit{
    id:string;
    company:CompanyDTO;
    public editForm:FormGroup;
    private returnUrl:'';
    
    constructor(private route: ActivatedRoute,
                private router: Router
                ,private companyDataService:CompanyDataService)
    {}

    ngOnInit() {
        this.route.paramMap.subscribe(params=>this.id=params.get('id'));
        this.initEditCompany();
        this.returnUrl=this.route.snapshot.queryParams['returnUrl']||'/';
    }
    
    initEditCompany(){
        this.editForm=new FormGroup({
            id: new FormControl('',[Validators.required]),
            name: new FormControl('',[Validators.required]),
            description: new FormControl('',[Validators.required])
        })
        this.companyDataService.GetCompanyInfo(this.id).subscribe((data:CompanyDTO)=>{
            this.company=data;
            for (let controlsKey in this.editForm.controls) {
                this.editForm.get(controlsKey).setValue(data[controlsKey]);
            }
        });
    }
    public editCompanyChange=(companyValue)=>{
        const company ={...companyValue};
        const newCompany: CompanyDTO={
            id: company.id,
            name:company.name,
            description:company.description
        }
        this.companyDataService.EditCompanyChange(newCompany).subscribe(x=>this.router.navigate([this.returnUrl]));
        
    }
}