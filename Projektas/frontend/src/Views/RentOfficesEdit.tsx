import appState from "app/appState";
import RentOfficeEdit from "components/RentOfficeEdit";
import NotFound from "./NotFound";

export default function RentOfficesEdit() {
    if (appState.roles.some(x => x == "Admin"))
        return <RentOfficeEdit />
    else return <NotFound />
}