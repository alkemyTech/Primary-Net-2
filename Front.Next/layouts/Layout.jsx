import { Navbar } from "@/components/ui/Navbar"
import { Sidebar } from "@/components/ui/Sidebar"
import { Box, Grid, Toolbar } from "@mui/material"

const drawerWidth = 240;

export const Layout = ({ children }) => {


    return (
        <Grid container>
            <Navbar drawerWidth={drawerWidth} />
            <Sidebar drawerWidth={drawerWidth} />
            <Box component={"main"} sx={{ flexGrow: 1, padding: 2 }}>
                <Toolbar />
                {children}
            </Box>
        </Grid>
    )
}
