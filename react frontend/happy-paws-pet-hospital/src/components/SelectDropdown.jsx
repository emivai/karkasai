import React from "react";
import { Form } from "react-bootstrap";

const SelectDropdown = ({
  header,
  options,
  value,
  handleChange,
  formId,
  disabled,
}) => {
  return (
    <div className="form-group row mb-2">
      <div>
        <label>{header}</label>
        <Form.Select
          onChange={handleChange}
          id={formId}
          value={value}
          disabled={disabled}
        >
          {options.map((option, index) => (
            <option key={option} value={index}>
              {option}
            </option>
          ))}
        </Form.Select>
      </div>
    </div>
  );
};

export default SelectDropdown;
