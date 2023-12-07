import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'user'
const namespace = method => name + '/' + method

const initialState = {
  doctors: null,
  clients: null,
  doctor: null
}

const getClients = createAsyncThunk(namespace('getClients'), async () => {
  const { data } = await client.get('users?type=1')
  return data
})

const getDoctors = createAsyncThunk(namespace('getDoctors'), async () => {
  const { data } = await client.get('users?type=2')
  return data
})

const editUser = createAsyncThunk(namespace('editUser'), async payload => {
  await client.put(`users/${payload.id}`, payload.value)
})

const deleteUser = createAsyncThunk(namespace('deleteUser'), async payload => {
  await client.put(`users/${payload}`)
})

const getUser = createAsyncThunk(namespace('getUser'), async payload => {
  const { data } = await client.get(`users/${payload.id}`, payload.value)
  return data
})

const userSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getClients.fulfilled, (state, payload) => {
        return { ...state, clients: payload.payload }
      })
      .addCase(getDoctors.fulfilled, (state, payload) => {
        return { ...state, doctors: payload.payload }
      })
      .addCase(editUser.fulfilled, state => {
        return { ...state }
      })
      .addCase(deleteUser.fulfilled, state => {
        return { ...state }
      })
      .addCase(getUser.fulfilled, state => {
        return { ...state, doctor: payload.payload }
      })
  }
})

export { getClients, getDoctors, editUser, deleteUser, userSlice, getUser }
