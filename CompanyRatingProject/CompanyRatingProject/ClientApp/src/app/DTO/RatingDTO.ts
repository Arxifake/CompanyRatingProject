import {DatePipe} from "@angular/common";

export class RatingDTO{
    constructor(
        public id?:string,
        public grade1?:number,
        public grade2?:number,
        public grade3?:number,
        public grade4?:number,
        public grade5?:number,
        public total?:number,
        public text?:string,
        public dateTime?:Date,
        public companyId?:string,
        public userId?:string
    ) {}
}