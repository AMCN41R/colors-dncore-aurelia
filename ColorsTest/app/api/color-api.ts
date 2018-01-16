import { HttpClient, json } from "aurelia-fetch-client";
import { autoinject } from "aurelia-framework";

@autoinject
export class ColorApi {
    constructor(private httpClient: HttpClient) { }

    async getColors(): Promise<IColor[]> {
        var response = await this.httpClient.fetch("api/colors");
        return await response.json();
    }

    async addColor(name: string): Promise<void> {
        await this.httpClient.fetch(`api/colors/${name}`, {
            method: "POST"
        })
    }

    async canDeleteColor(id: number): Promise<boolean> {
        var response = await this.httpClient.fetch(`api/colors/${id}/can-delete`);
        return await response.json();
    }

    async deleteColor(id: number): Promise<boolean> {
        var response = await this.httpClient.fetch(`api/colors/${id}`, {
            method: "DELETE"
        });

        return response.ok;
    }
}

export interface IColor {
    id: number;
    name: string;
    isEnabled: boolean;
}
