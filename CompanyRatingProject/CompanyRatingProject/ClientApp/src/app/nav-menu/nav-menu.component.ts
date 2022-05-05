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
  constructor(private _service:AuthGuard) {
  }
  ngOnInit():void{
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  readSessionStorageValue(key) {
    return sessionStorage.getItem(key);
  }
}
