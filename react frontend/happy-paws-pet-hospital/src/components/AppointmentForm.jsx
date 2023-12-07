import React, { useEffect, useState } from "react";
import Appointment from "../components/Appointment";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import MultiSelectDropdown from "../components/MultiSelectDropdown";
import SelectDropdown from "../components/SelectDropdown";
import { useDispatch, useSelector } from "react-redux";
import { getDoctors } from "../reducers/user";
import { Form } from "react-bootstrap";
import { getTimeslots, getTimeslotsByDoctorId } from "../reducers/timeslot";
import { getProcedures } from "../reducers/procedure";

const AppointmentForm = ({ pet, onChange, formValues }) => {
  const dispatch = useDispatch();

  const { doctors } = useSelector((state) => state.user);
  const { timeslots } = useSelector((state) => state.timeslot);
  const { procedures } = useSelector((state) => state.procedure);

  function handleChange(v) {
    const { id, value } = v.target;
    onChange(id, value);
  }

  function handleNumericChange(v) {
    console.log(v);
    const { id, value } = v.target;
    onChange(id, Number(value));
  }

  function handleDateChange(v) {
    const { id, value } = v.target;
    onChange(id, new Date(value).toISOString());
  }

  useEffect(() => {
    dispatch(getDoctors());
    dispatch(getProcedures());
  }, [dispatch]);

  useEffect(() => {
    if (formValues.doctor) {
      dispatch(getTimeslotsByDoctorId(formValues.doctor));
    }
  }, [dispatch, formValues.doctor]);

  console.log(formValues);
  return (
    <form className="my-4">
      <SelectDropdown header={"Pets"} options={[pet?.name]} disabled={true} />
      {doctors && (
        <div className="form-group row mb-2">
          <div>
            <label>{"Doctor"}</label>
            <Form.Select
              onChange={handleChange}
              id="doctor"
              value={formValues.doctor}
            >
              <option>Select doctor</option>
              {doctors.map((doctor) => (
                <option key={doctor.id} value={doctor.id}>
                  {doctor.name + " " + doctor.surname}
                </option>
              ))}
            </Form.Select>
          </div>
        </div>
      )}

      <MultiSelectDropdown
        options={procedures}
        formValues={formValues}
        onChange={onChange}
      />
      {formValues.doctor && timeslots && (
        <div className="form-group row mb-2">
          <div>
            <label>{"Doctor"}</label>
            <Form.Select
              onChange={handleChange}
              id="timeslot"
              value={formValues.timeslot}
            >
              <option>Select timeslot</option>
              {timeslots.map((timeslot) => (
                <option key={timeslot.id} value={timeslot.id}>
                  {timeslot.start + " " + timeslot.end}
                </option>
              ))}
            </Form.Select>
          </div>
        </div>
      )}
    </form>
  );
};

export default AppointmentForm;
