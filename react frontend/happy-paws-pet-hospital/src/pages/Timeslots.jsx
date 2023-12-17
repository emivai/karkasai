import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { ListGroup, Modal } from 'react-bootstrap'
import { createTimeslot, getTimeslots } from '../reducers/timeslot'
import { getDoctors } from '../reducers/user'
import TimeslotForm from '../components/TimeslotForm'
import TimeslotProfile from '../components/TimeSlotProfile'

const Timeslots = () => {
  const dispatch = useDispatch()

  const role = useSelector(state => state.auth.role)
  const { timeslots } = useSelector(state => state.timeslot)
  const [modalIsOpen, setModalIsOpen] = useState(false)

  function handleRegisterChange (v) {
    const { id, value } = v.target
    setRegisterValue({ ...registerValue, [id]: value })
  }

  const handleRegisterClick = async () => {
    await dispatch(createTimeslot(registerValue))
    handleCloseModal()
    refreshTimeslots()
  }

  const handleOpenModal = () => {
    setModalIsOpen(true)
  }

  const handleCloseModal = () => {
    setModalIsOpen(false)
  }

  const handleFormChange = (id, value) => {
    setRegisterValue({ ...registerValue, [id]: value })
  }

  function handleDateChange (v) {
    const { id, value } = v.target
    setRegisterValue({ ...registerValue, [id]: new Date(value).toISOString() })
  }

  function handleNumericChange (v) {
    const { id, value } = v.target
    setRegisterValue({ ...registerValue, [id]: Number(value - 1) })
  }

  function refreshTimeslots () {
    dispatch(getTimeslots())
  }

  useEffect(() => {
    dispatch(getTimeslots())
  }, [dispatch])

  const [registerValue, setRegisterValue] = useState({
    start: null,
    end: null,
    doctor: ''
  })

  return (
    <div
      className='container marketing pt-5 d-flex flex-column'
      style={{ marginBottom: '50px' }}
    >
      {role === 2 && (
        <div className='mb-2 align-self-start'>
          <button className='btn btn-info btn-lg' onClick={handleOpenModal}>
            Add timeslot
          </button>
        </div>
      )}

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Timeslot</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <TimeslotForm
            onChange={handleFormChange}
            formValues={registerValue}
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
            onClick={handleRegisterClick}
          >
            Register Timeslot
          </button>
        </Modal.Footer>
      </Modal>

      <div className='row'>
        <ListGroup>
          {timeslots?.map(timeslot => (
            <TimeslotProfile key={timeslot.id} timeslot={timeslot} />
          ))}
        </ListGroup>
      </div>
    </div>
  )
}

export default Timeslots
