import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'auth'
const namespace = method => name + '/' + method

const initialState = {
  userData: null,
  userProfile: null,
  isLoggedIn: false,
  role: null
}
const login = createAsyncThunk(namespace('login'), async payload => {
  const { data } = await client.post('auth/login',payload)
  localStorage.setItem('token', data.accessToken)
  return data
})
const register = createAsyncThunk(namespace('register'), async payload => {
  await client.post('auth/register', payload)
})
const currentUser = createAsyncThunk(namespace('currentUser'), async () => {
  const { data } = await client.get('auth/currentUser')
  return data
})
const logout = createAsyncThunk(namespace('logout'), async () => {
  await client.post('auth/logout')
  localStorage.removeItem('token')
})

const authSlice = createSlice({
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

export { login, register, currentUser, logout, authSlice }
