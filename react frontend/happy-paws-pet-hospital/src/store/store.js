import { configureStore } from "@reduxjs/toolkit";
import { userSlice } from "../reducers/user";
import { authSlice } from "../reducers/auth";
import { petSlice } from "../reducers/pet";
import { procedureSlice } from "../reducers/procedure";
import { timeslotSlice } from "../reducers/timeslot";
import { appointmentSlice } from "../reducers/appointment";
import { appointmentProcedureSlice } from "../reducers/appointmentprocedure";
import { noteSlice } from "../reducers/note";

export default configureStore({
  reducer: {
    auth: authSlice.reducer,
    user: userSlice.reducer,
    pet: petSlice.reducer,
    procedure: procedureSlice.reducer,
    timeslot: timeslotSlice.reducer,
    appointment: appointmentSlice.reducer,
    appointmentProcedure: appointmentProcedureSlice.reducer,
    note: noteSlice.reducer,
  },
  devTools: true,
});
