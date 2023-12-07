import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'appointment'
const namespace = method => name + '/' + method

const initialState = {
  appointments: null
}

const getAppointments = createAsyncThunk(
  namespace('getAppointments'),
  async payload => {
    const { data } = await client.get(`Pets/${payload.id}/Appointments`)
    return data
  }
)

const createAppointment = createAsyncThunk(
  namespace('createAppointment'),
  async payload => {
    await client.post('appointments', payload)
  }
)

const editAppointment = createAsyncThunk(
  namespace('editAppointment'),
  async payload => {
    await client.put(`appointments/${payload.id}`, payload.value)
  }
)

const deleteAppointment = createAsyncThunk(
  namespace('deleteAppointment'),
  async payload => {
    await client.delete(`appointments/${payload}`)
  }
)

const appointmentSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getAppointments.fulfilled, (state, payload) => {
        return { ...state, appointments: payload.payload }
      })
      .addCase(createAppointment.fulfilled, state => {
        return { ...state }
      })
      .addCase(editAppointment.fulfilled, state => {
        return { ...state }
      })
      .addCase(deleteAppointment.fulfilled, state => {
        return { ...state }
      })
  }
})

export {
  getAppointments,
  createAppointment,
  editAppointment,
  deleteAppointment,
  appointmentSlice
}
