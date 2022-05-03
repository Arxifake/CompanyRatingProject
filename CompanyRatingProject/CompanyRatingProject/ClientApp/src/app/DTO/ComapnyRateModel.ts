import {CompanyDTO} from "./CompanyDTO";
import {RatingDTO} from "./RatingDTO";

export class CompanyRateModel{
    constructor(
        public company:CompanyDTO,
        public rating:RatingDTO
    ) {}
}