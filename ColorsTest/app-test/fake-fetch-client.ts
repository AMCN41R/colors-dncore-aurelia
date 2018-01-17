import { HttpClient, json } from "aurelia-fetch-client";

export class FakeHttpClient extends HttpClient {
    data: any = {};
    shouldSucceed: boolean = true;

    // tslint:disable-next-line:variable-name
    private _urlReceived: string;
    public get urlReceived(): string {
        return this._urlReceived;
    }

    // tslint:disable-next-line:variable-name
    private _methodUsed: RestMethod;
    public get methodUsed(): RestMethod {
        return this._methodUsed;
    }

    // tslint:disable-next-line:variable-name
    private _dataReceived: any;
    public get dataReceived(): any {
        return this._dataReceived;
    }

    fetch(input: Request | string, init?: RequestInit): Promise<Response> {
        this._methodUsed = init ? this.mapRestMethod(init.method) : RestMethod.GET;
        this._urlReceived = (input instanceof Request) ? (<Request>input).url : <string>input;
        this._dataReceived = init ? init.body : null;

        return this.fakeResponse();
    }

    private mapRestMethod(method: string): RestMethod {
        switch (method.toUpperCase()) {
            case "GET":
                return RestMethod.GET;
            case "POST":
                return RestMethod.POST;
            case "PUT":
                return RestMethod.PUT;
            case "DELETE":
                return RestMethod.DELETE;
        }
    }

    private fakeResponse(): Promise<Response> {
        const response: Response = {
            arrayBuffer: null,
            blob: null,
            body: this.data,
            bodyUsed: this._dataReceived,
            clone: null,
            formData: null,
            headers: null,
            json: () => { return Promise.resolve(json(this.data)) },
            ok: this.shouldSucceed,
            redirected: false,
            status: this.shouldSucceed ? 200 : 400,
            statusText: this.shouldSucceed ? "Success" : "Bad Request",
            text: null,
            type: null,
            url: this.urlReceived
        };

        return Promise.resolve(response);
    }
}

export enum RestMethod {
    GET,
    PUT,
    POST,
    DELETE
}