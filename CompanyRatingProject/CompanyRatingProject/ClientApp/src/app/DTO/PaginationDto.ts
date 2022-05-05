import {CompanyDTO} from "./CompanyDTO";

export class PaginationDto {
    constructor(
        public companies?:CompanyDTO[],
        public hasNextPage?:boolean,
        public hasPrevPage?:boolean,
        public pageIndex?:number,
        public totalPages?:number,
        public pageSize?:number,
        public top?:string,
        public searchString?:string
    ) {
        
    }
}