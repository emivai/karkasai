import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'timeslot'
const namespace = method => name + '/' + method

const initialState = {
  timeslots: null
}

const getTimeslots = createAsyncThunk(namespace('gettimeslots'), async () => {
  const { data } = await client.get('timeslots')
  return data
})

const getTimeslotsByDoctorId = createAsyncThunk(
  namespace('getTimeslotsByDoctorId'),
  async payload => {
    const { data } = await client.get(`timeslots?doctorId=${payload}`)
    return data
  }
)

const editTimeslot = createAsyncThunk(
  namespace('editTimeslot'),
  async payload => {
    await client.put(`timeslots/${payload.id}`, payload.value)
  }
)

const deleteTimeslot = createAsyncThunk(
  namespace('deleteTimeslot'),
  async payload => {
    await client.delete(`timeslots/${payload}`)
  }
)

const timeslotSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getTimeslots.fulfilled, (state, payload) => {
        return { ...state, timeslots: payload.payload }
      })
      .addCase(getTimeslotsByDoctorId.fulfilled, (state, payload) => {
        return { ...state, timeslots: payload.payload }
      })
      .addCase(editTimeslot.fulfilled, state => {
        return { ...state }
      })
      .addCase(deleteTimeslot.fulfilled, state => {
        return { ...state }
      })
  }
})

export {
  getTimeslots,
  createTimeslot,
  deleteTimeslot,
  editTimeslot,
  timeslotSlice
}
