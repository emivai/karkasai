import React, { useState } from 'react'
import SelectDropdown from '../components/SelectDropdown'

const TimeslotForm = ({ onChange, formValues }) => {
  function handleChange (v) {
    const { id, value } = v.target
    onChange(id, value)
  }

  function handleDateChange (v) {
    const { id, value } = v.target
    onChange(id, new Date(value).toISOString())
  }

  function handleNumericChange (v) {
    const { id, value } = v.target
    const sanitizedValue = value.replace(/,/g, '.')
    const validInputRegex = /^\d+(\.\d{0,2})?$/

    if (validInputRegex.test(sanitizedValue)) {
      onChange(id, parseFloat(sanitizedValue))
      setTimeslotError('')
    } else {
      setTimeslotError(
        'Invalid input. Please enter a positive number with up to two decimal places.'
      )
    }
  }

  return (
    <form className='my-4'>
      <label>Start</label>
      <input
        type='datetime-local'
        id='start'
        autoFocus
        className='form-control'
        value={formValues.start ?? 0}
        onChange={e => handleDateChange(e)}
      />
      <label>End</label>
      <input
        type='datetime-local'
        id='end'
        autoFocus
        className='form-control'
        value={formValues.end ?? 0}
        onChange={e => handleDateChange(e)}
      />
      <SelectDropdown
        header={'Doctor'}
        formId='doctor'
        options={['Placeholder', 'Name Surname']}
        value={formValues.doctor ?? 0}
        handleChange={handleNumericChange}
      />
    </form>
  )
}

export default TimeslotForm
