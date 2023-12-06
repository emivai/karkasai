import React, { useState } from "react";

const MultiSelectDropdown = ({ options, formValues, onChange }) => {
  const [selectedOptions, setSelectedOptions] = useState([]);
  const [isDropdownOpen, setDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setDropdownOpen(!isDropdownOpen);
  };

  const handleCheckboxChange = (option) => {
    if (selectedOptions.includes(option)) {
      setSelectedOptions(selectedOptions.filter((item) => item !== option));
    } else {
      setSelectedOptions([...selectedOptions, option]);
    }
    onChange("procedureId", selectedOptions);
  };

  return (
    <div className="form-group row mb-2">
      <div>
        <label>Procedures</label>
        <div className="dropdown">
          <button
            className="btn btn-secondary dropdown-toggle w-100 bg-white text-dark text-truncate"
            type="button"
            onClick={toggleDropdown}
          >
            Choose...
          </button>
          <div
            className={`dropdown-menu${isDropdownOpen ? " show" : ""} w-100`}
          >
            {options?.map((option) => (
              <div key={option.id} className="form-check ms-3">
                <input
                  className="form-check-input"
                  type="checkbox"
                  id={option.id}
                  checked={selectedOptions.includes(option.id)}
                  onChange={() => handleCheckboxChange(option.id)}
                />
                <label className="form-check-label" htmlFor={option.id}>
                  {option.name}
                </label>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default MultiSelectDropdown;
