import { combineReducers, configureStore } from "@reduxjs/toolkit";
import auth from "./auth/authSlice";
import roles from "./roles/rolesSlice";


const rootReducer = combineReducers({
    auth,
    roles
});

export const store = configureStore({
    reducer: rootReducer
})
