import {RatingDTO} from "./RatingDTO";

export class CompanyDTO{
    constructor(
        public id?:string,
        public name?:string,
        public description?:string,
        public grade1Avg?:number,
        public grade2Avg?:number,
        public grade3Avg?:number,
        public grade4Avg?:number,
        public grade5Avg?:number,
        public totalAvg?:number,
        public ratings?:RatingDTO[]
    ) {}
}