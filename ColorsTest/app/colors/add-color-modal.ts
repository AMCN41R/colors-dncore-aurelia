import { autoinject, observable } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";

@autoinject
export class AddColorModal {
    constructor(private controller: DialogController) { }

    activate(opts: IAddColorModalOptions) {
        this.existingColors = opts.existingColors;
    }

    @observable private name: string;
    private nameChanged() {
        this.canAdd = 
            this.name 
            && this.name.trim() !== "" 
            && !this.existingColors.some(x => x.trim().toLowerCase() === this.name.trim().toLowerCase());
    }

    private canAdd: boolean;
    private existingColors: string[];

    private add() {
        if(!this.canAdd){
            return;
        }
        
        this.controller.ok(this.name);
    }

    private cancel() {
        this.controller.cancel();
    }
}

export interface IAddColorModalOptions {
    existingColors: string[]
}