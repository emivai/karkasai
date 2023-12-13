import React, { useState, useEffect } from 'react'
import Appointment from '../components/Appointment'
import Modal from 'react-bootstrap/Modal'
import Button from 'react-bootstrap/Button'
import MultiSelectDropdown from '../components/MultiSelectDropdown'
import SelectDropdown from '../components/SelectDropdown'
import { useLocation } from 'react-router-dom'
import AppointmentForm from '../components/AppointmentForm'
import { useDispatch, useSelector } from 'react-redux'
import { createAppointment, getAppointments } from '../reducers/appointment'
import { getUser } from '../reducers/user'
import { createAppointmentProcedure } from '../reducers/appointmentprocedure'

const appointmentStatus = {
  0: 'Scheduled',
  1: 'Cancelled',
  2: 'Done'
}

const Appointments = () => {
  const { state } = useLocation()
  const [modalIsOpen, setIsOpen] = useState(false)
  const [createValues, setCreateValues] = useState({ petId: state.id })
  const { appointments } = useSelector(state => state.appointment)
  const dispatch = useDispatch()
  const role = useSelector(state => state.auth.role)

  function handleOpenModal () {
    setIsOpen(true)
  }

  function handleCloseModal () {
    setIsOpen(false)
  }

  const handleFormChange = (id, value) => {
    setCreateValues({ ...createValues, [id]: value })
  }

  const getAppointmentStatus = value => {
    return appointmentStatus[value]
  }

  const handleBookClick = async () => {
    await dispatch(
      createAppointment({
        petId: createValues.petId,
        timeSlotId: createValues.timeslot,
        procedureIds: createValues.procedureId
      })
    )
    handleCloseModal()
    dispatch(getAppointments({ id: state.id }))
  }

  useEffect(() => {
    dispatch(getAppointments({ id: state.id }))
  }, [dispatch])

  return (
    <>
      <div className='text-center mt-5'>
        {(role === 1 || role === 0) && (
          <button
            type='button'
            className='btn btn-info btn-lg'
            onClick={handleOpenModal}
          >
            Schedule New Appointment
          </button>
        )}
      </div>
      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Appointment</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AppointmentForm
            pet={state}
            onChange={handleFormChange}
            formValues={createValues}
          />
        </Modal.Body>
        <Modal.Footer>
          <button
            type='button'
            className='btn btn-secondary'
            onClick={handleCloseModal}
          >
            Close
          </button>
          <button
            type='button'
            className='btn btn-info'
            onClick={handleBookClick}
          >
            Book Appointment
          </button>
        </Modal.Footer>
      </Modal>
      <div className='container d-flex align-items-center'>
        <div className='row mt-3'>
          {appointments?.map(appointment => (
            <Appointment
              key={appointment.id}
              time={
                new Date(appointment.timeSlot.start)
                  .toISOString()
                  .slice(0, 16)
                  .replace('T', ' ') +
                ' | ' +
                new Date(appointment.timeSlot.end)
                  .toISOString()
                  .slice(0, 16)
                  .replace('T', ' ')
              }
              status={getAppointmentStatus(appointment.status)}
              petName={appointment.pet.name}
              doctorName={`${appointment.timeSlot.doctor.name} ${appointment.timeSlot.doctor.surname}`}
              procedures={appointment.appointmentProcedures
                .map(procedure => procedure.procedure.name)
                .join(', ')}
            />
          ))}
        </div>
      </div>
    </>
  )
}

export default Appointments
