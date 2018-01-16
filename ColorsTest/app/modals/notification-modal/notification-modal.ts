import { autoinject, observable, Container } from "aurelia-framework";
import { DialogService, DialogController } from "aurelia-dialog";

@autoinject
export class NotificationModal {
    constructor(private controller: DialogController) { }

    activate(opts: INotificationModalOptions) {
        this.opts = opts;

        if(!opts.okButtonText) {
            this.opts.okButtonText = "Ok";
        }
    }

    private opts: INotificationModalOptions;

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

export interface INotificationModalOptions {
    title: string;
    message: string;
    okButtonText?: string;
    okCallback?: () => {}
}

export function notifyModal(opts: INotificationModalOptions, whenClosed?: () => Promise<void>) {
    const container = Container.instance;
    const dialogService: DialogService = container.get(DialogService);

    dialogService.open({
        lock: true,
        viewModel: NotificationModal,
        model: opts
    }).whenClosed(async () => {
        if(whenClosed) {
            await whenClosed();
        }
    });
}