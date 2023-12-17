import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import Accordion from 'react-bootstrap/Accordion'
import { Modal } from 'react-bootstrap'
import { createNote, deleteNote, editNote, getNotes } from '../reducers/note'
import {
  deleteAppointment,
  editAppointment,
  getAppointments
} from '../reducers/appointment'
import NotesForm from './NotesForm'

const Appointment = ({
  time,
  status = 'Scheduled',
  appointmentId,
  petName,
  petId,
  doctorName,
  procedures,
  notes = null
}) => {
  useEffect(() => {
    if (status === 'Done') {
      setStatusStyle('success')
    } else if (status === 'Cancelled') {
      setStatusStyle('danger')
    }
  }, [status])

  const dispatch = useDispatch()
  // const { notes } = useSelector((state) => state.note);
  const role = useSelector(state => state.auth.role)

  const [statusStyle, setStatusStyle] = useState('primary')
  const [addModalIsOpen, setAddIsOpen] = useState(false)
  const [editModalIsOpen, setEditIsOpen] = useState(false)
  const [notesForm, setNotesForm] = useState({ value: '' })

  function handleOpenAddModal () {
    setNotesForm({ value: '' })
    setAddIsOpen(true)
  }

  function handleCloseAddModal () {
    setAddIsOpen(false)
  }

  function handleOpenEditModal (note) {
    setNotesForm({ id: note.id, value: note.value })
    setEditIsOpen(true)
  }

  function handleCloseEditModal () {
    setEditIsOpen(false)
  }

  const handleChange = (id, value) => {
    setNotesForm({ ...notesForm, [id]: value })
  }

  const handleAddNotes = async () => {
    await dispatch(
      createNote({ petId, id: appointmentId, value: notesForm.value })
    )
    await dispatch(getAppointments({ id: petId }))
    handleCloseAddModal()
  }

  const handleEditNotes = async () => {
    await dispatch(
      editNote({
        petId,
        appointmentId,
        id: notesForm.id,
        value: notesForm.value
      })
    )
    await dispatch(getAppointments({ id: petId }))
    handleCloseEditModal()
  }

  const handleDeleteClick = async id => {
    await dispatch(deleteNote({ petId, appointmentId, id }))
    dispatch(getAppointments({ id: petId }))
  }

  const handleDeleteAppointmentClick = async () => {
    await dispatch(deleteAppointment({ petId, id: appointmentId }))
    dispatch(getAppointments({ id: petId }))
  }

  const handleCancelAppointmentClick = async () => {
    await dispatch(editAppointment({ petId, id: appointmentId }))
    dispatch(getAppointments({ id: petId }))
  }

  return (
    <>
      <Modal show={addModalIsOpen} onHide={handleCloseAddModal}>
        <Modal.Header closeButton>
          <Modal.Title>Add Note</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <NotesForm onChange={handleChange} formValues={notesForm} />
        </Modal.Body>
        <Modal.Footer>
          <button
            type='button'
            className='btn btn-secondary'
            onClick={handleCloseAddModal}
          >
            Close
          </button>
          <button
            type='button'
            className='btn btn-info'
            onClick={handleAddNotes}
          >
            Save Notes
          </button>
        </Modal.Footer>
      </Modal>
      <Modal show={editModalIsOpen} onHide={handleCloseEditModal}>
        <Modal.Header closeButton>
          <Modal.Title>Edit Note</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <NotesForm onChange={handleChange} formValues={notesForm} />
        </Modal.Body>
        <Modal.Footer>
          <button
            type='button'
            className='btn btn-secondary'
            onClick={handleCloseEditModal}
          >
            Close
          </button>
          <button
            type='button'
            className='btn btn-info'
            onClick={handleEditNotes}
          >
            Save Notes
          </button>
        </Modal.Footer>
      </Modal>
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
                <button
                  type='button'
                  className='btn btn-danger me-1'
                  onClick={handleCancelAppointmentClick}
                >
                  Cancel
                </button>
              )}
              {(role === 0 || role === 1) && (
                <button
                  type='button'
                  className='btn btn-danger me-1'
                  onClick={handleDeleteAppointmentClick}
                >
                  Delete
                </button>
              )}
              {(role === 0 || role === 2) && (
                <button
                  type='button'
                  className='btn btn-secondary me-1'
                  onClick={handleOpenAddModal}
                >
                  Add Note
                </button>
              )}
            </div>
            {notes && (
              <div className='col-md-12' style={{ marginTop: '10px' }}>
                <Accordion>
                  <Accordion.Item eventKey='0'>
                    <Accordion.Header>Notes</Accordion.Header>
                    <Accordion.Body>
                      {notes.map(note => (
                        <div
                          className='d-flex align-items-center'
                          key={note.id}
                        >
                          <div className='flex-grow-1'>{note.value}</div>
                          <button
                            type='button'
                            className='btn btn-secondary me-2 mb-2'
                            onClick={() => handleOpenEditModal(note)}
                          >
                            Edit
                          </button>
                          {(role === 0 || role === 2) && (
                            <button
                              type='button'
                              className='btn btn-danger mb-2'
                              onClick={() => handleDeleteClick(note.id)}
                            >
                              Delete
                            </button>
                          )}
                        </div>
                      ))}
                    </Accordion.Body>
                  </Accordion.Item>
                </Accordion>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  )
}

export default Appointment
