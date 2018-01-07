import { autoinject } from "aurelia-framework";
import { keyCodes } from "../utilities/key-codes";

@autoinject
export class EnterPressCustomAttribute {
    constructor(private element: Element) {
        this.enterPressed = e => {
            const key = e.which || e.keyCode;
            if (key === keyCodes.enter) {
                this.value();
            }
        };
    }

    private value: Function;
    private enterPressed: (e: KeyboardEvent) => void;

    attached() {
        this.element.addEventListener("keypress", this.enterPressed);
    }

    detached() {
        this.element.removeEventListener("keypress", this.enterPressed);
    }
}