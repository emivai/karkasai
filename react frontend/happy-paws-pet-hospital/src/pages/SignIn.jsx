import Logo from '../images/logo-black.png'
import React, { useState } from 'react'

const SignIn = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const handleSubmit = async event => {
    event.preventDefault()

    const requestBody = {
      email: email,
      password: password
    }

    try {
      const response = await fetch('https://localhost:7294/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
      })

      if (!response.ok) {
        // Handle authentication error
        console.error('Authentication failed')
        return
      }

      const responseData = await response.json()

      // Do something with the response data, e.g., store the token in state or cookies
      console.log('Authentication successful')
      console.log('Token:', responseData.token)
    } catch (error) {
      console.error('An error occurred during authentication:', error)
    }
  }

  return (
    <div className='container d-flex align-items-center justify-content-center'>
      <form className='form-signin' onSubmit={handleSubmit}>
        <img className='mb-4' src={Logo} alt='' width='150' height='150' />
        <h1 className='h3 mb-3 font-weight-normal'>Please sign in</h1>
        <label htmlFor='inputEmail' className='sr-only'>
          Email address
        </label>
        <input
          type='email'
          id='inputEmail'
          className='form-control'
          placeholder='Email address'
          required
          autoFocus
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
        <label htmlFor='inputPassword' className='sr-only'>
          Password
        </label>
        <input
          type='password'
          id='inputPassword'
          className='form-control'
          placeholder='Password'
          required
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <button className='btn mt-4 btn-lg btn-info btn-block' type='submit'>
          Sign in
        </button>
        <h1 className='h6 mb-4 mt-4 font-weight-light'>
          Don't have an account?
        </h1>
        <button
          className='btn btn-lg btn-secondary btn-block'
          type='button'
          onClick={() => console.log('Navigate to Register')}
        >
          Register
        </button>
      </form>
    </div>
  )
}

export default SignIn
