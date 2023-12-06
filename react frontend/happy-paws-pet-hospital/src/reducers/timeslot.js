import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { client } from "../api/client";

const name = "timeslot";
const namespace = (method) => name + "/" + method;

const initialState = {
  timeslots: null,
};

const getTimeslots = createAsyncThunk(namespace("gettimeslots"), async () => {
  const { data } = await client.get("timeslots");
  return data;
});

const getTimeslotsByDoctorId = createAsyncThunk(
  namespace("getTimeslotsByDoctorId"),
  async (payload) => {
    const { data } = await client.get(`timeslots?doctorId=${payload}`);
    return data;
  }
);
const createTimeslot = createAsyncThunk(
  namespace("createTimeslot"),
  async (payload) => {
    await client.post("timeslots", payload);
  }
);

const timeslotSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getTimeslots.fulfilled, (state, payload) => {
        return { ...state, timeslots: payload.payload };
      })
      .addCase(getTimeslotsByDoctorId.fulfilled, (state, payload) => {
        return { ...state, timeslots: payload.payload };
      })
      .addCase(createTimeslot.fulfilled, (state) => {
        return { ...state };
      });
  },
});

export { getTimeslots, createTimeslot, getTimeslotsByDoctorId, timeslotSlice };
