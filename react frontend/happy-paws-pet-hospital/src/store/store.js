import { configureStore } from '@reduxjs/toolkit'
import { userSlice } from '../reducers/user'
import { authSlice } from '../reducers/auth'
import { petSlice } from '../reducers/pet'

export default configureStore({
  reducer: {
    auth: authSlice.reducer,
    user: userSlice.reducer,
    pet: petSlice.reducer
  },
  devTools: true
})
