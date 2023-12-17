import { BrowserRouter, Routes, Route } from "react-router-dom";
import CustomNavbar from "./components/Navbar";
import Home from "./pages/Home";
import Doctors from "./pages/Doctors";
import Pets from "./pages/Pets";
import Appointments from "./pages/Appointments";
import SignIn from "./pages/SignIn";
import Register from "./pages/Register";
import Prices from "./pages/Prices";
import UserEdit from "./pages/UserEdit";
import Timeslots from "./pages/Timeslots";
import Footer from "./components/Footer";

function App() {
  return (
    <div className="d-flex flex-column h-100">
      <BrowserRouter>
        <CustomNavbar />
        <div className="flex-grow-1">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/doctors" element={<Doctors />} />
            <Route path="/pets" element={<Pets />} />
            <Route path="/appointments" element={<Appointments />} />
            <Route path="/signin" element={<SignIn />} />
            <Route path="/register" element={<Register />} />
            <Route path="/prices" element={<Prices />} />
            <Route path="/user/edit" element={<UserEdit />} />
            <Route path="/timeslots" element={<Timeslots />} />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
    </div>
  );
}

export default App;
