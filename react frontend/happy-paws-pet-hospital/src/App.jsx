import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Navbar from './components/Navbar'
import Home from './pages/Home'
import Doctors from './pages/Doctors'
import Login from './pages/Login'
import Pets from './pages/Pets'
import Appointments from './pages/Appointments'
import SignIn from './pages/SignIn'
import Register from './pages/Register'

function App () {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/doctors' element={<Doctors />} />
        <Route path='/login' element={<Login />} />
        <Route path='/pets' element={<Pets />} />
        <Route path='/appointments' element={<Appointments />} />
        <Route path='/signin' element={<SignIn />} />
        <Route path='/register' element={<Register />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
