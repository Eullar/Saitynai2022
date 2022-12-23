import appState from "app/appState";
import Orders from "components/Orders";
import NotFound from "./NotFound";

export default function OrdersView() {
    if (appState.roles.some(x => x == "User"))
        return <Orders />
    else return <NotFound />
}