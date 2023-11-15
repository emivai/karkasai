import React, { useState } from 'react'

const MultiSelectDropdown = () => {
  const options = [
    'Wellness exam',
    'Dental extraction',
    'Feline infectious peritonitis (FIP) vaccination',
    'Rabies vaccination',
    'Nail trim',
    'Haircut'
  ]

  const [selectedOptions, setSelectedOptions] = useState([])
  const [isDropdownOpen, setDropdownOpen] = useState(false)

  const toggleDropdown = () => {
    setDropdownOpen(!isDropdownOpen)
  }

  const handleCheckboxChange = option => {
    if (selectedOptions.includes(option)) {
      setSelectedOptions(selectedOptions.filter(item => item !== option))
    } else {
      setSelectedOptions([...selectedOptions, option])
    }
  }

  return (
    <div className='form-group row mb-2'>
      <div class='col-sm-3'>Procedures</div>
      <div class='col-sm-9'>
        <div className='dropdown'>
          <button
            className='btn btn-secondary dropdown-toggle w-100 bg-white text-dark'
            type='button'
            onClick={toggleDropdown}
          >
            {selectedOptions.length > 0
              ? selectedOptions.join(', ').length > 40
                ? selectedOptions.join(', ').substring(0, 37) + '...'
                : selectedOptions.join(', ')
              : 'Choose...'}
          </button>
          <div
            className={`dropdown-menu${isDropdownOpen ? ' show' : ''} w-100`}
          >
            <div className='ms-3'>Choose...</div>
            {options.map(option => (
              <div key={option} className='form-check ms-3'>
                <input
                  className='form-check-input'
                  type='checkbox'
                  id={option}
                  checked={selectedOptions.includes(option)}
                  onChange={() => handleCheckboxChange(option)}
                />
                <label className='form-check-label' htmlFor={option}>
                  {option}
                </label>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}

export default MultiSelectDropdown
