import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../api/client'

const name = 'appointmentProcedure'
const namespace = method => name + '/' + method

const initialState = {
  appointmentProcedures: null
}

const getAppointmentProcedures = createAsyncThunk(
  namespace('getAppointmentProcedures'),
  async () => {
    const { data } = await client.get('appointmentProcedures')
    return data
  }
)

const createAppointmentProcedure = createAsyncThunk(
  namespace('createAppointmentProcedure'),
  async payload => {
    await client.post('appointmentProcedures', payload)
  }
)

const editAppointmentProcedure = createAsyncThunk(
  namespace('editAppointmentProcedure'),
  async payload => {
    await client.put(`appointmentProcedures/${payload.id}`, payload.value)
  }
)

const deleteAppointmentProcedure = createAsyncThunk(
  namespace('deleteAppointmentProcedure'),
  async payload => {
    await client.delete(`appointmentProcedures/${payload}`)
  }
)

const appointmentProcedureSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getAppointmentProcedures.fulfilled, (state, payload) => {
        return { ...state, appointmentProcedures: payload.payload }
      })
      .addCase(createAppointmentProcedure.fulfilled, state => {
        return { ...state }
      })
      .addCase(editAppointmentProcedure.fulfilled, state => {
        return { ...state }
      })
      .addCase(deleteAppointmentProcedure.fulfilled, state => {
        return { ...state }
      })
  }
})

export {
  getAppointmentProcedures,
  createAppointmentProcedure,
  editAppointmentProcedure,
  deleteAppointmentProcedure,
  appointmentProcedureSlice
}
