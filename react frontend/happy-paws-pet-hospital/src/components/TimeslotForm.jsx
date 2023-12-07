import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Form } from 'react-bootstrap'
import moment from 'moment'

const TimeslotForm = ({ onChange, formValues }) => {
  function handleChange (v) {
    const { id, value } = v.target
    onChange(id, value)
  }

  function handleDateChange (v) {
    const { id, value } = v.target

    // Use Moment.js to format the date to UTC
    const formattedDate = moment(value).toISOString()

    onChange(id, formattedDate)
  }

  return (
    <form className='my-4'>
      <label>Start</label>
      <input
        type='datetime-local'
        id='start'
        autoFocus
        className='form-control'
        value={
          formValues.start
            ? moment(formValues.start).format('YYYY-MM-DDTHH:mm')
            : ''
        }
        onChange={e => handleDateChange(e)}
      />
      <label>End</label>
      <input
        type='datetime-local'
        id='end'
        autoFocus
        className='form-control'
        value={
          formValues.end
            ? moment(formValues.end).format('YYYY-MM-DDTHH:mm')
            : ''
        }
        onChange={e => handleDateChange(e)}
      />
    </form>
  )
}

export default TimeslotForm
