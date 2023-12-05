import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { useDispatch, useSelector } from 'react-redux'
import { client } from '../api/client'

const name = 'procedure'
const namespace = method => name + '/' + method

const initialState = {
  procedures: null
}

const getProcedures = createAsyncThunk(namespace('getProcedures'), async () => {
  const { data } = await client.get('procedures')
  return data
})

const createProcedure = createAsyncThunk(
  namespace('createProcedure'),
  async payload => {
    await client.post('procedures', payload)
  }
)
const procedureSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(getProcedures.fulfilled, (state, payload) => {
        return { ...state, procedures: payload.payload }
      })
      .addCase(createProcedure.fulfilled, state => {
        return { ...state }
      })
  }
})

export { getProcedures, createProcedure, procedureSlice }
