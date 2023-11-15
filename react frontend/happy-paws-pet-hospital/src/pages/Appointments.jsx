import React from 'react'
import Appointment from '../components/Appointment'

const Appointments = () => {
  return (
    <>
      <div className='container d-flex align-items-center'>
        <div className='row mt-3'>
          <Appointment
            time={'2023-11-25 13:00-14:00'}
            status={'Scheduled'}
            petName={'Nancy'}
            doctorName={'Sarah Smith'}
            procedures={
              'Procedures: Feline infectious peritonitis (FIP) vaccination'
            }
          />
          <Appointment
            time={'2023-10-30 10:30-11:30'}
            status={'Scheduled'}
            petName={'Oreo'}
            doctorName={'Lily Anderson'}
            procedures={'Dental extraction'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Cancelled'}
            petName={'Dave'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Cancelled'}
            petName={'Dave'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam'}
          />
          <Appointment
            time={'2022-09-13 12:30-13:30'}
            status={'Done'}
            petName={'Smudge'}
            doctorName={'Liam Herwig'}
            procedures={'Wellness exam, Nail trim'}
          />
        </div>
      </div>
    </>
  )
}

export default Appointments
