import appState from "app/appState";
import RentOfficeCreate from "components/RentOfficeCreate";
import NotFound from "./NotFound";

function RentOfficesCreate() {
    if (appState.roles.some(x => x == "Admin"))
        return <RentOfficeCreate />
    else return <NotFound />
}

export default RentOfficesCreate;