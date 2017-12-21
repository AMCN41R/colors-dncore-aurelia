import { autoinject } from "aurelia-framework";
import { PeopleApi, IPerson } from "../api/people-api";
import { ColorApi, IColor } from "../api/color-api";
import { AppRouter } from "../app-router";

@autoinject
export class EditPerson {
    constructor(
        private router: AppRouter,
        private peopleApi: PeopleApi,
        private colorApi: ColorApi
    ) { }

    private error: string = null;
    private person: IPerson
    private colors: IColorViewModel[];

    activate(params) {
        if (!(params.id as number)) {
            this.error = "Error loading patient";
            return;
        }

        // not awaiting this call so that the page is loaded, 
        // and the data will load in the background
        this.fetchData(params.id);
    }

    private async fetchData(id: number): Promise<void> {
        this.person = await this.peopleApi.getPerson(id);

        const allColors = await this.colorApi.getColors();
        this.colors = allColors.map(x => {
            return {
                id: x.id,
                name: x.name,
                selected: this.person.colors.some(y => y.id == x.id)
            }
        });
    }

    private setAuthorised(authorised: boolean) {
        this.person.isAuthorised = authorised;
    }

    private setEnabled(enabled: boolean) {
        this.person.isEnabled = enabled;
    }

    private setColor(color: IColorViewModel) {
        const c = this.colors.find(x => x.id == color.id);
        c.selected = !c.selected;
    }

    private async save(): Promise<void> {
        this.person.colors =
            this.colors
                .filter(x => x.selected)
                .map(x => {
                    return {
                        id: x.id,
                        name: x.name, 
                        isEnabled: true
                    };
                });

        await this.peopleApi.updatePerson(this.person);
        this.router.goToPeople();
    }

    private cancel() {
        this.router.goToPeople();
    }
}

interface IColorViewModel {
    id: number,
    name: string;
    selected: boolean;
}