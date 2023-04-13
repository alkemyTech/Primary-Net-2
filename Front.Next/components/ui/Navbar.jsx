import { LogoutOutlined, MenuOutlined } from "@mui/icons-material"
import { AppBar, Grid, IconButton, Toolbar, Typography } from "@mui/material"
import { signOut } from "next-auth/react"

export const Navbar = ({drawerWidth}) => {

    const logout = () => {
        signOut({ callbackUrl: 'http://localhost:3000/login' })
    }

  return (
    <AppBar position="fixed" sx={{width:{sm:`calc(100% - ${drawerWidth}px )`} , ml:{sm:`${drawerWidth}px`}}}>
        <Toolbar>
            <IconButton color="inherit" edge="start" sx={{mr:2 , display:{sm:"none"}}}>
                <MenuOutlined/>
            </IconButton>

            <Grid container direction={"row"} justifyContent={"space-between"} >
                <Typography component={"div"} variant="h6">
                    Primates Wallet
                </Typography>
                <IconButton color="error" onClick={()=>logout()}>
                    <LogoutOutlined/>
                </IconButton>

            </Grid>


        </Toolbar>
    </AppBar>
  )
}
