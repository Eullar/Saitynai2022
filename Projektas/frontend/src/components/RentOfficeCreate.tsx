import backend from "app/backend";
import { InputText } from "primereact/inputtext";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

class State {
    name = '';
    location = '';

    isSaveError = false;

    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function RentOfficeCreate() {
    const [state, setState] = useState(new State());
    const navigate = useNavigate();

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

    let save = () => {
        update(() => {
            let entity = {
                name: state.name,
                location: state.location,
            };

            backend.post('api/RentOffices', entity)
                .then(resp => {
                    navigate("./../", { state: "refresh" });
                })
                .catch(err => {
                    updateState(state => state.isSaveError = true);
                });
        });
    }

    let html =
        <>
            <div className="d-flex flex-column h-100 overflow-auto">
                <div className="d-flex justify-content-center">
                    <div className="d-flex flex-column align-items-start" style={{ width: "80ch" }}>
                        {state.isSaveError &&
                            <div
                                className="alert alert-warning w-100"
                            >Saving failed due to backend failure. Please, wait a little and retry.</div>
                        }

                        <label htmlFor="name" className="form-label" style={{ color: "white" }}>Name of rent office:</label>
                        <InputText
                            id="name"
                            className={"form-control"}
                            value={state.name}
                            onChange={(e) => update(() => state.name = e.target.value)}
                        />

                        <label htmlFor="description" className="form-label" style={{ color: "white" }}>Location:</label>
                        <InputText
                            id="description"
                            className={"form-control"}
                            value={state.location}
                            onChange={(e) => update(() => state.location = e.target.value)}
                        />
                    </div>
                </div>

                <div className="d-flex justify-content-center align-items-center w-100 mt-1">
                    <button
                        type="button"
                        className="btn btn-primary mx-1"
                        onClick={() => save()}
                    ><i className="fa-solid fa-floppy-disk"></i>Save</button>
                    <button
                        type="button"
                        className="btn btn-primary mx-1"
                        onClick={() => navigate("./../")}
                    ><i className="fa-solid fa-xmark"></i>Cancel</button>
                </div>
            </div>
        </>;

    return html;
}

export default RentOfficeCreate;