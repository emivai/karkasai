import Logo from '../images/logo-black.png'
import { useNavigate, Link } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import React, { useEffect, useState } from 'react'
import { login } from '../reducers/auth'

const SignIn = () => {
  const dispatch = useDispatch()
  const navigate = useNavigate()

  const { isLoggedIn } = useSelector(state => state.auth)

  const [loginValue, setLoginValue] = useState({
    email: '',
    password: ''
  })

  function handleLoginChange (v) {
    const { id, value } = v.target
    setLoginValue({ ...loginValue, [id]: value })
  }

  const handleLoginClick = async () => {
    if (loginValue.email !== '' && loginValue.password !== '') {
      await dispatch(login(loginValue))
    }
  }

  useEffect(() => {
    if (isLoggedIn) {
      navigate('/')
    }
  }, [isLoggedIn])

  return (
    <div
      className='container d-flex align-items-center justify-content-center'
      style={{ marginBottom: '50px' }}
    >
      <div className='form'>
        <img className='mb-4' src={Logo} alt='' width='150' height='150' />
        <h1 className='h3 mb-3 font-weight-normal'>Please sign in</h1>
        <label className='sr-only'>Email address</label>
        <input
          type='email'
          id='email'
          className='form-control'
          placeholder='Email address'
          required
          autoFocus
          onChange={e => handleLoginChange(e)}
        />
        <label className='sr-only'>Password</label>
        <input
          type='password'
          id='password'
          className='form-control'
          placeholder='Password'
          required
          onChange={e => handleLoginChange(e)}
        />
        <button
          className='btn mt-4 btn-lg btn-info btn-block'
          onClick={handleLoginClick}
          type='button'
        >
          Sign in
        </button>
        <h1 className='h6 mb-4 mt-4 font-weight-light'>
          Don't have an account?
        </h1>
        <Link to={'/register'}>
          <button className='btn btn-lg btn-secondary btn-block' type='button'>
            Register
          </button>
        </Link>
      </div>
    </div>
  )
}

export default SignIn
