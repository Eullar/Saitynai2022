import appState from "app/appState";
import backend from "app/backend";
import { Car } from "models/Car";
import { Dialog } from "primereact/dialog";
import { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import golf from "../assets/mk2.jpg"

class State {
    isInitialized: boolean = false;
    isLoaded: boolean = false;
    isLoading: boolean = true;
    isDialogVisible: boolean = false;
    cars: Car[] | null = null;
    deletedCar: Car | null = null;
    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function Cars() {
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

    let toOrders = (id: string) => {
        navigate(`./${id}/Orders`);
    }


    let deleteCar = (car: Car) => {
        update(() => {
            state.deletedCar = car;
            state.isDialogVisible = true;
        });
    }
    let onConfirmedDelete = (id: string) => {
        update(() => {
            backend.delete(`api/RentOffices/${locationParams['id']}/Cars/${id}`)
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
        backend.get<Car[]>(`api/RentOffices/${locationParams['id']}/Cars`)
            .then(resp => {
                update(state => {
                    state.isLoading = false;
                    state.isLoaded = true;
                    state.cars = resp.data;
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
                <h2>Cars in this rent office</h2>
                {isAdmin() && <div className="p-2"><button
                    type="button"
                    className="btn btn-primary mx-1"
                    onClick={() => navigate("./Create/")}
                ><i className="fa-solid"></i>Create a new Car</button></div>}
                <Row xd={1} md={3} className="g-4">
                    {state.cars?.map(car =>
                        <Col>
                            <Card
                                bg="light"
                                text="dark"
                            >
                                <Card.Img variant="top" src={golf} />
                                <Card.Body>
                                    <div
                                        onClick={() => toOrders(car.id)}>
                                        <Card.Title>
                                            {`${car.manufacturer} ${car.model}`}
                                        </Card.Title>
                                        <Card.Text>
                                            Transmission: {car.transmissionType}
                                        </Card.Text>
                                        <Card.Text>
                                            Drivetrain: {car.drivetrainType}
                                        </Card.Text>
                                        <Card.Text>
                                            Tyres: {car.tyreCompound}
                                        </Card.Text>
                                    </div>
                                    {isAdmin() && <>
                                        <Button
                                            variant="primary"
                                            onClick={() => toEdit(car.id)}>Edit</Button>
                                        <Button
                                            variant="danger"
                                            onClick={() => deleteCar(car)}>Delete</Button>
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
                        <h3>{state.deletedCar?.manufacturer + " " + state.deletedCar?.model}</h3>
                    </div>
                    <Button
                        variant="danger"
                        onClick={() => onConfirmedDelete(state.deletedCar?.id ?? '')}>Delete</Button>
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

export default Cars;