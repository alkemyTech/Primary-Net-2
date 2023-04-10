import { createSlice } from "@reduxjs/toolkit";

export const authSlice = createSlice({
    name: "auth",
    initialState: {
        status: 'checking',
        UserId: 0,
        First_Name: '',
        Last_Name: '',
        Email: '',
        Points: 0,
        Rol: '',
        ErrorMessage: ''
    },
    reducers:
    {
        login: (state, {payload}) => {
            state.status = 'authenticated';
            state.UserId = payload.userId;
            state.First_Name = payload.first_Name;
            state.Last_Name = payload.last_Name;
            state.Email = payload.email;
            state.Points = payload.points;
            state.Rol = payload.rol;
            state.ErrorMessage = null;
        },
        logout: (state, { payload }) => {
            state.status = 'not-authenticated';
            state.UserId = null;
            state.First_Name = null;
            state.Last_Name = null;
            state.Email = null;
            state.Points = null;
            state.Rol = null;
            state.ErrorMessage = payload
        },
        checking: (state) => {
            state.status = "checking";
        }

    }
})

export const { login, logout, checking} = authSlice.actions;
export default authSlice.reducer;