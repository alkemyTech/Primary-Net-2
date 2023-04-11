import { Box, Typography } from "@mui/material";
import Link from "next/link";


export default function Home() {
  return (
    <Box bgcolor={"primary.background"}>

    
    <Link href={"/login"}> 
    LoginPage
    </Link>

    </Box>
  )
}
