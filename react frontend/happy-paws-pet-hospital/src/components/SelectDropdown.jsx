import React from "react";

const SelectDropdown = ({ header, options }) => {
  return (
    <div className="form-group row mb-2">
      <div className="col-sm-3">{header}</div>
      <div className="col-sm-9">
        <select className="form-select">
          <option selected>Choose...</option>
          {options.map((option, index) => (
            <option value={index + 1}>{option}</option>
          ))}
        </select>
      </div>
    </div>
  );
};

export default SelectDropdown;
