import { useState } from 'react';

import appState from 'app/appState';

import LogIn from './Login';
import StatusAndLogOut from './StatusAndLogout';
import Registration from './Registration';


class State {
	isInitialized: boolean = false;

	shallowClone(): State {
		return Object.assign(new State(), this);
	}
}


function Auth() {
	const [state, updateState] = useState(new State());

	let update = (updater: (state: State) => void) => {
		updateState(state => {
			updater(state);
			return state.shallowClone();
		})
	}

	if (!state.isInitialized) {
		appState.when(appState.isLoggedIn, () => {
			update(state => { });
		});

		update(state => state.isInitialized = true);
	}

	let html =
		<>
			{!appState.isLoggedIn.value &&
				<div style={{ display: "flex", justifyContent: 'flex-end' }}>
					<LogIn />
					<Registration />
				</div>
			}
			{appState.isLoggedIn.value &&
				<StatusAndLogOut />
			}
		</>;

	return html;
}

export default Auth;