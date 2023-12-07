import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { ListGroup, Modal } from 'react-bootstrap'
import { createProcedure, getProcedures } from '../reducers/procedure'
import PriceForm from '../components/PriceForm'
import PriceProfile from '../components/PriceProfile'

const Prices = () => {
  const dispatch = useDispatch()
  const { procedures } = useSelector(state => state.procedure)
  const [modalIsOpen, setModalIsOpen] = useState(false)

  const handleOpenModal = () => {
    setModalIsOpen(true)
  }

  const handleCloseModal = () => {
    setModalIsOpen(false)
  }

  const [registerValue, setRegisterValue] = useState({
    name: '',
    price: 0.0,
    description: ''
  })

  const handleRegisterClick = async () => {
    await dispatch(createProcedure(registerValue))
    handleCloseModal()
    dispatch(getProcedures())
  }

  const handleFormChange = (id, value) => {
    setRegisterValue({ ...registerValue, [id]: value })
  }

  useEffect(() => {
    dispatch(getProcedures())
  }, [dispatch])

  return (
    <div className='container marketing pt-5 d-flex flex-column'>
      <div className='mb-2 align-self-start'>
        <button className='btn btn-info btn-lg' onClick={handleOpenModal}>
          Add procedure
        </button>
      </div>

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Procedure</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <PriceForm onChange={handleFormChange} formValues={registerValue} />
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
            Register Procedure
          </button>
        </Modal.Footer>
      </Modal>

      <div className='row'>
        <ListGroup>
          {procedures?.map(procedure => (
            <PriceProfile key={procedure.id} procedure={procedure} />
          ))}
        </ListGroup>
      </div>
    </div>
  )
}

export default Prices
