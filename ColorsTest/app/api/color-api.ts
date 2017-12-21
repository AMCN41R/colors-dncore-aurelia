import { HttpClient, json } from "aurelia-fetch-client";
import { autoinject } from "aurelia-framework";

@autoinject
export class ColorApi {
    constructor(private httpClient: HttpClient) { }

    async getColors(): Promise<IColor[]> {
        var response = await this.httpClient.fetch("api/colors");
        return await response.json();
    }
}

export interface IColor {
    id: number;
    name: string;
    isEnabled: boolean;
}
