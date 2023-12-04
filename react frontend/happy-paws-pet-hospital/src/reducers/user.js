import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from 'axios'

const name = 'user'
const namespace = method => name + '/' + method

const config = {
  headers: { Authorization: `Bearer ${localStorage.token}` }
}

const initialState = {
  userData: null,
  userProfile: null,
  isLoggedIn: false,
  role: null
}
const login = createAsyncThunk(namespace('login'), async payload => {
  const { data } = await axios.post(
    'https://localhost:7294/auth/login',
    payload
  )
  localStorage.setItem('token', data.accessToken)
  return data
})
const register = createAsyncThunk(namespace('register'), async payload => {
  await axios.post('https://localhost:7294/auth/register', payload)
})
const currentUser = createAsyncThunk(namespace('currentUser'), async () => {
  const { data } = await axios.get(
    'https://localhost:7294/auth/currentUser',
    config
  )
  return data
})
const logout = createAsyncThunk(namespace('logout'), async () => {
  await axios.post('https://localhost:7294/auth/logout', null, config)
  localStorage.removeItem('token')
})

const userSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(login.fulfilled, state => {
        return { ...state, isLoggedIn: true }
      })
      .addCase(register.fulfilled, state => {
        return { ...state }
      })
      .addCase(currentUser.fulfilled, (state, payload) => {
        return {
          ...state,
          userData: payload.payload.user,
          role: payload.payload.role,
          isLoggedIn: true
        }
      })
      .addCase(logout.fulfilled, () => {
        return { ...initialState }
      })
  }
})

export { login, register, currentUser, logout, userSlice }
