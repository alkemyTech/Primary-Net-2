import { Layout } from "@/layouts/Layout";
import { Box } from "@mui/material";
import Link from "next/link";


export default function Home() {

  

  return (
    <Layout>

      <Box bgcolor={"primary.background"}>
        <Link href={"/login"}>
          LoginPage
        </Link>

        <Link href={"/admin/catalogues/new"}>
          newProduct
        </Link>

      </Box>
    </Layout>
  )
}
