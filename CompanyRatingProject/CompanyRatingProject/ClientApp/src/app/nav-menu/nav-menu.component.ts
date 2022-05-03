import { Component } from '@angular/core';
import {HomeDataService} from "../home/homedataservice";
import {AuthGuard} from "../AuthGuard/AuthGuard";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers:[AuthGuard]
})
export class NavMenuComponent {
  isExpanded = false;
  Auth:boolean;
  constructor(private _service:AuthGuard) {
  }
  ngOnInit():void{
    this._service._authChanged.subscribe(res=>{this.Auth=res;})
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
