import appState from 'app/appState';
import { createDefaultInstance, replaceBackend } from 'app/backend';

function StatusAndLogOut() {
	let onLogOut = () => {
		appState.userId = "";
		appState.userTitle = "";
		appState.authJwt = "";
		appState.roles = [];

		let defaultBackend = createDefaultInstance();
		replaceBackend(defaultBackend);

		appState.isLoggedIn.value = false;
	}

	let html =
		<>
			<span className="d-flex align-items-center">
				<span style={{ color: 'white' }}>{appState.userTitle}</span>
				<button
					type="button"
					className="btn btn-primary btn-sm ms-2"
					onClick={() => onLogOut()}
				>Log out</button>
			</span>
		</>;

	return html;
}

export default StatusAndLogOut;