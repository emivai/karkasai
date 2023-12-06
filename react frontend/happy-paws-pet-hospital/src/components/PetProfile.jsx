import React, {useState} from 'react'
import { ListGroup } from "react-bootstrap";
import { Link } from "react-router-dom";
import PetModal from './PetModal';

const petType = {
  0: "Dog",
  1: "Cat",
  2: "Rodent",
  3: "Exotic",
};

const PetProfile = ({ imageSource, title, birthdate, type }) => {

  const [modalIsOpen, setModalIsOpen] = useState(false);

  const handleOpenModal = () => {
    setModalIsOpen(true);
  };

  const handleCloseModal = () => {
    setModalIsOpen(false);
  }
  const getPetType = (value) => {
    return petType[value];
  };

  return (
    <ListGroup.Item>
      {/* <PetModal modalState={modalIsOpen} handleCloseModal={() => handleCloseModal()} /> */}
    <div className="d-flex flex-row align-items-center gap-3">
      <img
        className="rounded-circle image"
        src={imageSource}
        width="80"
        height="80"
      />
      <div className="flex-grow-1">
        <h2>{title}</h2>
        <div>{getPetType(type)}</div>
        {new Date(birthdate).toLocaleDateString("lt-LT")}
      </div>
      <button type="button" className="btn btn-sm btn-link me-3">
        <Link to="/appointments">Appointments</Link>
      </button>
      <button type="button" className="btn btn-sm me-3">
        Edit
      </button>
      <button type="button" className="btn btn-sm btn-danger me-3">
        Delete
      </button>
    </div>
  </ListGroup.Item>
  )
}

export default PetProfile
