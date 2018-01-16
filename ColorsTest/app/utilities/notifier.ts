import * as toastr from "toastr";

export class Notifier {
    constructor() {
        toastr.options.closeButton = false;
        toastr.options.positionClass = "toast-bottom-center";
    }

    info(message: string): void {
        toastr.info(message);
    }

    warning(message: string): void {
        toastr.warning(message);
    }

    success(message?: string, onclick?: () => void): void {
        if (!message) {
            message = "Success";
        }

        if(!onclick) {
            toastr.success(message);
            return;    
        }

        const opts: ToastrOptions = {
            onclick: onclick
        };

        toastr.success(message, null, opts);
    }

    error(message?: string): void {
        if (!message) {
            message = "Error";
        }

        toastr.error(message);
    }
}
