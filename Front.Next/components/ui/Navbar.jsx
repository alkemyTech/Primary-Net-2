import { LogoutOutlined, MenuOutlined } from "@mui/icons-material"
import { AppBar, Grid, IconButton, Toolbar, Typography, Box, Tooltip } from "@mui/material"
import { getSession, signOut, useSession } from "next-auth/react"
import LockIcon from "@mui/icons-material/Lock";
import LockOpenIcon from '@mui/icons-material/LockOpen';



export const Navbar = ({ drawerWidth,  isAccountLocked,   setShowAlertLock, setShowAlert}) => {


    return (
        <>

            <AppBar position="fixed" sx={{ width: { sm: `calc(100% - ${drawerWidth}px )` }, ml: { sm: `${drawerWidth}px` } }}>
                <Toolbar>
                    <IconButton color="inherit" edge="start" sx={{ mr: 2, display: { sm: "none" } }}>
                        <MenuOutlined />
                    </IconButton>

                    <Grid container direction={"row"} justifyContent={"space-between"} >
                        <Typography component={"div"} variant="h6">
                            Primates Wallet
                        </Typography>
                        <Box component={"div"}>
                            <IconButton sx={{ color: "tertiary.main" }} onClick={() => setShowAlertLock(true)}>
                                {
                                    !isAccountLocked
                                        ? <Tooltip title="Lock Account"><LockIcon />
                                        </Tooltip> 
                                        :  <Tooltip title="Lock Account"><LockOpenIcon /></Tooltip>
                                }
                            </IconButton>
                        </Box>
                        <Box component={"div"}>
                            <IconButton color="error" onClick={() => setShowAlert(true)}>
                                <Tooltip title="Logout">
                                <LogoutOutlined />

                                </Tooltip>
                            </IconButton>
                        </Box>



                    </Grid>

                </Toolbar>
            </AppBar>

        </>
    )

}



export const getServerSideProps = async (context) => {

    try {
        const session = await getSession(context);
        console.log(session)

        return {
            props: {
                account: res.data
            }
        }
    }
    catch (error) {
        console.log(error)
    }
    handleCloseModal();
  };



