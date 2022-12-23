import { ObservableClass } from "observable/observable";


class AppState extends ObservableClass {
	userId: string = "";
	userTitle: string = "";
	roles: string[] = [];

	authJwt: string | null = null;

	isLoggedIn = this.observableProperty<boolean>(false);
}

let appState = new AppState();
export default appState;