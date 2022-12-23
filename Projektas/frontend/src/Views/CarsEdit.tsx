import appState from "app/appState";
import CarsEdit from "components/CarsEdit";
import NotFound from "./NotFound";

export default function CarEdit() {
    if (appState.roles.some(x => x == "Admin"))
        return <CarsEdit />
    return <NotFound />
}