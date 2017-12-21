import { HttpClient, json } from "aurelia-fetch-client";
import { autoinject } from "aurelia-framework";
import { IColor } from "./color-api";

@autoinject
export class PeopleApi {
    constructor(private httpClient: HttpClient) { }

    async getPeople(): Promise<IPerson[]> {
        var response = await this.httpClient.fetch("api/people");
        return await response.json();
    }

    async getPerson(id: number): Promise<IPerson> {
        var response = await this.httpClient.fetch(`api/people/${id}`);
        return await response.json();
    }

    async updatePerson(person: IPerson): Promise<void> {
        await this.httpClient.fetch(`api/people/${person.id}`, {
            method: "POST",
            body: json(person)
        });
    }
}

export interface IPerson {
    id: number;
    firstName: string;
    lastName: string;
    isAuthorised: boolean;
    isValid: boolean;
    isEnabled: boolean;
    fullName: string;
    isPalindrome: boolean;
    colors: IColor[];
}
