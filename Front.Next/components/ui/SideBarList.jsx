import { Box, Divider, List, ListItem, ListSubheader } from "@mui/material"
import { SidebarOption } from "./SidebarOption"
import { getSession, useSession } from "next-auth/react"



const options = [
  // {
  //   name: "Home",
  //   path: "/users/"
  // },
  {
    name: "Catalogue",
    path: "/catalogue",
  },
  {
    name: "Transactions",
    path: "/users/transactions"
  },
  {
    name: "Fixed Term Deposits",
    path: "/fixed"
  },

  {
    name: "Transfer",
    path: "/account/transfer"
  },
  {
    name: "Topup",
    path: "/account/deposit"
  },
  {
    name: "Create Deposit",
    path: "/fixed/make"
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

export const SideBarList = ({ session, isAccountLocked }) => {

  const userRole = session?.user?.rol;

  return (
    <List>
      {
        userRole !== "Admin"
          ? null
          :
          <>
            {/* <Divider sx={{ pt: 2 }} /> */}
            <ListSubheader>
              Admin Panel
            </ListSubheader>
            {
              entities.map(({ name, path }) => (
                <SidebarOption key={name} name={name} path={path} />

              ))
            }
          </>
      }
      <Box display={ isAccountLocked? "none" : "" }>
      <ListSubheader>
              User Options
      </ListSubheader>        {
          options.map(({ name, path }) => (
            <SidebarOption key={name} name={name} path={path} />
          ))
        }
      </Box>

    </List>
  );
};
