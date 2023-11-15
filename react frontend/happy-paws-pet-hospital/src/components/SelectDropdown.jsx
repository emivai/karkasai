import React from 'react'

const SelectDropdown = ({ header, options }) => {
  return (
    <div class='form-group row mb-2'>
      <div class='col-sm-3'>{header}</div>
      <div class='col-sm-9'>
        <select class='form-select'>
          <option selected>Choose...</option>
          {options.map((option, index) => (
            <option value={index + 1}>{option}</option>
          ))}
        </select>
      </div>
    </div>
  )
}

export default SelectDropdown
