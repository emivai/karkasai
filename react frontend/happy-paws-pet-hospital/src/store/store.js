import { configureStore } from '@reduxjs/toolkit'
import { userSlice } from '../reducers/user'
import { authSlice } from '../reducers/auth'
import { petSlice } from '../reducers/pet'
import { procedureSlice } from '../reducers/procedure'

export default configureStore({
  reducer: {
    auth: authSlice.reducer,
    user: userSlice.reducer,
    pet: petSlice.reducer,
    procedure: procedureSlice.reducer
  },
  devTools: true
})
