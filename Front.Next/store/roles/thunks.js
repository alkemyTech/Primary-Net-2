import { getAllRoles } from "./rolesSlice";

const { default: axios } = require("axios");

export const getRoles = ()=> {
    return async ( dispatch ) => {
        try {

            const token = localStorage.getItem("Token")
            const headers = {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
              } 

              console.log(token)


            const {data} = await axios.get("https://localhost:7149/api/Role", headers)
            dispatch( getAllRoles( data ));

        } catch (error) {
            console.log(error);
        }
    }
}
