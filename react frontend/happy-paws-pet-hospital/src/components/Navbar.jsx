import React from 'react'

function Navbar () {
  return (
    <header>
      <nav className='navbar navbar-expand-md navbar-dark fixed-top bg-dark'>
        <div className='container-fluid'>
          <div className='row w-100'>
            <div className='col-10'>

              <div className='collapse navbar-collapse' id='navbarCollapse'>
              <div className='navbar-brand' href='#'>
                Happy Paws
              </div>
                <ul className='navbar-nav mr-auto'>
                  <li className='nav-item active'>
                    <a className='nav-link' href='#'>
                      Home <span className='sr-only'></span>
                    </a>
                  </li>
                  <li className='nav-item'>
                    <a className='nav-link' href='html/doctors.html'>
                      Doctors
                    </a>
                  </li>
                  <li className='nav-item'>
                    <a className='nav-link' href='#'>
                      Prices
                    </a>
                  </li>
                  <li className='nav-item'>
                    <a className='nav-link' href='/html/pets.html'>
                      My Pets
                    </a>
                  </li>
                  <li className='nav-item'>
                    <a className='nav-link' href='/html/appointments.html'>
                      Appointments
                    </a>
                  </li>
                </ul>
              </div>
            </div>
            <div className='col-2'>
              <a
                className='btn btn-sm btn-outline-secondary'
                href='html/signin.html'
              >
                Sign in
              </a>
            </div>
          </div>
        </div>
      </nav>
    </header>
  )
}

export default Navbar
