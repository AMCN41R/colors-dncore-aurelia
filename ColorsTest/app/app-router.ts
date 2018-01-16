import { Router, RouterConfiguration } from "aurelia-router";
import { PLATFORM } from "aurelia-pal";

export class AppRouter {

    private router: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        this.router = router

        config.map([
            { route: "/", redirect: "/people" },
            { route: "/people", name: "people", moduleId: PLATFORM.moduleName("./pages/people/list/list") },
            { route: "/people/:id", name: "edit-person", moduleId: PLATFORM.moduleName("./pages/people/edit/edit-person") },
            { route: "/colors", name: "colors", moduleId: PLATFORM.moduleName("./pages/colors/colors-admin") }
        ]);
    }

    goToPeople() {
        this.router.navigateToRoute("people");
    }

    goToPerson(id: number) {
        this.router.navigateToRoute("edit-person", { id: id });
    }

    goToColors() {
        this.router.navigateToRoute("colors");
    }
}