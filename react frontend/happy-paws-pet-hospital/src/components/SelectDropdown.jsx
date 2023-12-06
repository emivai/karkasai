import React from "react";
import { Form } from "react-bootstrap";

const SelectDropdown = ({ header, options, handleChange, formId }) => {
  return (
    <div className="form-group row mb-2">
      <div>
        <label>{header}</label>
        <Form.Select onChange={handleChange} id={formId}>
          {options.map((option, index) => (
            <option key={option} value={index + 1}>
              {option}
            </option>
          ))}
        </Form.Select>
      </div>
    </div>
  );
};

export default SelectDropdown;
