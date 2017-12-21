export class ToYesNoValueConverter {
    toView(value: boolean): string {
        return value ? "Yes" : "No";
    }
}