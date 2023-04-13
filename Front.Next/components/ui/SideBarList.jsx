import { Divider, List, ListItem, ListSubheader } from "@mui/material"
import { SidebarOption } from "./SidebarOption"

const options = [
  {
    name: "Home",
    path: "/"
  },
  {
    name: "Transactions",
    path: "/transactions"
  },
  {
    name: "Fixed Term Deposits",
    path: "/"
  },
  {
    name: "Transfer",
    path: "/"
  },
  {
    name: "Topup",
    path: "/"
  },
  {
    name: "Admin Panel",
    path: "/"
  }
]


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
]

export const SideBarList = () => {

  const isAdmin = true;

  return (
    <List>
      {
        options.map(({ name, path }) => (
          <SidebarOption key={name} name={name} path={path} />
        ))
      }
      {
        !isAdmin
          ? null
          :
          <>
            <Divider sx={{pt:2}} />
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
    </List>
  )
}
