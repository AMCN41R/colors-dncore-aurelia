import "jest";
import { ToYesNoValueConverter } from "../../app/converters/to-yes-no-value-converter";

describe("to-yes-no-value-converter", () => {
    let sut: ToYesNoValueConverter;

    beforeEach(() => {
        sut = new ToYesNoValueConverter();
    });

    describe("toView", () => {

        test("value of 'true' returns 'Yes'", () => {
            expect(sut.toView(true)).toBe("Yes");
        });

        test("value of 'false' returns 'No'", () => {
            expect(sut.toView(false)).toBe("No");
        });

        test("null or undefined returns 'No'", () => {
            expect(sut.toView(null)).toBe("No");
            expect(sut.toView(undefined)).toBe("No");
        });
        
    });
});