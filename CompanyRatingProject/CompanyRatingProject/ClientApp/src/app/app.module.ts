import { BrowserModule } from '@angular/platform-browser';
import { NgModule }      from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppComponent }   from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";
import {HomeComponent} from "./home/home.component";
import {CompanyComponent} from "./company/company.component";
import {CompanyDeleteComponent} from "./company/delete/company.delete.component";
import {CompanyEditComponent} from "./company/edit/company.edit.component";
import {HomeLoginComponent} from "./home/login/home.login.component";
import {CompanyAddComponent} from "./company/add/company.add.component";
import {AuthGuard} from "./AuthGuard/AuthGuard";
import {RatingEditComponent} from "./rating/rating.edit.component";

@NgModule({
    
    declarations:
        [
            AppComponent,
            HomeComponent,
            CompanyComponent,
            CompanyDeleteComponent,
            CompanyEditComponent,
            CompanyAddComponent,
            NavMenuComponent,
            HomeLoginComponent,
            RatingEditComponent
            
        ],
    imports:
        [
            BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
            HttpClientModule,
            FormsModule,
            RouterModule.forRoot([
                {path: '', component: HomeComponent, pathMatch: 'full'},
                {path: 'company/:id', component: CompanyComponent},
                {path: 'deleteCompany/:id', component: CompanyDeleteComponent,canActivate:[AuthGuard]},
                {path: 'editCompany/:id', component: CompanyEditComponent,canActivate:[AuthGuard]},
                {path: 'login', component: HomeLoginComponent},
                {path:'addCompany',component:CompanyAddComponent,canActivate:[AuthGuard]},
                {path:'editRate/:id',component:RatingEditComponent}
            ]),
            ReactiveFormsModule
        ],
    providers:[AuthGuard],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }