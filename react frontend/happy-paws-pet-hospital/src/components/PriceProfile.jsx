import React, { useState, useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { ListGroup } from 'react-bootstrap'
import { Modal } from 'react-bootstrap'
import {
  editProcedure,
  deleteProcedure,
  getProcedures
} from '../reducers/procedure'
import PriceForm from './PriceForm'

const PriceProfile = ({ procedure }) => {
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
    await dispatch(editProcedure({ id: procedure.id, value: editValue }))
    handleCloseModal()
    dispatch(getProcedures())
  }

  const handleDeleteClick = async id => {
    await dispatch(deleteProcedure(id))
    dispatch(getProcedures())
  }

  useEffect(() => {
    if (procedure) {
      setEditValue({
        name: procedure.name,
        price: procedure.price,
        description: procedure.description
      })
    }
  }, [procedure])

  return (
    <ListGroup.Item>
      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> Edit Procedure</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <PriceForm onChange={handleFormChange} formValues={editValue} />
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
          <h2>{procedure.name}</h2>
          <div>Price: {procedure.price} eur</div>
          <div>{procedure.description}</div>
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
          onClick={() => handleDeleteClick(procedure.id)}
        >
          Delete
        </button>
      </div>
    </ListGroup.Item>
  )
}

export default PriceProfile
