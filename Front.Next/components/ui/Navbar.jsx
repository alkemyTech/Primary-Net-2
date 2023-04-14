import { LogoutOutlined, MenuOutlined } from "@mui/icons-material";
import {
  AppBar,
  Box,
  Grid,
  IconButton,
  Toolbar,
  Typography,
} from "@mui/material";
import { signOut, useSession } from "next-auth/react";
import LockIcon from "@mui/icons-material/Lock";
import Swal from "sweetalert2";
import AccountLockModal from "../commons/modal/AccountLockModal";
import { useState } from "react";
import axios from "axios";

export const Navbar = ({ drawerWidth }) => {
  const { data: session } = useSession();
  const logout = () => {
    signOut({ callbackUrl: "http://localhost:3000/login" });
  };
  const [modalOpen, setModalOpen] = useState(false);

  const handleCloseModal = () => {
    setModalOpen(false);
  };
console.log(session)
  const handleConfirmModal = async () => {
    const isBlocked = session.user.accountIsDeleted;
    //agregar renderizado condicional para mensaje

    if (isBlocked) {
      await axios.put(
        `https://localhost:7149/api/Account/Activate/${session.user.accountId}`,
        {
          headers: {
            Authorization: `Bearer ${session.user?.token}`,
            "Content-Type": "application/json",
          },
        }
      );
    } else {
      //cambiar endpoint
      await axios.delete(
        `https://localhost:7149/api/Account/${session.user.accountId}`,
        {
          headers: {
            Authorization: `Bearer ${session.user?.token}`,
            "Content-Type": "application/json",
          },
        }
      );
    }
    handleCloseModal();
  };

  return (
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
          <Box>
            <IconButton color="error" onClick={() => setModalOpen(true)}>
              <LockIcon />
            </IconButton>
            <IconButton
              sx={{ color: "tertiary.main" }}
              onClick={() => logout()}
            >
              <LogoutOutlined />
            </IconButton>
          </Box>
        </Grid>
      </Toolbar>
      <AccountLockModal
        open={modalOpen}
        handleClose={handleCloseModal}
        handleConfirm={handleConfirmModal}
      />
    </AppBar>
  );
};
