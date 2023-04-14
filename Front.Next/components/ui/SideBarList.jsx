import {
  Divider,
  List,
  ListItem,
  ListSubheader,
  ListItemButton,
  ListItemText,
  ListItemIcon,
  Grid,
} from "@mui/material";
import { SidebarOption } from "./SidebarOption";
import { useSession } from "next-auth/react";
import LockIcon from '@mui/icons-material/Lock';
import Link from "next/link";

const options = [
  {
    name: "Home",
    path: "/user/",
  },
  {
    name: "Catalogue",
    path: "/catalogue",
  },
  {
    name: "Transactions",
    path: "/user/transactions",
  },
  {
    name: "Fixed Term Deposits",
    path: "/user/fixed",
  },
  {
    name: "Transfer",
    path: "/user/",
  },
  {
    name: "Topup",
    path: "/user/",
  },
];

export const entities = [
  {
    name: "Accounts",
    path: "/admin/accounts",
  },
  {
    name: "Catalogues",
    path: "/admin/catalogues",
  },
  {
    name: "Fixed Term Deposits",
    path: "/admin/fixed",
  },
  {
    name: "Roles",
    path: "/admin/roles",
  },
  {
    name: "Transactions",
    path: "/admin/transactions",
  },
  {
    name: "Users",
    path: "/admin/users",
  },
];

export const SideBarList = ({ session }) => {
  const userRole = session?.user?.rol;

  return (
    <List>
      {options.map(({ name, path }) => (
        <SidebarOption key={name} name={name} path={path} />
      ))}
      {userRole !== "Admin" ? null : (
        <>
          <Divider sx={{ pt: 2 }} />
          <ListSubheader>Admin Panel</ListSubheader>
          {entities.map(({ name, path }) => (
            <SidebarOption key={name} name={name} path={path} />
          ))}
        </>
      )}
    </List>
  );
};
