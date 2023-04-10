import axios from "axios";
import { login, checking, logout } from "./authSlice";

export const startLoginWithEmailAndPassword = ( { UserName, Password } ) => {

    return async ( dispatch ) => {

        dispatch( checking() );

        try {
                    const response = await axios.post("https://localhost:7149/api/Auth/login", {UserName, Password});
                                
                    localStorage.setItem("Token", response.data)
            
                    const headers = {
                        'Authorization': `Bearer ${response.data}`,
                        'Content-Type': 'application/json'
                      } 
                    
                    const { data } = await axios.get("https://localhost:7149/api/Auth/me", { headers });
                
            
                    dispatch( login( data ) );
            
        } catch (error) {

            const { data } = error.response;

            dispatch( logout( data ) )

        }

    }
};

export const HandleRegister = async( {First_Name, Last_Name, Email, Password} ) => {

    const { data } = await axios.post("https://localhost:7149/api/User/signup", { First_Name, Last_Name, Email, Password })

}