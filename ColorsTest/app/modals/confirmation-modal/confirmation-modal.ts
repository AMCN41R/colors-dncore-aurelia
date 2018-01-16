import { autoinject, observable } from "aurelia-framework";
import { DialogController } from "aurelia-dialog";

@autoinject
export class ConfirmationModal {
    constructor(private controller: DialogController) { }

    activate(opts: IConfirmationModalOptions) {
        this.opts = opts;

        if(!opts.okButtonText) {
            this.opts.okButtonText = "Ok";
        }

        if(!opts.cancelButtonText) {
            this.opts.cancelButtonText = "Cancel";
        }
    }

    private opts: IConfirmationModalOptions;

    private okClicked() {
        if(this.opts && this.opts.okCallback){
            this.opts.okCallback();
        }

        this.controller.ok();
    }

    private cancelClicked() {
        this.controller.cancel();
    }
}

export interface IConfirmationModalOptions {
    title: string;
    question: string;
    okButtonText?: string;
    cancelButtonText?: string;
    okCallback?: () => {}
}