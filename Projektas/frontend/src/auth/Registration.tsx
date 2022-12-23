import backend from "app/backend";
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

class State {
    username: string = "";
    email: string = "";
    password: string = "";
    isDialogVisible: boolean = false;

    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function Registration() {
    const [state, setState] = useState(new State());
    const navigate = useNavigate();
    let update = (updater: () => void) => {
        updater();
        setState(state.shallowClone());
    }
    let updateState = (updater: (state: State) => void) => {
        updater(state);
        return state.shallowClone();
    }

    let onRegistration = () => {
        update(() => {
            backend.post(
                "/api/register",
                {
                    username: state.username,
                    email: state.email,
                    password: state.password
                })
                .then(resp => {
                    update(() => state.isDialogVisible = false)
                })
        })
    }

    let html =
        <>
            <button
                type="button"
                className="btn btn-primary btn-sm"
                onClick={() => update(() => state.isDialogVisible = true)}
                style={{ justifyContent: "right", marginLeft: "auto" }}
            >Register</button>

            <Dialog
                visible={state.isDialogVisible}
                onHide={() => update(() => state.isDialogVisible = false)}
                header={<span className="me-2">Registration</span>}
                style={{ width: "50ch" }}
            >
                <div className="mb-3">
                    <label
                        htmlFor="username"
                        className="form-label">User name:</label>
                    <InputText
                        id="username"
                        className="form-control"
                        placeholder="Username"
                        autoFocus
                        value={state.username}
                        onChange={(e) => update(() => state.username = e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label
                        htmlFor="email"
                        className="form-label">Email:</label>
                    <InputText
                        id="email"
                        className="form-control"
                        placeholder="Email"
                        value={state.email}
                        onChange={(e) => update(() => state.email = e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label
                        htmlFor="password"
                        className="form-label">Password:</label>
                    <Password
                        id="password"
                        toggleMask
                        feedback={false}
                        className="form-control"
                        placeholder="Password"
                        value={state.password}
                        onChange={(e) => update(() => state.password = e.target.value)}
                    />
                </div>
                <div className="d-flex justify-contend-end">
                    <button
                        type="button"
                        className="btn btn-primary me-2"
                        onClick={() => onRegistration()}
                    >Register</button>
                    <button
                        type="button"
                        className="btn btn-primary"
                        onClick={() => update(() => state.isDialogVisible = false)}
                    >Cancel</button>
                </div>
            </Dialog>
        </>

    return html;
}

export default Registration;