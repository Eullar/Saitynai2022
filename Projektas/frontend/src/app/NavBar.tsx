import Auth from "auth/Auth";
import { NavLink } from "react-router-dom";

function NavBar() {
    let html =
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <NavLink className="navbar-brand pl-2" to="/">Rent A Racecar</NavLink>
            <div className="collapse navbar-collapse">
                <div className="navbar-nav d-flex w-100">
                    <NavLink className="nav-item nav-link d-flex justify-content-start" to="/RentOffices">Rent offices</NavLink>
                    <div className="w-100 d-flex justify-content-end"><Auth /></div>
                </div>
            </div>
        </nav>

    return html;
}

export default NavBar;