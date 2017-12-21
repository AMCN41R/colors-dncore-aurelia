import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";

export class AppRouter {

    private router: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        this.router = router

        config.map([
            { route: "/", redirect: "/people" },
            { route: "/people", name: "people", moduleId: PLATFORM.moduleName("./people/list") },
            { route: "/people/:id", name: "edit-person", moduleId: PLATFORM.moduleName("./people/edit-person") },
        ]);
    }

    goToPeople() {
        this.router.navigateToRoute("people");
    }

    goToPerson(id: number) {
        this.router.navigateToRoute("edit-person", { id: id });
    }
}