import appState from "app/appState";
import backend from "app/backend";
import { Order } from "models/Order";
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
    orders: Order[] | null = null;
    deletedOrder: Order | null = null;
    shallowClone(): State {
        return Object.assign(new State(), this);
    }
}

function Orders() {
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


    let onDelete = (order: Order) => {
        update(() => {
            state.deletedOrder = order;
            state.isDialogVisible = true;
        });
    }

    let onConfirmedDelete = (id: string) => {
        update(() => {
            backend.delete(`api/RentOffices/${locationParams['id']}/Cars/${locationParams['carId']}/Orders/${id}`)
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
        backend.get<Order[]>(`api/RentOffices/${locationParams['id']}/Cars/${locationParams['carId']}/Orders`)
            .then(resp => {
                update(state => {
                    state.isLoading = false;
                    state.isLoaded = true;
                    state.orders = resp.data;
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

    let isUser = () => {
        const roles = appState.roles;
        return roles.some(x => x === "User");
    }

    let html =
        <div className="content">
            {state.isLoaded && <>
                {!isAdmin() && <h2>Your orders for this Car</h2>}
                {isAdmin() && <h2>All orders for this Car</h2>}
                {isUser() && <div className="p-2"><button
                    type="button"
                    className="btn btn-primary mx-1"
                    onClick={() => navigate("./Create/")}
                ><i className="fa-solid"></i>Create a new Order</button></div>}
                <Row xd={1} md={3} className="g-4">
                    {state.orders?.map(order =>
                        <Col>
                            <Card
                                bg="light"
                                text="dark"
                            >
                                <Card.Body>
                                    <Card.Title>
                                        {`Order: ${order.id}`}
                                    </Card.Title>
                                    <Card.Text>
                                        Order date: {order.orderDate.toString()}
                                    </Card.Text>
                                    <Card.Text>
                                        Rent date: {order.rentDate.toString()}
                                    </Card.Text>
                                    <Card.Text>
                                        Car ordered: {order.carId}
                                    </Card.Text>
                                    {isAdmin() && <>
                                        <Card.Text>
                                            User: {order.userId}
                                        </Card.Text>
                                        <Button
                                            variant="primary"
                                            onClick={() => toEdit(order.id)}>Edit</Button>
                                        <Button
                                            variant="danger"
                                            onClick={() => onDelete(order)}>Delete</Button>
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
                        <h3>{state.deletedOrder?.id}</h3>
                    </div>
                    <Button
                        variant="danger"
                        onClick={() => onConfirmedDelete(state.deletedOrder?.id ?? '')}>Delete</Button>
                    <button
                        type="button"
                        className="btn btn-primary"
                        onClick={() => update(() => state.isDialogVisible = false)}
                    >Cancel</button>
                </Dialog>
            </>}
        </div>

    return html;
}

export default Orders;