import React from 'react'

const NotesForm = ({ onChange, formValues }) => {
  function handleChange (v) {
    const { id, value } = v.target
    onChange(id, value)
  }
  return (
    <>
      <label>Write note:</label>
      <textarea
        className='form-control'
        id='value'
        rows='3'
        value={formValues.value}
        onChange={handleChange}
      ></textarea>
    </>
  )
}

export default NotesForm
