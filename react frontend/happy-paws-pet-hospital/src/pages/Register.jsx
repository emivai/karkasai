import { useNavigate, Link } from 'react-router-dom'
import { useDispatch } from 'react-redux'
import React, { useState } from 'react'
import { register } from '../reducers/auth'
import Form from 'react-bootstrap/Form'

const Register = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()

  const [registerValue, setRegisterValue] = useState({
    name: '',
    surname: '',
    type: 1,
    photo: '',
    email: '',
    phoneNumber: '',
    password: '',
    confirmPassword: ''
  })

  function handleRegisterChange (v) {
    const { id, value } = v.target
    setRegisterValue({ ...registerValue, [id]: value })
  }

  const handleRegisterClick = async () => {
    if (
      registerValue.name !== '' &&
      registerValue.password === registerValue.confirmPassword
    ) {
      await dispatch(register(registerValue))
      navigate('/signin')
    }
  }

  console.log(registerValue)

  return (
    <div className='container d-flex align-items-center justify-content-center mt-5'>
      <div className='form'>
        <h1 className='h3 mb-3 font-weight-normal'>Register</h1>
        <label className='sr-only'>Name</label>
        <input
          type='text'
          id='name'
          className='form-control'
          placeholder='Name'
          required
          autoFocus
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Surname</label>
        <input
          type='text'
          id='surname'
          className='form-control'
          placeholder='Surname'
          required
          autoFocus
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Type</label>
        <Form.Select id='type' onChange={e => handleRegisterChange(e)}>
          <option value={1}>Client</option>
          <option value={2}>Doctor</option>
        </Form.Select>
        <label className='sr-only'>Photo</label>
        <input
          type='text'
          id='photo'
          className='form-control'
          placeholder='Photo url'
          required
          autoFocus
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Email address</label>
        <input
          type='email'
          id='email'
          className='form-control'
          placeholder='Email address'
          required
          autoFocus
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Phone number</label>
        <input
          type='tel'
          id='phoneNumber'
          className='form-control'
          placeholder='+370'
          required
          autoFocus
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Password</label>
        <input
          type='password'
          id='password'
          className='form-control'
          placeholder='Password'
          required
          onChange={e => handleRegisterChange(e)}
        />
        <label className='sr-only'>Confirm Password</label>
        <input
          type='password'
          id='confirmPassword'
          className='form-control'
          placeholder='Confirm Password'
          required
          onChange={e => handleRegisterChange(e)}
        />
        <button
          className='btn mt-4  btn-lg btn-secondary btn-block'
          type='button'
          onClick={handleRegisterClick}
        >
          Register
        </button>
      </div>
    </div>
  )
}

export default Register
