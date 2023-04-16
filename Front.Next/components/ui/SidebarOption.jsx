import { ArrowForwardIos } from "@mui/icons-material";
import {
  Grid,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Typography,
} from "@mui/material";
import Link from "next/link";

export const SidebarOption = ({ name, path }) => {
  return (
    <ListItem disablePadding>
      <ListItemButton sx={{ borderRadius: '5px', transition: 'background-color 0.5s cubic-bezier(0.4, 0, 0.2, 1), color 0.3s cubic-bezier(0.4, 0, 0.2, 1)', '&:hover': { backgroundColor: 'primary.main','& .MuiListItemText-primary': {color: '#fff'}, } }}>
        <Link href={`${path}`} style={{ textDecoration: "none" }}>
          <Grid container justifyContent={"center"} alignItems={"center"} sx={{ color: 'text.primary' }}>
            <ListItemIcon sx={{ minWidth: '30px' }}>
              <ArrowForwardIos fontSize="small" />
            </ListItemIcon>
            <ListItemText primaryTypographyProps={{fontSize:16}} primary={name} sx={{ '&:hover': { color: '#fff' } }} />
          </Grid>
        </Link>
      </ListItemButton>
    </ListItem>
  );
};
