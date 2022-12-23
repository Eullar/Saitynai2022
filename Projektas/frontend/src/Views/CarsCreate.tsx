import appState from "app/appState";
import Cars from "components/Cars";
import CarsCreate from "components/CarsCreate";
import NotFound from "./NotFound";

export default function CarCreate() {
    if (appState.roles.some(x => x == "Admin"))
        return <CarsCreate />
    return <NotFound />
}