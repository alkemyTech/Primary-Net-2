import {
  Box,
  Button,
  Divider,
  Drawer,
  List,
  Toolbar,
  Typography,
} from "@mui/material";
import { SidebarOption } from "./SidebarOption";
import { SideBarList } from "./SideBarList";
import { useSession } from "next-auth/react";
import {LockIcon } from "@mui/icons-material";
 
export const Sidebar = ({ drawerWidth }) => {
  const { data: session } = useSession();

  return (
    <Box
      component={"nav"}
      sx={{ flexShrink: { sm: 0 }, width: { sm: drawerWidth } }}
    >
      <Drawer
        variant="permanent"
        open
        sx={{
          display: { xs: "block" },
          "& .MuiDrawer-paper": {
            boxSizing: "border-box",
            width: drawerWidth,
          },
        }}
      >
        <Toolbar>
          <Typography variant="h6" noWrap component={"div"}>
            {`${session?.user.first_Name} ${session?.user.last_Name} `}
          </Typography>
        </Toolbar>
        <Divider />
        <List>
          <SideBarList session={session} />
        </List>
      </Drawer>
    </Box>
  );
};
