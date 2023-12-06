import React from 'react'
import { NavLink, Link, useNavigate } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import { logout } from '../reducers/auth'

const Navbar = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()

  const { isLoggedIn, userData } = useSelector(state => state.auth)

  const handleLogoutClick = async () => {
    await dispatch(logout())
    navigate('/')
  }

  return (
    <header>
      <nav className='navbar navbar-expand-md navbar-dark bg-dark'>
        <a className='navbar-brand ms-5' href='/#'>
          Happy Paws
        </a>
        <button
          className='navbar-toggler'
          type='button'
          data-toggle='collapse'
          data-target='#navbarCollapse'
        >
          <span className='navbar-toggler-icon'></span>
        </button>
        <div className='collapse navbar-collapse' id='navbarCollapse'>
          <ul className='navbar-nav mr-auto flex-grow-1'>
            <li className='nav-item'>
              <NavLink to='/' className='nav-link'>
                Home
              </NavLink>
            </li>
            <li className='nav-item'>
              <NavLink to='/prices' className='nav-link'>
                Prices
              </NavLink>
            </li>
            {isLoggedIn && (
              <>
                <li className='nav-item'>
                  <NavLink to='/doctors' className='nav-link'>
                    Doctors
                  </NavLink>
                </li>
                <li className='nav-item'>
                  <NavLink to='/pets' className='nav-link'>
                    Pets
                  </NavLink>
                </li>
                <li className='nav-item'>
                  <NavLink to='/appointments' className='nav-link'>
                    Appointments
                  </NavLink>
                </li>
                <li className='nav-item'>
                  <NavLink to='/timeslots' className='nav-link'>
                    Timeslots
                  </NavLink>
                </li>
              </>
            )}
          </ul>
          {isLoggedIn && userData ? (
            <>
              <div className='text-light mx-2'>{userData.name}</div>
              <button
                type='button'
                className='btn btn-outline-danger mx-3'
                onClick={handleLogoutClick}
              >
                Logout
              </button>
            </>
          ) : (
            <>
              <Link
                to='/signin'
                className='btn btn-sm btn-outline-secondary me-3'
              >
                Sign in
              </Link>
              <Link to='/register' className='btn btn-sm btn-secondary me-3'>
                Register
              </Link>
            </>
          )}
        </div>
      </nav>
    </header>
  )
}

export default Navbar
