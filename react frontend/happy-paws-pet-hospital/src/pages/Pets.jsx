import React, { useEffect, useState } from "react";
import PetProfile from "../components/PetProfile";
import { useDispatch, useSelector } from "react-redux";
import { createPet, getPets } from "../reducers/pet";
import { ListGroup, Modal } from "react-bootstrap"; // Assuming you are using react-bootstrap for modals
import SelectDropdown from "../components/SelectDropdown";
import { Link } from "react-router-dom";

const petType = {
  0: "Dog",
  1: "Cat",
  2: "Rodent",
  3: "Exotic",
};

const Pets = () => {
  const dispatch = useDispatch();
  const { pets } = useSelector((state) => state.pet);
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const handleOpenModal = () => {
    setModalIsOpen(true);
  };

  const handleCloseModal = () => {
    setModalIsOpen(false);
  };

  const [registerValue, setRegisterValue] = useState({
    name: "",
    type: 0,
    birthdate: null,
    photo: "",
  });

  function handleRegisterChange(v) {
    const { id, value } = v.target;
    setRegisterValue({ ...registerValue, [id]: value });
  }

  function handleNumericChange(v) {
    const { id, value } = v.target;
    setRegisterValue({ ...registerValue, [id]: Number(value - 1) });
  }

  function handleDateChange(v) {
    const { id, value } = v.target;
    setRegisterValue({ ...registerValue, [id]: new Date(value).toISOString() });
  }
  const handleRegisterClick = async () => {
    await dispatch(createPet(registerValue));
    handleCloseModal();
    refreshPets();
  };

  function refreshPets() {
    dispatch(getPets());
  }

  useEffect(() => {
    dispatch(getPets());
  }, [dispatch]);

  return (
    <div className="container marketing pt-5 d-flex flex-column">
      <div className="mb-5 mx-auto align-self-start">
        <button className="btn btn-info btn-lg" onClick={handleOpenModal}>
          Add pet
        </button>
      </div>

      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Pet</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className="my-4">
            <SelectDropdown
              header={"Type"}
              formId="type"
              options={["Dog", "Cat", "Rodent", "Exotic"]}
              handleChange={handleNumericChange}
            />
            <label className="sr-only">Name</label>
            <input
              type="text"
              id="name"
              className="form-control"
              placeholder="Enter pet's name"
              required
              autoFocus
              onChange={(e) => handleRegisterChange(e)}
            />
            <label>BirthDate</label>
            <input
              type="date"
              id="birthdate"
              autoFocus
              className="form-control"
              onChange={(e) => handleDateChange(e)}
            />
            <label className="sr-only">Photo</label>
            <input
              type="text"
              id="photo"
              className="form-control"
              placeholder="Photo url"
              required
              autoFocus
              onChange={(e) => handleRegisterChange(e)}
            />
          </form>
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
            onClick={handleRegisterClick}
          >
            Save changes
          </button>
        </Modal.Footer>
      </Modal>

      <div className="row">
        <ListGroup>
          {pets?.map((pet) => (
            <PetProfile
              key={pet.id}
              title={pet.name}
              birthdate={new Date(pet.birthdate).toLocaleDateString("lt-LT")}
              type={pet.type}
              imageSource={pet.photo}
            />
          ))}
        </ListGroup>
      </div>
    </div>
  );
};

export default Pets;
