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
import Link from "next/link";

export const Sidebar = ({drawerWidth, isAccountLocked}) => {

    const { data: session } = useSession();

  return (
    <Box component={"nav"} sx={{flexShrink:{sm:0} , width:{sm:drawerWidth}}}>
        <Drawer variant="permanent" open sx={{
                display:{ xs: 'block' },
                '& .MuiDrawer-paper': {
                    boxSizing: 'border-box',
                    width: drawerWidth
                }
            }}>
                <Toolbar >
                    <Typography variant="h6" noWrap component={"div"} >
                        <Link href={`users/${session?.user?.userId}`} style={{textDecoration:"none", color:"black"}}>
                        {`${session?.user.first_Name} ${session?.user.last_Name} `}
                        </Link>
                    </Typography>
                </Toolbar>
            <Divider/>
            <List>
            <SideBarList session={session} isAccountLocked={isAccountLocked}/>

            </List>
        </Drawer>
    </Box>
  );
};

/*    
<Image
    cover
    src={primate}
    alt="Logo de la aplicación"
    style={{
    position: "absolute",
    top: 0,
    left: 0,
    width: "100%",
    height: "auto",
}}>
 */