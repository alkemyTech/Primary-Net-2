import { LogoutOutlined, MenuOutlined } from "@mui/icons-material"
import { AppBar, Grid, IconButton, Toolbar, Typography } from "@mui/material"
import { signOut } from "next-auth/react"
import { ConfirmSweetAlert } from "../alerts/ConfirmSweetAlert"
import { useState } from "react"

export const Navbar = ({ drawerWidth }) => {

    const [showAlert, setShowAlert] = useState(false);

    const logout = () => {
        signOut({ callbackUrl: 'http://localhost:3000/login' })
        
    }

    return (
        <>
            {
                showAlert && <ConfirmSweetAlert title="Logging out" text="Are you sure you want to logout?" confirmButtonText="Yes" cancelButtonText="No" onConfirm={logout} onCancel={()=>setShowAlert(false)} />
            }
            <AppBar position="fixed" sx={{ width: { sm: `calc(100% - ${drawerWidth}px )` }, ml: { sm: `${drawerWidth}px` } }}>
                <Toolbar>
                    <IconButton color="inherit" edge="start" sx={{ mr: 2, display: { sm: "none" } }}>
                        <MenuOutlined />
                    </IconButton>

                    <Grid container direction={"row"} justifyContent={"space-between"} >
                        <Typography component={"div"} variant="h6">
                            Primates Wallet
                        </Typography>
                        <IconButton color="error" onClick={() => setShowAlert(true)}>
                            <LogoutOutlined />
                        </IconButton>
                    </Grid>

                </Toolbar>
            </AppBar>

        </>
    )
}
