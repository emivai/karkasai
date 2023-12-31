import React, { useState } from "react";
import Appointment from "../components/Appointment";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import MultiSelectDropdown from "../components/MultiSelectDropdown";
import SelectDropdown from "../components/SelectDropdown";

const Appointments = () => {
  const [modalIsOpen, setIsOpen] = useState(false);

  function handleOpenModal() {
    setIsOpen(true);
  }

  function handleCloseModal() {
    setIsOpen(false);
  }

  return (
    <>
      <div className="text-center mt-5">
        <button
          type="button"
          className="btn btn-info btn-lg"
          onClick={handleOpenModal}
        >
          Schedule New Appointment
        </button>
      </div>
      <Modal show={modalIsOpen} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title> New Apointment</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className="my-4">
            <SelectDropdown
              header={"Pets"}
              options={["Nancy", "Smudge", "Oreo"]}
            />
            <SelectDropdown
              header={"Doctor"}
              options={["Sarah Smith", "Ann Jones", "Liam Herwig"]}
            />
            <MultiSelectDropdown />
            <SelectDropdown
              header={"Time"}
              options={[
                "2023-10-12 12:00-13:00",
                "2023-10-12 13:00-14:00",
                "2023-10-12 15:30-16:30",
              ]}
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
            onClick={handleCloseModal}
          >
            Save changes
          </button>
        </Modal.Footer>
      </Modal>
      <div className="container d-flex align-items-center">
        <div className="row mt-3">
          <Appointment
            time={"2023-11-25 13:00-14:00"}
            status={"Scheduled"}
            petName={"Nancy"}
            doctorName={"Sarah Smith"}
            procedures={
              "Procedures: Feline infectious peritonitis (FIP) vaccination"
            }
          />
          <Appointment
            time={"2023-10-30 10:30-11:30"}
            status={"Scheduled"}
            petName={"Oreo"}
            doctorName={"Lily Anderson"}
            procedures={"Dental extraction"}
          />
          <Appointment
            time={"2022-09-13 12:30-13:30"}
            status={"Cancelled"}
            petName={"Dave"}
            doctorName={"Liam Herwig"}
            procedures={"Wellness exam"}
          />
          <Appointment
            time={"2022-09-13 12:30-13:30"}
            status={"Cancelled"}
            petName={"Dave"}
            doctorName={"Liam Herwig"}
            procedures={"Wellness exam"}
          />
          <Appointment
            time={"2022-09-13 12:30-13:30"}
            status={"Done"}
            petName={"Smudge"}
            doctorName={"Liam Herwig"}
            procedures={"Wellness exam, Nail trim"}
          />
        </div>
      </div>
    </>
  );
};

export default Appointments;
