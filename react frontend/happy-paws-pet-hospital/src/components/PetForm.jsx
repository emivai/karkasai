import SelectDropdown from '../components/SelectDropdown'
import React, { useState } from 'react'

const PetForm = ({ onChange, formValues }) => {
  const [nameError, setNameError] = useState('')
  const [birthdateError, setBirthdateError] = useState('')

  function handleChange (v) {
    const { id, value } = v.target
    onChange(id, value)
  }

  function handleNumericChange (v) {
    const { id, value } = v.target
    onChange(id, Number(value))
  }

  function handleDateChange (v) {
    const { id, value } = v.target
    onChange(id, new Date(value).toISOString())
  }

  function handleBlur (field, value) {
    switch (field) {
      case 'name':
        if (value.trim() === '') {
          setNameError('Name is required')
        } else if (value.length > 50) {
          setNameError('Name cannot be more than 50 characters long')
        } else {
          setNameError('')
        }
        break
      case 'birthdate':
        const currentDate = new Date()
        const selectedDate = new Date(value)

        if (isNaN(selectedDate.getTime())) {
          setBirthdateError('Invalid date format')
        } else if (
          selectedDate < new Date(currentDate.getFullYear() - 200, 0, 1)
        ) {
          setBirthdateError('Birthdate cannot be older than 200 years ago')
        } else if (selectedDate > currentDate) {
          setBirthdateError('Birthdate cannot be in the future')
        } else {
          setBirthdateError('')
        }
        break
      default:
        break
    }
  }

  return (
    <form className='my-4'>
      <SelectDropdown
        header={'Type'}
        formId='type'
        options={['Dog', 'Cat', 'Rodent', 'Exotic']}
        value={formValues.type ?? 0}
        handleChange={handleNumericChange}
      />
      <label className='sr-only'>Name</label>
      <input
        type='text'
        id='name'
        className='form-control'
        placeholder="Enter pet's name"
        required
        value={formValues.name ?? ''}
        onChange={e => handleChange(e)}
        onBlur={() => handleBlur('name', formValues.name)}
      />
      {nameError && <div className='text-danger'>{nameError}</div>}
      <label>BirthDate</label>
      <input
        type='date'
        id='birthdate'
        className='form-control'
        value={new Date(formValues.birthdate).toLocaleDateString('lt-LT')}
        onChange={e => handleDateChange(e)}
        onBlur={() => handleBlur('birthdate', formValues.birthdate)}
      />
      {birthdateError && <div className='text-danger'>{birthdateError}</div>}
      <label className='sr-only'>Photo</label>
      <input
        type='text'
        id='photo'
        className='form-control'
        placeholder='Photo url'
        required
        value={formValues.photo ?? ''}
        onChange={e => handleChange(e)}
      />
    </form>
  )
}

export default PetForm
