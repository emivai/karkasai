import { configureStore } from '@reduxjs/toolkit'
import { userSlice } from '../reducers/user'
import { authSlice } from '../reducers/auth'

export default configureStore({
  reducer: {
    auth: authSlice.reducer,
    user: userSlice.reducer
  },
  devTools: true
})
