import React, { useState, useEffect } from "react";
import { useDispatch } from "react-redux";
import { ListGroup } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { Modal } from "react-bootstrap";
import { editPet, deletePet, getPets } from "../reducers/pet";
import PetForm from "./PetForm";

const petType = {
  0: "Dog",
  1: "Cat",
  2: "Rodent",
  3: "Exotic",
};

const PetProfile = ({ pet }) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const [editValue, setEditValue] = useState();
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const handleOpenModal = () => {
    setModalIsOpen(true);
  };

  const handleCloseModal = () => {
    setModalIsOpen(false);
  };
  const getPetType = (value) => {
    return petType[value];
  };

  const handleFormChange = (id, value) => {
    setEditValue({ ...editValue, [id]: value });
  };

  const handleEditClick = async () => {
    await dispatch(editPet({ id: pet.id, value: editValue }));
    handleCloseModal();
    dispatch(getPets());
  };

  const handleDeleteClick = async () => {
    await dispatch(deletePet(pet.id));
    dispatch(getPets());
  };

  const handleAppointmentsClick = () => {
    navigate("/appointments", { state: pet });
  };

  useEffect(() => {
    if (pet) {
      setEditValue({
        name: pet.name,
        type: pet.type,
        birthdate: pet.birthdate,
        photo: pet.photo,
      });
    }
  }, [pet]);

  return (
    <ListGroup.Item>
      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> Edit Pet</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <PetForm onChange={handleFormChange} formValues={editValue} />
        </Modal.Body>
        <Modal.Footer>
          <button
            type="button"
            className="btn btn-secondary"
            onClick={handleCloseModal}
          >
            Close
          </button>
          <button
            type="button"
            className="btn btn-info"
            onClick={handleEditClick}
          >
            Save changes
          </button>
        </Modal.Footer>
      </Modal>
      <div className="d-flex flex-row align-items-center gap-3">
        <img
          className="rounded-circle image"
          src={pet?.photo}
          width="80"
          height="80"
        />
        <div className="flex-grow-1">
          <h2>{pet?.name}</h2>
          <div>{getPetType(pet?.type)}</div>
          {new Date(pet?.birthdate).toLocaleDateString("lt-LT")}
        </div>
        <button
          type="button"
          className="btn btn-sm me-3"
          onClick={handleAppointmentsClick}
        >
          Appointments
        </button>
        <button
          type="button"
          className="btn btn-sm me-3"
          onClick={handleOpenModal}
        >
          Edit
        </button>
        <button
          type="button"
          className="btn btn-sm btn-danger me-3"
          onClick={handleDeleteClick}
        >
          Delete
        </button>
      </div>
    </ListGroup.Item>
  );
};

export default PetProfile;
