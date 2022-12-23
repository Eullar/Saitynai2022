import backend from "app/backend";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Calendar } from "primereact/calendar";
import { stat } from "fs";
import { Order } from "models/Order";

class State {
    rentDate: Date = new Date(Date.now());

    isInitialized = false;
    isSaveError = false;

    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function OrdersEdit() {
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

    if (!state.isInitialized) {
        backend.get<Order>(`api/RentOffices/${locationParams['id']}/Cars/${locationParams['carId']}/Orders/${locationParams['orderId']}`)
            .then(response => {
                updateState(resp => {
                    state.rentDate = new Date(resp.rentDate);
                })
            })
            .catch();

        update(() => {
            state.isInitialized = true;
        });
    }

    let onSave = () => {
        update(() => {

            let entity = {
                rentDate: state.rentDate
            };

            backend.put(`api/RentOffices/${locationParams['id']}/Cars/${locationParams['carId']}/Orders/${locationParams['orderId']}`, entity)
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

                        <label htmlFor="date" className="form-label" style={{ color: "white" }}>Select the date you wish to rent the car for:</label>
                        <Calendar
                            id="date"
                            className="form-control"
                            value={state.rentDate}
                            onChange={(e) => update(() => state.rentDate = e.value as Date)}
                            dateFormat="yy-mm-dd"
                        />
                    </div>
                </div>

                <div className="d-flex justify-content-center align-items-center w-100 mt-1">
                    <button
                        type="button"
                        className="btn btn-primary mx-1"
                        onClick={() => onSave()}
                    ><i className="fa-solid fa-floppy-disk"></i>Save</button>
                    <button
                        type="button"
                        className="btn btn-primary mx-1"
                        onClick={() => navigate('./../')}
                    ><i className="fa-solid fa-xmark"></i>Cancel</button>
                </div>
            </div>
        </>;

    return html;
}

export default OrdersEdit;