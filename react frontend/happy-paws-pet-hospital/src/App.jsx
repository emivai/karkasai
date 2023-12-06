import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Doctors from "./pages/Doctors";
import Pets from "./pages/Pets";
import Appointments from "./pages/Appointments";
import SignIn from "./pages/SignIn";
import Register from "./pages/Register";
import Prices from "./pages/Prices";
import UserEdit from "./pages/UserEdit";
import Timeslots from './pages/Timeslots'

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes> 
        <Route path="/" element={<Home />} />
        <Route path="/doctors" element={<Doctors />} />
        <Route path="/pets" element={<Pets />} />
        <Route path="/appointments" element={<Appointments />} />
        <Route path="/signin" element={<SignIn />} />
        <Route path="/register" element={<Register />} />
        <Route path="/prices" element={<Prices />} />
        <Route path="/user/edit" element={<UserEdit />} />
        <Route path='/timeslots' element={<Timeslots />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
