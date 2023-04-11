const { createSlice } = require("@reduxjs/toolkit");

const rolesSlice = createSlice({
    name:"roles",
    initialState:{
        roles:[]
    },
    reducers:{
        getAllRoles:(state, {payload}) => {
            state.roles = payload;
        }
    }
});

export const { getAllRoles } = rolesSlice.actions;
export default rolesSlice.reducer;