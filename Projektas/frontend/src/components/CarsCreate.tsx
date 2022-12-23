import backend from "app/backend";
import { InputText } from "primereact/inputtext";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Select, MenuItem, FormControl, InputLabel } from "@mui/material";

class State {
    manufacturer = '';
    model = '';
    transmission = '';
    tyre = '';
    drivetrain = '';

    isSaveError = false;

    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function CarsCreate() {
    const [state, setState] = useState(new State());
    const navigate = useNavigate();
    const locationParams = useParams();

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
                manufacturer: state.manufacturer,
                model: state.model,
                transmissionType: state.transmission,
                drivetrainType: state.drivetrain,
                tyreCompound: state.tyre
            };

            backend.post(`api/RentOffices/${locationParams['id']}/Cars`, entity)
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

                        <label htmlFor="name" className="form-label" style={{ color: "white" }}>Manufacturer</label>
                        <InputText
                            id="name"
                            className={"form-control"}
                            value={state.manufacturer}
                            onChange={(e) => update(() => state.manufacturer = e.target.value)}
                        />

                        <label htmlFor="description" className="form-label" style={{ color: "white" }}>Model:</label>
                        <InputText
                            id="description"
                            className={"form-control"}
                            value={state.model}
                            onChange={(e) => update(() => state.model = e.target.value)}
                        />

                        <FormControl fullWidth>
                            <InputLabel>Transmission type</InputLabel>
                            <Select
                                value={state.transmission}
                                label="Transmission type"
                                onChange={(e) => update(() => state.transmission = e.target.value)}
                            >
                                <MenuItem value='Automatic'>Automatic</MenuItem>
                                <MenuItem value='Manual'>Manual</MenuItem>
                                <MenuItem value='Sequencial'>Sequencial</MenuItem>
                            </Select>
                        </FormControl>

                        <FormControl fullWidth>
                            <InputLabel>Tyre type</InputLabel>
                            <Select
                                value={state.tyre}
                                label="Tyre type"
                                onChange={(e) => update(() => state.tyre = e.target.value)}
                            >
                                <MenuItem value='Road'>Road</MenuItem>
                                <MenuItem value='SemiSlick'>SemiSlick</MenuItem>
                                <MenuItem value='Slick'>Slick</MenuItem>
                            </Select>
                        </FormControl>
                        <FormControl fullWidth>
                            <InputLabel>Drivetrain type</InputLabel>
                            <Select
                                value={state.drivetrain}
                                label="Drivetrain type"
                                onChange={(e) => update(() => state.drivetrain = e.target.value)}
                            >
                                <MenuItem value='FF'>FF</MenuItem>
                                <MenuItem value='FR'>FR</MenuItem>
                                <MenuItem value='MR'>MR</MenuItem>
                                <MenuItem value='RR'>RR</MenuItem>
                                <MenuItem value='AWD'>AWD</MenuItem>
                            </Select>
                        </FormControl>
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

export default CarsCreate;