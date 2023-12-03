import React from 'react'

const Login = () => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [rememberMe, setRememberMe] = useState(false)

  const handleSubmit = event => {
    event.preventDefault()

    // Perform authentication or other actions with the form data
    console.log('Email:', email)
    console.log('Password:', password)
    console.log('Remember Me:', rememberMe)
  }

  return (
    <div className='text-center'>
      <form className='form-signin' onSubmit={handleSubmit}>
        <img
          className='mb-4'
          src='../images/logo-black.png'
          alt=''
          width='150'
          height='150'
        />
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
        <div className='checkbox mb-3'>
          <label>
            <input
              type='checkbox'
              value={rememberMe}
              onChange={() => setRememberMe(!rememberMe)}
            />{' '}
            Remember me
          </label>
        </div>
        <button className='btn btn-lg btn-info btn-block' type='submit'>
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

export default Login
