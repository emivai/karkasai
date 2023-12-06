import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { client } from "../api/client";

const name = "pet";
const namespace = (method) => name + "/" + method;

const initialState = {
  pets: null,
};

const getPets = createAsyncThunk(namespace("getPets"), async () => {
  const { data } = await client.get("pets");
  return data;
});

const createPet = createAsyncThunk(namespace("createPet"), async (payload) => {
  await client.post("pets", payload);
});

const editPet = createAsyncThunk(namespace("editPet"), async (payload) => {
  await client.put(`pets/${payload.id}`, payload.value);
});

const deletePet = createAsyncThunk(namespace("deletePet"), async (payload) => {
  await client.delete(`pets/${payload}`);
});

const petSlice = createSlice({
  name: name,
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getPets.fulfilled, (state, payload) => {
        return { ...state, pets: payload.payload };
      })
      .addCase(createPet.fulfilled, (state) => {
        return { ...state };
      })
      .addCase(editPet.fulfilled, (state) => {
        return { ...state };
      })
      .addCase(deletePet.fulfilled, (state) => {
        return { ...state };
      });
  },
});

export { getPets, createPet, editPet, deletePet, petSlice };
