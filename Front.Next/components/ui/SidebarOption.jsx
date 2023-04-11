import { ArrowForwardIos } from "@mui/icons-material"
import { Grid, ListItem, ListItemButton, ListItemIcon, ListItemText, Typography } from "@mui/material"
import Link from "next/link"

export const SidebarOption = ({ name, path }) => {
    return (
        <ListItem disablePadding>
            <ListItemButton >
                <Link href={`${path}`} style={{ textDecoration: "none", color: "#000"   }}>
                    <Grid container justifyContent={"center"} alignItems={"center"}>
                        <ListItemIcon >
                            <ArrowForwardIos fontSize="10px" />
                        </ListItemIcon>
                        <ListItemText primaryTypographyProps={{fontSize:16}} primary={name} />
                    </Grid>
                </Link>
            </ListItemButton>
        </ListItem>
    )
}
