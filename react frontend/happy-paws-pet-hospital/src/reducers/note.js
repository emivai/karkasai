import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { client } from "../api/client";

const name = "note";
const namespace = (method) => name + "/" + method;

const initialState = {
  notes: null,
};

const getNotes = createAsyncThunk(namespace("getnotes"), async (payload) => {
  const { data } = await client.get(
    `Pets/${payload.petId}/Appointments/${payload.id}/notes`
  );
  return data;
});

const createNote = createAsyncThunk(
  namespace("createNote"),
  async (payload) => {
    await client.post(
      `Pets/${payload.petId}/Appointments/${payload.id}/notes`,
      { value: payload.value }
    );
  }
);

const editNote = createAsyncThunk(namespace("editNote"), async (payload) => {
  await client.put(
    `Pets/${payload.petId}/Appointments/${payload.appointmentId}/notes/${payload.id}`,
    {
      value: payload.value,
    }
  );
});

const deleteNote = createAsyncThunk(
  namespace("deleteNote"),
  async (payload) => {
    await client.delete(
      `Pets/${payload.petId}/Appointments/${payload.appointmentId}/notes/${payload.id}`
    );
  }
);

const noteSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getNotes.fulfilled, (state, payload) => {
        return { ...state, notes: payload.payload };
      })
      .addCase(editNote.fulfilled, (state) => {
        return { ...state };
      })
      .addCase(deleteNote.fulfilled, (state) => {
        return { ...state };
      });
  },
});

export { getNotes, createNote, deleteNote, editNote, noteSlice };
