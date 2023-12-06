import React, { useEffect, useState } from "react";
import PetProfile from "../components/PetProfile";
import { useDispatch, useSelector } from "react-redux";
import { createPet, getPets } from "../reducers/pet";
import { ListGroup, Modal } from "react-bootstrap"; // Assuming you are using react-bootstrap for modals
import SelectDropdown from "../components/SelectDropdown";
import { Link } from "react-router-dom";

const PetForm = ({ onChange, formValues }) => {
  function handleRegisterChange(v) {
    const { id, value } = v.target;
    onChange(id, value);
  }

  function handleNumericChange(v) {
    const { id, value } = v.target;
    onChange(id, Number(value));
  }

  function handleDateChange(v) {
    const { id, value } = v.target;
    onChange(id, new Date(value).toISOString());
  }

  return (
    <form className="my-4">
      <SelectDropdown
        header={"Type"}
        formId="type"
        options={["Dog", "Cat", "Rodent", "Exotic"]}
        value={formValues.type ?? 0}
        handleChange={handleNumericChange}
      />
      <label className="sr-only">Name</label>
      <input
        type="text"
        id="name"
        className="form-control"
        placeholder="Enter pet's name"
        required
        value={formValues.name ?? ""}
        onChange={(e) => handleRegisterChange(e)}
      />
      <label>BirthDate</label>
      <input
        type="date"
        id="birthdate"
        className="form-control"
        value={new Date(formValues.birthdate).toLocaleDateString("lt-LT")}
        onChange={(e) => handleDateChange(e)}
      />
      <label className="sr-only">Photo</label>
      <input
        type="text"
        id="photo"
        className="form-control"
        placeholder="Photo url"
        required
        value={formValues.photo ?? ""}
        onChange={(e) => handleRegisterChange(e)}
      />
    </form>
  );
};

export default PetForm;
