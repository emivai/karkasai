import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'user'
const namespace = method => name + '/' + method

const initialState = {
  doctors: null,
  clients: null
}

const getClients = createAsyncThunk(namespace('getClients'), async () => {
    const { data } = await client.get('users?type=1')
    return data
})

const getDoctors = createAsyncThunk(namespace('currentUser'), async () => {
    const { data } = await client.get('users?type=1')
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
  }
})

export { getClients, getDoctors, userSlice }
