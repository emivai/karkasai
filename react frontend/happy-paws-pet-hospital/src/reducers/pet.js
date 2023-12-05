import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'pet'
const namespace = method => name + '/' + method

const initialState = {
  pets: null
}

const getPets = createAsyncThunk(namespace('getPets'), async () => {
    const { data } = await client.get('pets')
    return data
})

const petSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getPets.fulfilled, (state, payload) => {
        return { ...state, pets: payload.payload }
      })
  }
})

export { getPets, petSlice }
