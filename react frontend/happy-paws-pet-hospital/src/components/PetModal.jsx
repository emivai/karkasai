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

const PetModal = (modalState, handleCloseModal, pet = null) => {
    const dispatch = useDispatch();

    const [registerValue, setRegisterValue] = useState({
        name: pet?.name && "",
        type: pet?.type && 0,
        birthdate: pet?.birthdate && null,
        photo: pet?.photo && "",
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

  return (
    <Modal show={modalState} onHide={() => handleCloseModal()}>
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
  )
}

export default PetModal