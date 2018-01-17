import "../setup";

import { json } from "aurelia-fetch-client";
import { FakeHttpClient, RestMethod } from "../fake-fetch-client";
import { PeopleApi, IPerson } from "../../app/api/people-api";

describe("people-api", () => {
    let sut: PeopleApi;
    let fakeHttpClient: FakeHttpClient;

    beforeEach(() => {
        fakeHttpClient = new FakeHttpClient();
        sut = new PeopleApi(fakeHttpClient);
    });

    describe("getPeople", () => {

        test("calls correct url", async () => {
            await sut.getPeople();
            expect(fakeHttpClient.urlReceived).toBe("api/people");
        });

        test("uses correct REST verb", async () => {
            await sut.getPeople();
            expect(fakeHttpClient.methodUsed).toBe(RestMethod.GET);
        });

    });

    describe("getPerson", () => {

        test("calls correct url", async () => {
            await sut.getPerson(123);
            expect(fakeHttpClient.urlReceived).toBe("api/people/123");
        });

        test("uses correct REST verb", async () => {
            await sut.getPerson(123);
            expect(fakeHttpClient.methodUsed).toBe(RestMethod.GET);
        });

    });

    describe("updatePerson", () => {

        const person: IPerson = {
            id: 123,
            firstName: "Tony",
            lastName: "Stark",
            fullName: "Tony Stark",
            isAuthorised: true,
            isEnabled: true,
            isValid: true,
            isPalindrome: false,
            colors: null
        };

        test("calls correct url", async () => {
            await sut.updatePerson(person);
            expect(fakeHttpClient.urlReceived).toBe("api/people/123");
        });

        test("uses correct REST verb", async () => {
            await sut.updatePerson(person);
            expect(fakeHttpClient.methodUsed).toBe(RestMethod.POST);
        });

        test("passes correct data", () => {
            sut.updatePerson(person);
            expect(fakeHttpClient.dataReceived).toEqual(json(person));
        });

    });
});