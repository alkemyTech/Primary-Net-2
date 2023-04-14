import { Divider, List, ListItem, ListSubheader } from "@mui/material"
import { SidebarOption } from "./SidebarOption"
import { useSession } from "next-auth/react"




const options = [
  {
    name: "Home",
    path: "/users/"
  },
  {
    name: "Catalogue",
    path: "/users/catalogue"
  },
  {
    name: "Transactions",
    path: "/users/transactions"
  },
  {
    name: "Fixed Term Deposits",
    path: "/users/fixed"
  },
  {
    name: "Transfer",
    path: "/account/transfer"
  },
  {
    name: "Topup",
    path: "/users/"
  },

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
  // {
  //   name: "Fixed Term Deposits",
  //   path: "/admin/fixed",
  // },
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

export const SideBarList = ({ session }) => {

  const userRole = session?.user?.rol;

  return (
    <List>
      {
        options.map(({ name, path }) => (
          <SidebarOption key={name} name={name} path={path} />
        ))
      }
      {
        userRole !== "Admin"
          ? null
          :
          <>
            <Divider sx={{ pt: 2 }} />
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
