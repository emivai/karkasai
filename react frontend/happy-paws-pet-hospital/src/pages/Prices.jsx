import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { ListGroup, Modal } from 'react-bootstrap'
import {
  createProcedure,
  getProcedures,
  deleteProcedure,
  editProcedure
} from '../reducers/procedure'

const Prices = () => {
  const dispatch = useDispatch()
  const { procedures } = useSelector(state => state.procedure)
  const [modalIsOpen, setModalIsOpen] = useState(false)

  function handleRegisterChange (v) {
    const { id, value } = v.target
    setRegisterValue({ ...registerValue, [id]: value })
  }

  const handleRegisterClick = async () => {
    await dispatch(createProcedure(registerValue))
    handleCloseModal()
    refreshProcedures()
  }

  const handleOpenModal = () => {
    setModalIsOpen(true)
  }

  const handleCloseModal = () => {
    setModalIsOpen(false)
  }

  function refreshProcedures () {
    dispatch(getProcedures())
  }

  const handleEditClick = async id => {
    await dispatch(editProcedure({ id: id, value: editValue }))
    handleCloseModal()
    dispatch(getProcedures())
  }

  const handleDeleteClick = async id => {
    await dispatch(deleteProcedure(id))
    dispatch(getProcedures())
  }

  useEffect(() => {
    dispatch(getProcedures())
  }, [dispatch])

  const [registerValue, setRegisterValue] = useState({
    name: '',
    price: 0,
    description: ''
  })

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='mb-5 mx-auto align-self-start'>
        <button className='btn btn-info btn-lg' onClick={handleOpenModal}>
          Add procedure
        </button>
      </div>

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>New Procedure</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className='my-4'>
            <label className='sr-only'>Name</label>
            <input
              type='text'
              id='name'
              className='form-control'
              placeholder="Enter procedure's name"
              required
              autoFocus
              onChange={e => handleRegisterChange(e)}
            />
            <label className='sr-only'>Price</label>
            <input
              type='text'
              id='price'
              className='form-control'
              placeholder='Example: 20'
              required
              autoFocus
              onChange={e => handleRegisterChange(e)}
            />
            <label className='sr-only'>Description</label>
            <input
              type='text'
              id='description'
              className='form-control'
              placeholder='Enter description'
              required
              autoFocus
              onChange={e => handleRegisterChange(e)}
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
          {procedures?.map(procedure => (
            <ListGroup.Item key={procedure.id}>
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
          ))}
        </ListGroup>
      </div>
    </div>
  )
}

export default Prices
