import { useEffect, useState } from 'react'
import PetProfile from '../components/PetProfile'
import { useDispatch, useSelector } from 'react-redux'
import { createPet, getPets } from '../reducers/pet'
import { ListGroup, Modal } from 'react-bootstrap'
import PetForm from '../components/PetForm'

const Pets = () => {
  const dispatch = useDispatch()
  const { pets } = useSelector(state => state.pet)
  const [modalIsOpen, setModalIsOpen] = useState(false)
  const role = useSelector(state => state.auth.role)

  const handleOpenModal = () => {
    setModalIsOpen(true)
  }

  const handleCloseModal = () => {
    setModalIsOpen(false)
  }

  const [registerValue, setRegisterValue] = useState({
    name: '',
    type: 0,
    birthdate: new Date(),
    photo: ''
  })

  const handleRegisterClick = async () => {
    await dispatch(createPet(registerValue))
    handleCloseModal()
    dispatch(getPets())
  }

  const handleFormChange = (id, value) => {
    setRegisterValue({ ...registerValue, [id]: value })
  }

  useEffect(() => {
    dispatch(getPets())
  }, [dispatch])

  return (
    <div
      className='container marketing pt-5 d-flex flex-column'
      style={{ marginBottom: '50px' }}
    >
      {(role === 0 || role === 1) && (
        <div className='mb-2 align-self-start'>
          <button className='btn btn-info btn-lg' onClick={handleOpenModal}>
            Add pet
          </button>
        </div>
      )}

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Pet</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <PetForm onChange={handleFormChange} formValues={registerValue} />
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
            Register Pet
          </button>
        </Modal.Footer>
      </Modal>

      <div className='row'>
        <ListGroup>
          {pets?.map(pet => (
            <PetProfile key={pet.id} pet={pet} />
          ))}
        </ListGroup>
      </div>
    </div>
  )
}

export default Pets
