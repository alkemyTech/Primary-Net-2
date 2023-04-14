import { LogoutOutlined, MenuOutlined } from "@mui/icons-material"
import { AppBar, Grid, IconButton, Toolbar, Typography } from "@mui/material"
<<<<<<< HEAD
import { useState } from "react"
import { ConfirmSweetAlert } from "../alerts/ConfirmSweetAlert";
import { signOut } from "next-auth/react";
import Swal from "sweetalert2";
import { useRouter } from "next/router";

export const Navbar = ({drawerWidth}) => {

    const [showAlert, setShowAlert] = useState(false);

    const router = useRouter();

    // const handleLogout = async () => {
    //     await signOut();
    //     console.log("HOLA")
    //     router.replace('/login');
    //     Swal.fire({
    //       title: 'Logged Out',
    //       text: 'You have been successfully logged out.',
    //       icon: 'success',
    //       timer: 3000,
    //       timerProgressBar: true,
    //     });
    // };


  return (
    <>
    {/* {
        showAlert && <ConfirmSweetAlert 
                        title="Logout"
                        text="Are you sure you want to log out?"
                        icon="warning"
                        showCancelButton
                        confirmButtonText="Yes, log out"
                        cancelButtonText="Cancel"
                        confirmButtonColor="#dc3545"
                        cancelButtonColor="#6c757d"
                        onClose={() => setShowAlert(false)}
                        onCancel={() => setShowAlert(false)}
                        onConfirm={() => {
                            console.log("HOLA")
                            router.push('/login?logout=true')
                        }
                        }
                    />
    } */}
        <AppBar position="fixed" sx={{width:{sm:`calc(100% - ${drawerWidth}px )`} , ml:{sm:`${drawerWidth}px`}}}>
            <Toolbar>
                <IconButton color="inherit" edge="start" sx={{mr:2 , display:{sm:"none"}}}>
                    <MenuOutlined/>
=======
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
>>>>>>> develop
                </IconButton>
                

                <Grid container direction={"row"} justifyContent={"space-between"} >
                    <Typography component={"div"} variant="h6">
                        Primates Wallet
                    </Typography>
                    <IconButton  color="error">
                        <LogoutOutlined/>
                    </IconButton>

                </Grid>


            </Toolbar>
        </AppBar>
    </>

  )
}
