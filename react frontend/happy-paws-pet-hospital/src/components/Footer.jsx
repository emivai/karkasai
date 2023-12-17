import React from 'react'
import { Link } from 'react-router-dom'

const Footer = () => (
  <footer
    className='page-footer font-small blue pt-4 bg-dark'
    style={{ color: 'white' }}
  >
    <div className='container-fluid text-center text-md-left'>
      <div className='row'>
        <div className='col-md-6 mt-md-0 mt-3'>
          <h5 className='text-uppercase'>Happy Paws</h5>
          <p>We care for pets when they need it most.</p>
        </div>

        <hr className='clearfix w-100 d-md-none pb-0' />

        <div className='col-md-3 mb-md-0 mb-3'>
          <ul className='list-unstyled'>
            <li>
              <Link to='/' className='text-white'>
                Home
              </Link>
            </li>
            <li>
              <Link to='/prices' className='text-white'>
                Pricing
              </Link>
            </li>
          </ul>
        </div>

        <div className='col-md-3 mb-md-0 mb-3'>
          <div className='mb-3'>Get in touch:</div>
          <ul className='list-unstyled' style={{ color: 'white' }}>
            <div className='mb-3'>info@happypaws.com</div>
            <div>
              123 Petal Street <br /> Kaunas, KU 45678
              <br /> Lithuania
            </div>
          </ul>
        </div>
      </div>
    </div>

    <div className='footer-copyright text-center py-3'>
      © 2023 Copyright:
      <a className='link-light' href='http://localhost:5173/'>
        {' '}
        HappyPaws.com
      </a>
    </div>
  </footer>
)

export default Footer
