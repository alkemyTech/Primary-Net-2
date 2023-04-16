import { ConfirmSweetAlert } from "@/components/alerts/ConfirmSweetAlert";
import { SweetAlert } from "@/components/alerts/SweetAlert";
import { Navbar } from "@/components/ui/Navbar"
import { Sidebar } from "@/components/ui/Sidebar"
import { Box, Grid, Toolbar } from "@mui/material"
import axios from "axios";
import { signOut, useSession } from "next-auth/react";
import { useEffect, useState } from "react";

const drawerWidth = 240;

export const Layout = ({ children }) => {

    const { data: session } = useSession();
    const [isAccountLocked, setIsAccountLocked] = useState(false)
    const [showAlert, setShowAlert] = useState(false);
    const [showAlertLock, setShowAlertLock] = useState(false);
    const [unlockedAlert, setUnlockedAlert] = useState(false);
    const [lockeddAlert, setLockedAlert] = useState(false);

    const logout = () => {
        signOut({ callbackUrl: 'http://localhost:3000/login' })
    }


    const isLocked = async () => {
        try {
            const res = await axios.get(`https://localhost:7149/api/Account/${session?.user?.accountId}`, {
                headers: {
                    'Authorization': `Bearer ${session.user?.token}`,
                    "Content-Type": "application/json",
                }
            })
            const { isBlocked } = res.data;
            setIsAccountLocked(isBlocked)
        } catch (error) {
            console.log(error)
        }
    }

    const lockAccount = async () => {
        const { data } = await axios.put(`https://localhost:7149/api/Account/Activate/${session.user?.accountId}`, {},
            {
                headers: {
                    'Authorization': `Bearer ${session.user?.token}`,
                    "Content-Type": "application/json",
                }
            })
        data === "Account Locked" ? setIsAccountLocked(true) : setIsAccountLocked(false)
        setShowAlertLock(false)
        setShowAlert(false)
    }

    useEffect(() => {
        isLocked()
    }, [isAccountLocked, isLocked])

    return (
        <>
            {
                showAlert && <ConfirmSweetAlert title="Logging out" text="Are you sure you want to logout?" confirmButtonText="Yes" cancelButtonText="No" onConfirm={logout} onCancel={() => setShowAlert(false)} onClose={() => setShowAlert(false)} />
            }

            {
                showAlertLock && <ConfirmSweetAlert title={`${isAccountLocked ? 'Unlock' : 'Lock'} account`} text={`Are you sure you want ${isAccountLocked ? 'unlock' : 'lock'} your account?`} confirmButtonText="Yes" cancelButtonText="No" onConfirm={lockAccount} onClose={() => setShowAlertLock(false)} onCancel={() => setShowAlertLock(false)} />
            }

            {
                lockeddAlert && <SweetAlert title="Account blocked" text="Your account has been blocked" icon="success" timer={4000} onClose={() => setLockedAlert(false)} />
            }

            {
                unlockedAlert && <SweetAlert title="Account unlocked" text="Your account has been unlocked" icon="success" timer={4000} onClose={() => setUnlockedAlert(false)} />
            }
            <Grid container>

                <Navbar drawerWidth={drawerWidth} isAccountLocked={isAccountLocked} setShowAlert={setShowAlert} setShowAlertLock={setShowAlertLock} />
                <Sidebar drawerWidth={drawerWidth} isAccountLocked={isAccountLocked} />
                <Box component={"main"} sx={{ flexGrow: 1, padding: 2 }}>
                    <Toolbar />
                    {children}
                </Box>
            </Grid>
        </>
    )
}
