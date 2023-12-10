import React, { useState, useEffect } from "react";
import Appointment from "../components/Appointment";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import MultiSelectDropdown from "../components/MultiSelectDropdown";
import SelectDropdown from "../components/SelectDropdown";
import { useLocation } from "react-router-dom";
import AppointmentForm from "../components/AppointmentForm";
import { useDispatch, useSelector } from "react-redux";
import { createAppointment, getAppointments } from "../reducers/appointment";
import { getUser } from "../reducers/user";
import { createAppointmentProcedure } from "../reducers/appointmentprocedure";

const Appointments = () => {
  const { state } = useLocation();
  const [modalIsOpen, setIsOpen] = useState(false);
  const [createValues, setCreateValues] = useState({ petId: state.id });
  const { appointments } = useSelector((state) => state.appointment);
  const dispatch = useDispatch();
  const users = useSelector((state) => state.user);

  function handleOpenModal() {
    setIsOpen(true);
  }

  function handleCloseModal() {
    setIsOpen(false);
  }

  const handleFormChange = (id, value) => {
    setCreateValues({ ...createValues, [id]: value });
  };

  const handleBookClick = async () => {
    await dispatch(
      createAppointment({
        petId: createValues.petId,
        timeSlotId: createValues.timeslot,
        procedureIds: createValues.procedureId,
      })
    );
    handleCloseModal();
  };

  useEffect(() => {
    dispatch(getAppointments({ id: state.id }));
    dispatch(getUser({ id: state.userId }));
  }, [dispatch]);

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
          <Modal.Title> New Appointment</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <AppointmentForm
            pet={state}
            onChange={handleFormChange}
            formValues={createValues}
          />
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
            onClick={handleBookClick}
          >
            Book Appointment
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
        {/* <div className='row mt-3'>
          {appointments &&
            appointments.map(appointment => (
              <Appointment
                key={appointment.id} // Make sure to provide a unique key
                time={`${appointment.startTime} - ${appointment.endTime}`}
                status={appointment.status}
                petName={appointment.petName}
                doctorName={appointment.doctorName}
                procedures={appointment.procedures}
              />
            ))}
        </div> */}
      </div>
    </>
  );
};

export default Appointments;
