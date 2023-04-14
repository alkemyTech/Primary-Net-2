import { Layout } from "@/layouts/Layout";
import { Box, Button } from "@mui/material";
import Link from "next/link";
import { createAccountModal } from "@/components/commons/modal/createAccountModal";
import { getSession } from "next-auth/react";

export default function Home() {
  const handleActivate = async () => {
    const session = await getSession();
    await createAccountModal(session?.user?.token);
  };
  return (
    <Layout>
      <Box bgcolor={"primary.background"}>
        <Link href={"/login"}>LoginPage</Link>
        <br />
        <Link href={"/admin/catalogues/new"}>newProduct</Link>
      </Box>

      <Box sx={{ display: "flex", justifyContent: "center" }}>
        <Button
          type="button"
          variant="contained"
          size="small"
          color="primary"
          onClick={() => handleActivate()}
          sx={{ padding: "5px", width: "70%" }}
        >
          Activate Account
        </Button>
      </Box>
    </Layout>
  );
}
