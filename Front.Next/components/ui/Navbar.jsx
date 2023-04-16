import { LogoutOutlined, MenuOutlined } from "@mui/icons-material";
import {
  AppBar,
  Grid,
  IconButton,
  Toolbar,
  Typography,
  Box,
} from "@mui/material";
import { getSession, signOut, useSession } from "next-auth/react";
import { ConfirmSweetAlert } from "../alerts/ConfirmSweetAlert";
import { useEffect, useState } from "react";
import LockIcon from "@mui/icons-material/Lock";
import axios from "axios";
import { headers } from "@/next.config";
import LockOpenIcon from "@mui/icons-material/LockOpen";
import { SweetAlert } from "../alerts/SweetAlert";
import https from "https";
import { useRouter } from "next/router";

export const Navbar = ({
  drawerWidth,
  isAccountLocked,
  setShowAlertLock,
  setShowAlert,
}) => {
  return (
    <>
      <AppBar
        position="fixed"
        sx={{
          width: { sm: `calc(100% - ${drawerWidth}px )` },
          ml: { sm: `${drawerWidth}px` },
        }}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            edge="start"
            sx={{ mr: 2, display: { sm: "none" } }}
          >
            <MenuOutlined />
          </IconButton>

          <Grid container direction={"row"} justifyContent={"space-between"}>
            <Typography component={"div"} variant="h6">
              Primates Wallet
            </Typography>
            <Box sx={{ direction: "flex" }}>
              <IconButton
                sx={{
                  color: "tertiary.main",
                  boxShadow: "0px 2px 2px rgba(0, 0, 0, 0.2)",
                }}
                onClick={() => setShowAlertLock(true)}
                title={!isAccountLocked ? "Lock" : "Unlock"}
              >
                {!isAccountLocked ? <LockIcon /> : <LockOpenIcon />}
              </IconButton>

              <IconButton
                color="error"
                sx={{
                  boxShadow: "0px 2px 2px rgba(0, 0, 0, 0.2)",
                }}
                onClick={() => setShowAlert(true)}
                title={"Logout"}
              >
                <LogoutOutlined />
              </IconButton>
            </Box>
          </Grid>
        </Toolbar>
      </AppBar>
    </>
  );
};

export const getServerSideProps = async (context) => {
  try {
    const session = await getSession(context);
    return {
      props: {
        account: res.data,
      },
    };
  } catch (error) {
    console.log(error);
  }
  handleCloseModal();
};
