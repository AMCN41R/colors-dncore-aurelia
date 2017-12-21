import { autoinject } from "aurelia-framework";
import { PeopleApi, IPerson } from "../api/people-api";
import { AppRouter } from "../app-router";

@autoinject
export class List {
    constructor(
        private router: AppRouter,
        private peopleApi: PeopleApi
    ) { }

    private people: IPerson[];

    activate() {

        // not awaiting this call so that the page is loaded, 
        // and the data will load in the background
        this.fetchData();
    }

    private async fetchData(): Promise<void> {
        this.people = await this.peopleApi.getPeople();
    }

    private formatColours(person: IPerson): string {
        return person.colors.map(x => x.name).join(", ");
    }

    private goToPerson(id: number) {
        this.router.goToPerson(id);
    }
}