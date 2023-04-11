import { Box, Divider, Drawer, List, Toolbar, Typography } from "@mui/material"
import { SidebarOption } from "./SidebarOption"
import { SideBarList } from "./SideBarList"

export const Sidebar = ({drawerWidth}) => {

    const displayName = "userLogueado"

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
                        {displayName}
                    </Typography>
                </Toolbar>
            <Divider/>
            <List>
            <SideBarList/>

            </List>
        </Drawer>
    </Box>
  )
}
