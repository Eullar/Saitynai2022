import appState from "app/appState";
import backend from "app/backend";
import { RentOffice } from "models/RentOffice";
import { Dialog } from "primereact/dialog";
import { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import { useLocation, useNavigate, useParams } from "react-router-dom";

class State {
    isInitialized: boolean = false;
    isLoaded: boolean = false;
    isLoading: boolean = true;
    isDialogVisible: boolean = false;
    rentOffices: RentOffice[] | null = null;
    deletedRentOffice: RentOffice | null = null;
    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function RentOffices() {
    const [state, setState] = useState(new State());
    const locationParams = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    let update = (updater: (state: State) => void) => {
        updateState(state => {
            updater(state);
            return state.shallowClone();
        })
    }

    let toEdit = (id: string) => {
        navigate(`./${id}/Edit/`);
    }

    let toCars = (id: string) => {
        navigate(`./${id}/Cars`);
    }


    let deleteRentOffice = (rentOffice: RentOffice) => {
        update(() => {
            state.deletedRentOffice = rentOffice;
            state.isDialogVisible = true;
        });
    }
    let onConfirmedDelete = (id: string) => {
        update(() => {
            backend.delete(`api/RentOffices/${id}`)
                .then(resp => {
                    update(() => {
                        state.isDialogVisible = false;
                        location.state = "refresh";
                    });
                })
                .catch(err => {
                })
        });
    }
    let updateState = (updater: (state: State) => void) => {
        setState(state => {
            updater(state);
            return state.shallowClone();
        })
    }

    if (!state.isInitialized || location.state === "refresh") {
        backend.get<RentOffice[]>('api/RentOffices')
            .then(resp => {
                update(state => {
                    state.isLoading = false;
                    state.isLoaded = true;
                    state.rentOffices = resp.data;
                })
            });

        location.state = null;

        update(state => {
            state.isLoading = true;
            state.isLoaded = false;
            state.isInitialized = true;
        })
    }

    let isAdmin = () => {
        const roles = appState.roles;
        return roles.some(x => x === "Admin");
    }

    let html =
        <div className="content">
            {state.isLoaded && <>
                <h2>Rent Offices</h2>
                {isAdmin() && <div className="p-2"><button
                    type="button"
                    className="btn btn-primary mx-1"
                    onClick={() => navigate("./Create/")}
                ><i className="fa-solid"></i>Create new Rent Office</button></div>}
                <Row xd={1} md={3} className="g-4">
                    {state.rentOffices?.map(rentOffice =>
                        <Col>
                            <Card
                                bg="light"
                                text="dark"
                            >
                                <Card.Body>
                                    <div
                                        onClick={() => toCars(rentOffice.id)}>
                                        <Card.Title>
                                            {rentOffice.name}
                                        </Card.Title>
                                        <Card.Text>
                                            Location: {rentOffice.location}
                                        </Card.Text>
                                        <Card.Text>
                                            Amount of cars: {rentOffice.carCount}
                                        </Card.Text>
                                    </div>
                                    {isAdmin() && <>
                                        <Button
                                            variant="primary"
                                            onClick={() => toEdit(rentOffice.id)}>Edit</Button>
                                        <Button
                                            variant="danger"
                                            onClick={() => deleteRentOffice(rentOffice)}>Delete</Button>
                                    </>}
                                </Card.Body>
                            </Card>

                        </Col>
                    )}
                </Row>
                <Dialog
                    visible={state.isDialogVisible}
                    onHide={() => update(() => state.isDialogVisible = false)}
                    header={<span className="me-2">Do you really want to delete this rent office</span>}
                    style={{ width: "50ch" }}
                >
                    <div>
                        <h3>{state.deletedRentOffice?.name}</h3>
                    </div>
                    <Button
                        variant="danger"
                        onClick={() => onConfirmedDelete(state.deletedRentOffice?.id ?? '')}>Delete</Button>
                    <button
                        type="button"
                        className="btn btn-primary"
                        onClick={() => update(() => state.isDialogVisible = false)}
                    >Cancel</button>
                </Dialog>
            </>
            }
        </div >

    return html;
}

export default RentOffices;