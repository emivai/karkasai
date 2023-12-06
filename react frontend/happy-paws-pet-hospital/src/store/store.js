import { configureStore } from '@reduxjs/toolkit'
import { userSlice } from '../reducers/user'
import { authSlice } from '../reducers/auth'
import { petSlice } from '../reducers/pet'
import { procedureSlice } from '../reducers/procedure'
import { timeslotSlice } from '../reducers/timeslot'

export default configureStore({
  reducer: {
    auth: authSlice.reducer,
    user: userSlice.reducer,
    pet: petSlice.reducer,
    procedure: procedureSlice.reducer,
    timeslot: timeslotSlice.reducer
  },
  devTools: true
})
