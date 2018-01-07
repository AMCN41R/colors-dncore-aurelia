import { PLATFORM } from "aurelia-pal";
import { autoinject } from "aurelia-framework";
import { DialogService } from "aurelia-dialog";
import { ColorApi, IColor } from "../api/color-api";
import { AddColorModal, IAddColorModalOptions } from "./add-color-modal";

@autoinject
export class ColorsAdmin {
    constructor(
        private colorsApi: ColorApi,
        private dialogService: DialogService
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

        this.dialogService.open({
            lock: false,
            viewModel: AddColorModal,
            model: opts
        }).whenClosed(async response => {
            if (!response.wasCancelled) {
                await this.colorsApi.addColor(response.output);
            }
        })
    }

    private edit(color: IColor) {

    }

    private delete(color: IColor) {
        
    }
}