import React from 'react'
import { NavLink, Link } from 'react-router-dom'

const Navbar = () => {
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
              <NavLink to='/doctors' className='nav-link'>
                Doctors
              </NavLink>
            </li>
            <li className='nav-item'>
              <NavLink to='/prices' className='nav-link'>
                Prices
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
          </ul>
          <Link to='/signin' className='btn btn-sm btn-outline-secondary me-5'>
            Sign in
          </Link>
        </div>
      </nav>
    </header>
  )
}

export default Navbar
