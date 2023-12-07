import React, { useState } from 'react'

const PriceForm = ({ onChange, formValues }) => {
  const [nameError, setNameError] = useState('')
  const [descriptionError, setDescriptionError] = useState('')
  const [priceError, setPriceError] = useState('')

  function handleChange (v) {
    const { id, value } = v.target
    onChange(id, value)
  }

  function handleNumericChange (v) {
    const { id, value } = v.target
    const sanitizedValue = value.replace(/,/g, '.')
    const validInputRegex = /^\d+(\.\d{0,2})?$/

    if (validInputRegex.test(sanitizedValue)) {
      onChange(id, parseFloat(sanitizedValue))
      setPriceError('')
    } else {
      setPriceError(
        'Invalid input. Please enter a positive number with up to two decimal places.'
      )
    }
  }

  function handleBlur (field, value) {
    switch (field) {
      case 'name':
        if (value.trim() === '') {
          setNameError('Name is required')
        } else if (value.length > 100) {
          setNameError('Name cannot be more than 100 characters long')
        } else {
          setNameError('')
        }
        break
      case 'description':
        if (value.trim() === '') {
          setDescriptionError('Description is required')
        } else if (value.length > 500) {
          setDescriptionError(
            'Description cannot be more than 500 characters long'
          )
        } else {
          setDescriptionError('')
        }
        break
      default:
        break
    }
  }

  return (
    <form className='my-4'>
      <label className='sr-only'>Name</label>
      <input
        type='text'
        id='name'
        className='form-control'
        placeholder="Enter procedure's name"
        required
        autoFocus
        value={formValues.name ?? ''}
        onChange={e => handleChange(e)}
        onBlur={() => handleBlur('name', formValues.name)}
      />
      {nameError && <div className='text-danger'>{nameError}</div>}
      <label className='sr-only'>Price</label>
      <input
        type='number'
        id='price'
        className='form-control'
        placeholder='Example: 20.00'
        step='0.01'
        min='0'
        required
        autoFocus
        value={formValues.price ?? 0}
        onChange={handleNumericChange}
        onBlur={() => handleBlur('price', formValues.price)}
      />
      {priceError && <div className='text-danger'>{priceError}</div>}
      <label className='sr-only'>Description</label>
      <input
        type='text'
        id='description'
        className='form-control'
        placeholder='Enter description'
        required
        autoFocus
        value={formValues.description ?? ''}
        onChange={e => handleChange(e)}
        onBlur={() => handleBlur('description', formValues.description)}
      />
      {descriptionError && (
        <div className='text-danger'>{descriptionError}</div>
      )}
    </form>
  )
}

export default PriceForm
