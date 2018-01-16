import { PLATFORM } from "aurelia-pal";
import { autoinject, reset } from "aurelia-framework";
import { DialogService, DialogOpenResult, DialogCancelResult } from "aurelia-dialog";
import { ColorApi, IColor } from "../api/color-api";
import { AddColorModal, IAddColorModalOptions } from "./add-color-modal";
import { ConfirmationModal, IConfirmationModalOptions } from "../modals/confirmation-modal/confirmation-modal";
import { NotificationModal, INotificationModalOptions, notifyModal } from "../modals/notification-modal/notification-modal";
import { Notifier } from "../utilities/notifier";

@autoinject
export class ColorsAdmin {
    constructor(
        private colorsApi: ColorApi,
        private dialogService: DialogService,
        private notify: Notifier
    ) { }

    private colors: IColor[];

    activate() {
        this.fetchData();
    }

    private async fetchData() {
        this.colors = await this.colorsApi.getColors();
    }

    private async add() {
        const opts: IAddColorModalOptions = {
            existingColors: this.colors.map(x => x.name)
        };

        const dialog = await this.dialogService.open({
            lock: false,
            viewModel: AddColorModal,
            model: opts
        }) as DialogOpenResult;

        const result = await dialog.closeResult;

        if (result.wasCancelled) {
            return;
        }

        await this.colorsApi.addColor(result.output);
        this.notify.success("New color has been added.");
        await this.fetchData();
    }

    private async delete(color: IColor) {
        const confirmOpts: IConfirmationModalOptions = {
            title: "Delete Color",
            question: `Are you sure you want to delete the color ${color.name}?`,
            okButtonText: "Yes",
            cancelButtonText: "Cancel"
        };

        const dialog = await this.dialogService.open({
            lock: false,
            viewModel: ConfirmationModal,
            model: confirmOpts
        }) as DialogOpenResult;

        const result = await dialog.closeResult;

        if (result.wasCancelled) {
            return;
        }

        if (!(await this.colorsApi.canDeleteColor(color.id))) {
            const opts: INotificationModalOptions = { title: "Delete Color", message: "Cannot delete this color as it is currently is use." };
            notifyModal(opts);
            return;
        }

        await this.colorsApi.deleteColor(color.id);
        this.notify.success("Color has been deleted.");
        await this.fetchData();
    }
}