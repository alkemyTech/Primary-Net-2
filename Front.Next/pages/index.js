import { SweetAlert } from "@/components/alerts/SweetAlert";
import { Layout } from "@/layouts/Layout";
<<<<<<< HEAD
import { Box, Typography } from "@mui/material";
import { useSession } from "next-auth/react";
import Link from "next/link";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";


export default function Home() {
  const {data: session} = useSession();
  console.log(session);
  const [showAlert, setShowAlert] = useState(false);
  const router = useRouter();

  useEffect(() => {
    if (router.query.invalidcredentials === 'true') {
      setShowAlert(true);
    }
  }, [router.query.invalidcredentials]);
  return (
    <>
      {
        showAlert && <SweetAlert
          title="Unauthorized"
          text="You don't have permission to visit this page."
          icon="error"
          timer={4000}
          onClose={() => setShowAlert(false)}
        />
      }

      <Layout>

        <Box bgcolor={"primary.background"}>


          <Link href={"/login"}>
            LoginPage
          </Link>


          <Link href={"/admin"}>
            Admin Panel
          </Link>

        </Box>
      </Layout>
    </>
  )
=======
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
>>>>>>> develop
}
