import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import './App.scss';
import NavBar from './NavBar';
import Footer from './Footer';
import About from './About';
import RentOfficeView from 'Views/RentOfficeView';
import RentOfficesCreate from 'Views/RentOfficeCreate';
import CarsView from 'Views/CarsView';
import RentOfficesEdit from 'Views/RentOfficesEdit';
import CarCreate from 'Views/CarsCreate';
import CarEdit from 'Views/CarsEdit';
import OrdersView from 'Views/OrdersView';
import OrdersCreate from 'components/OrdersCreate';
import OrdersEdit from 'components/OrdersEdit';

function App() {
	let html =
		<Router>
			<NavBar />
			<Routes>
				<Route path="/" element={<About />}></Route>
				<Route path="/RentOffices/:id/Cars/:carId/Orders/:orderId/Edit" element={<OrdersEdit />}></Route>
				<Route path="/RentOffices/:id/Cars/:carId/Orders/Create" element={<OrdersCreate />}></Route>
				<Route path="/RentOffices/:id/Cars/:carId/Orders" element={<OrdersView />}></Route>
				<Route path="/RentOffices/:id/Cars/Create" element={<CarCreate />}></Route>
				<Route path="/RentOffices/:id/Cars/:carId/Edit" element={<CarEdit />}></Route>
				<Route path="/RentOffices" element={<RentOfficeView />}></Route>
				<Route path="/RentOffices/Create" element={<RentOfficesCreate />}></Route>
				<Route path="/RentOffices/:id/Edit" element={<RentOfficesEdit />}></Route>
				<Route path="/RentOffices/:id/Cars" element={<CarsView />}></Route>
			</Routes>
			<Footer />
		</Router>;
	//
	return html;
}

export default App;