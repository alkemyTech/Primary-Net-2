import { SweetAlert } from "@/components/alerts/SweetAlert";
import { Layout } from "@/layouts/Layout";
import { Grid, Typography } from "@mui/material";
import { getSession } from "next-auth/react";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import Head from "next/head";
import axios from "axios";
import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

export default function Home({ transactions = [], deposits = [], user }) {
console.log(transactions)
  const formatDate = (date = "") => {
    const day = date.slice(0, 10);
    return `${day}`;
  };

  const [showAlert, setShowAlert] = useState(false);
  const router = useRouter();

  const transactionSlice = () =>
    transactions.length > 3 ? transactions.slice(0, 3) : transactions;
  const depositsSlice = () =>
    deposits.length > 3 ? deposits.slice(0, 3) : deposits;

  useEffect(() => {
    if (router.query.invalidcredentials === "true") {
      setShowAlert(true);
    }
  }, [router.query.invalidcredentials]);

  return (
    <>
      <Head>
        <title>Primates - Home</title>
      </Head>

      {showAlert && (
        <SweetAlert
          title="Unauthorized"
          text="You don't have permission to visit this page."
          icon="error"
          timer={4000}
          onClose={() => setShowAlert(false)}
        />
      )}

      <Layout>
        <Grid container sx={{ backgroundColor: "#fff", borderRadius: 5, p: 2 }}>
          <Grid container display={"flex"} pb={2}>
            <Typography variant="h4">
              Welcome {user?.first_Name}!
            </Typography>
          </Grid>
          <Grid>
            <Grid container display={"flex"} direction={"row"}>
              <Typography variant="h5">Balance:</Typography>
              <Grid item>
                <Typography variant="h5" sx={{ fontWeight: "bold" }}>
                  $ {user?.money}
                </Typography>
              </Grid>
            </Grid>

            <Grid container display={"flex"} direction={"row"}>
              <Typography variant="h5">Points:</Typography>

              <Grid item>
                <Typography variant="h5" sx={{ fontWeight: "bold" }}>
                  {user?.points}
                </Typography>
              </Grid>
            </Grid>
          </Grid>
        </Grid>

        <Grid container p={4}>
          <Typography
            sx={{ fontWeight: "bold", color: "primary.main" }}
            variant="h5"
          >
            {" "}
            Lastest Transactions{" "}
          </Typography>
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell sx={{ fontWeight: "bold", fontSize: "large" }}>
                    {" "}
                    Id{" "}
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    Amount
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    Concept
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    {" "}
                    Date{" "}
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    Type
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {transactionSlice().map((row) => (
                  <TableRow
                    key={row.id}
                    sx={{
                      "&:last-child td, &:last-child th": { border: 0 },
                      ":hover": {
                        backgroundColor: "secondary.main",
                        color: "#eee",
                      },
                    }}
                  >
                    <TableCell
                      component="th"
                      scope="row"
                      sx={{ fontSize: "large", width: "20px" }}
                    >
                      {row.id}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      $ {row.amount}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {row.concept}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {row.date}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {row.type}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Grid>

        <Grid container p={4}>
          <Typography
            sx={{ fontWeight: "bold", color: "primary.main" }}
            variant="h5"
          >
            {" "}
            Fixed Term Deposits{" "}
          </Typography>
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell sx={{ fontWeight: "bold", fontSize: "large" }}>
                    {" "}
                    Id{" "}
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    Amount
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    Creation Date
                  </TableCell>
                  <TableCell
                    align="right"
                    sx={{ fontWeight: "bold", fontSize: "large" }}
                  >
                    {" "}
                    Closing Date{" "}
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {depositsSlice().map((row) => (
                  <TableRow
                    key={row.id}
                    sx={{
                      "&:last-child td, &:last-child th": { border: 0 },
                      ":hover": {
                        backgroundColor: "secondary.main",
                        color: "#eee",
                      },
                    }}
                  >
                    <TableCell
                      sx={{ fontSize: "large", width: "20px" }}
                      component="th"
                      scope="row"
                    >
                      {row.id}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {row.amount}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {formatDate(row.creation_Date)}
                    </TableCell>
                    <TableCell align="right" sx={{ fontSize: "large" }}>
                      {formatDate(row.closing_Date)}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Grid>

        <Grid></Grid>
      </Layout>
    </>
  );
}

export const getServerSideProps = async (context) => {
  const session = await getSession(context);
  try {
    const token = session.user?.token;

    const headers = {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    };

    const [userData, transactionData, fixedTermData] = await Promise.all([
      axios.get("https://localhost:7149/api/Auth/me", { headers }),
      axios.get("https://localhost:7149/api/Transactions?page=1", { headers }),
      axios.get("https://localhost:7149/api/FixedDeposit/UserDeposits", { headers }),
    ]);

    return {
      props: {
        transactions: transactionData.data.result,
        deposits: fixedTermData.data.result,
        user: userData.data,
      },
    };
  } catch (error) {
    return {
      props: {
        fixed: null,
        error: `An error occurred: ${error.message}`,
        session,
      },
    };
  }
};
