import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { ListGroup, Modal } from 'react-bootstrap'
import { createTimeslot, getTimeslots } from '../reducers/timeslot'
import { getDoctors } from '../reducers/user'
import SelectDropdown from '../components/SelectDropdown'

const Timeslots = () => {
  const dispatch = useDispatch()
  const { timeslots } = useSelector(state => state.timeslot)
  const { doctors } = useSelector(state => state.user)
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
    dispatch(getDoctors())
  }, [dispatch])

  const [registerValue, setRegisterValue] = useState({
    start: null,
    end: null,
    doctor: ''
  })

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='mb-5 mx-auto align-self-start'>
        <button className='btn btn-info btn-lg' onClick={handleOpenModal}>
          Add timeslot
        </button>
      </div>

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>New timeslot</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className='my-4'>
            <label>Start</label>
            <input
              type='datetime-local'
              id='start'
              autoFocus
              className='form-control'
              onChange={e => handleDateChange(e)}
            />
            <label>End</label>
            <input
              type='datetime-local'
              id='end'
              autoFocus
              className='form-control'
              onChange={e => handleDateChange(e)}
            />
            <SelectDropdown
              header={'Doctor'}
              formId='doctor'
              options={['Placeholder', 'Name Surname']}
              handleChange={handleNumericChange}
            />
          </form>
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
            Save changes
          </button>
        </Modal.Footer>
      </Modal>

      <div className='row'>
        <ListGroup>
          {timeslots?.map(timeslot => (
            <ListGroup.Item key={timeslot.id}>
              <div className='d-flex flex-row align-items-center gap-3'>
                <div className='flex-grow-1'>
                  <h2>
                    {new Date(timeslot.start).toLocaleDateString('lt-LT')}
                  </h2>
                  <div>
                    {new Date(timeslot.start).toLocaleTimeString('lt-LT', {
                      hour: '2-digit',
                      minute: '2-digit'
                    })}{' '}
                    -{' '}
                    {new Date(timeslot.end).toLocaleTimeString('lt-LT', {
                      hour: '2-digit',
                      minute: '2-digit'
                    })}
                  </div>
                  <div>Doctor Name</div>
                </div>
                <button type='button' className='btn btn-sm me-3'>
                  Edit
                </button>
                <button type='button' className='btn btn-sm btn-danger me-3'>
                  Delete
                </button>
              </div>
            </ListGroup.Item>
          ))}
        </ListGroup>
      </div>
    </div>
  )
}

export default Timeslots
