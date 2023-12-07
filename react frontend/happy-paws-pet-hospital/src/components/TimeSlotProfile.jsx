import React, { useState, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { ListGroup } from 'react-bootstrap'
import { Modal } from 'react-bootstrap'
import {
  editTimeslot,
  deleteTimeslot,
  getTimeslots
} from '../reducers/timeslot'
import TimeslotForm from './TimeslotForm'

const TimeslotProfile = ({ timeslot }) => {
  const dispatch = useDispatch()

  const [editValue, setEditValue] = useState()
  const [modalIsOpen, setModalIsOpen] = useState(false)

  const handleOpenModal = () => {
    setModalIsOpen(true)
  }

  const handleCloseModal = () => {
    setModalIsOpen(false)
  }

  const handleFormChange = (id, value) => {
    setEditValue({ ...editValue, [id]: value })
  }

  const handleEditClick = async () => {
    await dispatch(editTimeslot({ id: timeslot.id, value: editValue }))
    handleCloseModal()
    dispatch(getTimeslots())
  }

  const handleDeleteClick = async id => {
    await dispatch(deleteTimeslot(id))
    dispatch(getTimeslots())
  }

  useEffect(() => {
    if (timeslot) {
      setEditValue({
        start: timeslot.start,
        end: timeslot.end,
        doctor: timeslot.doctor
      })
    }
  }, [timeslot])

  return (
    <ListGroup.Item>
      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> Edit Timeslot</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <TimeslotForm onChange={handleFormChange} formValues={editValue} />
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
            onClick={handleEditClick}
          >
            Save changes
          </button>
        </Modal.Footer>
      </Modal>
      <div className='d-flex flex-row align-items-center gap-3'>
        <div className='flex-grow-1'>
          <h2>{new Date(timeslot.start).toLocaleDateString('lt-LT')}</h2>
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
        <button
          type='button'
          className='btn btn-sm me-3'
          onClick={handleOpenModal}
        >
          Edit
        </button>
        <button
          type='button'
          className='btn btn-sm btn-danger me-3'
          onClick={() => handleDeleteClick(timeslot.id)}
        >
          Delete
        </button>
      </div>
    </ListGroup.Item>
  )
}

export default TimeslotProfile
