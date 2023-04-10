import { createTheme } from "@mui/material";
import { red } from "@mui/material/colors";


const mainTheme = createTheme({
    palette : {
        mode:"light",
       background:{
        default: "#E5E7EB"
       },
       primary: {main:"#312E81"},
       secondary : {main: "#edf2ff"},
       tertiary : {main: "#FB923C"},
       error:{main : red.A400}
    } 
})


export default mainTheme;