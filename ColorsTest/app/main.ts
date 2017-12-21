import { PLATFORM } from "aurelia-pal";
import { Aurelia } from "aurelia-framework";

import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "../node_modules/font-awesome/css/font-awesome.min.css";

import "./main.scss";

export async function configure(aurelia: Aurelia) {

    aurelia
        .use
        .standardConfiguration()
        .feature(PLATFORM.moduleName("converters/index"))
        .developmentLogging;

    await aurelia.start();
    await aurelia.setRoot(PLATFORM.moduleName("app"));
}