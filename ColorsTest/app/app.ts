import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM, autoinject } from "aurelia-framework";
import { AppRouter } from "./app-router";

@autoinject
export class App {
    constructor(private appRouter: AppRouter) { }
    
    private configureRouter(config: RouterConfiguration, router: Router) {
        this.appRouter.configureRouter(config, router);
    }

    private goToPeople() {
        this.appRouter.goToPeople();
    }
}
