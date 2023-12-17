import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import Accordion from 'react-bootstrap/Accordion'

const Appointment = ({
  time,
  status = 'Scheduled',
  petName,
  doctorName,
  procedures
}) => {
  useEffect(() => {
    if (status === 'Done') {
      setStatusStyle('success')
    } else if (status === 'Cancelled') {
      setStatusStyle('danger')
    }
  }, [])

  const [statusStyle, setStatusStyle] = useState('primary')
  const role = useSelector(state => state.auth.role)

  return (
    <div className='col-12 mt-3'>
      <div className='card'>
        <div className='card-body'>
          <h5 className='card-title'>{time}</h5>
          <p className={`badge bg-${statusStyle}`}>{status}</p>
          <p className='card-text'>Pet: {petName}</p>
          <p className='card-text'>Doctor: {doctorName}</p>
          <p className='card-text'>Procedures: {procedures}</p>
          <div className='btn-toolbar d-flex gap-1' role='toolbar'>
            {status === 'Scheduled' && (
              <button type='button' className='btn btn-secondary mr-2'>
                Edit
              </button>
            )}
            {(role === 0 || role === 1) && (
              <button type='button' className='btn btn-danger mr-2'>
                Delete
              </button>
            )}
          </div>
          <div className='col-md-12' style={{ marginTop: '10px' }}>
            <Accordion>
              <Accordion.Item eventKey='0'>
                <Accordion.Header>Notes</Accordion.Header>
                <Accordion.Body>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat. Duis aute
                  irure dolor in reprehenderit in voluptate velit esse cillum
                  dolore eu fugiat nulla pariatur. Excepteur sint occaecat
                  cupidatat non proident, sunt in culpa qui officia deserunt
                  mollit anim id est laborum.
                </Accordion.Body>
              </Accordion.Item>
            </Accordion>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Appointment
