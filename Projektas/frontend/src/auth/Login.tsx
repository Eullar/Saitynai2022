import { useState } from 'react';
import axios from 'axios';

import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Password } from 'primereact/password';

import appState from 'app/appState';
import backend, { replaceBackend } from 'app/backend';

class State {
	isDialogVisible: boolean = false;

	username: string = "";
	password: string = "";

	isUsernameErr: boolean = false;
	isPasswordErr: boolean = false;
	isLoginErr: boolean = false;

	resetErrors() {
		this.isUsernameErr = false;
		this.isPasswordErr = false;
		this.isLoginErr = false;
	}

	shallowClone(): State {
		return Object.assign(new State(), this);
	}
}

function LogIn() {
	const [state, setState] = useState(new State());

	let update = (updater: () => void) => {
		updater();
		setState(state.shallowClone());
	}

	let updateState = (updater: (state: State) => void) => {
		setState(state => {
			updater(state);
			return state.shallowClone();
		})
	}

	let onLogIn = () => {
		update(() => {
			state.resetErrors();

			if (state.username.trim() === "")
				state.isUsernameErr = true;

			if (state.password === "")
				state.isPasswordErr = true;

			let hasErrs =
				state.isUsernameErr ||
				state.isPasswordErr;

			if (hasErrs)
				return;

			backend.post(
				"api/login",
				{
					username: state.username,
					password: state.password
				}
			)
				.then(resp => {
					let data = resp.data;

					appState.userTitle = state.username;
					appState.roles = data.roles;
					appState.authJwt = data.authenticationToken;

					console.log(resp.data.authenticationToken);

					let backendWithAuth =
						axios.create({
							baseURL: 'https://localhost:7211',
							headers: {
								Authorization: `Bearer ${appState.authJwt}`
							}
						});
					replaceBackend(backendWithAuth);

					appState.isLoggedIn.value = true;
				})
				.catch(() => {
					updateState(state => {
						state.isLoginErr = true;
					});
				});
		});
	}

	let html =
		<>
			<button
				type="button"
				className="btn btn-primary btn-sm"
				onClick={() => update(() => state.isDialogVisible = true)}
				style={{ marginRight: "20px", float: "right" }}
			>Login</button>
			<Dialog
				visible={state.isDialogVisible}
				onHide={() => update(() => state.isDialogVisible = false)}
				header={<span className="me-2">Enter your crendentials, please.</span>}
				style={{ width: "50ch" }}
			>
				{state.isLoginErr &&
					<div className="alert alert-warning">Log in has failed. Incorrect username, password or both.</div>
				}
				<div className="mb-3">
					<label
						htmlFor="username"
						className="form-label"
					>Username:</label>
					<InputText
						id="username"
						className={"form-control " + (state.isUsernameErr ? "is-invalid" : "")}
						placeholder="Enter your username"
						autoFocus
						value={state.username}
						onChange={(e) => update(() => state.username = e.target.value)}
					/>
					{state.isUsernameErr &&
						<div className="invalid-feedback">Username must be non empty and non whitespace.</div>
					}
				</div>
				<div className="mb-3">
					<label
						htmlFor="password"
						className="form-label"
					>Password</label>
					<Password
						id="password"
						className={"form-control " + (state.isPasswordErr ? "is-invalid" : "")}
						placeholder="Enter your password"
						toggleMask
						feedback={false}
						value={state.password}
						onChange={(e) => update(() => state.password = e.target.value)}
					/>
					{state.isPasswordErr &&
						<div className="invalid-feedback">Password must be non empty.</div>
					}
				</div>
				<div className="d-flex justify-content-end">
					<button
						type="button"
						className="btn btn-primary me-2"
						onClick={() => onLogIn()}
					>Log in</button>
					<button
						type="button"
						className="btn btn-primary"
						onClick={() => update(() => state.isDialogVisible = false)}
					>Cancel</button>
				</div>
			</Dialog>
		</>;

	return html;
}

export default LogIn;