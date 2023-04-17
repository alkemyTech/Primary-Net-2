import React from "react";
import {
  Typography,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Button,
  Grid,
} from "@mui/material";

import { Layout } from "@/layouts/Layout";
import { getSession, useSession } from "next-auth/react";
import axios from "axios";
import https from "https";
import { useRouter } from "next/router";
import Head from "next/head";
import EditIcon from "@mui/icons-material/Edit";

const Details = ({ transaction }) => {
  const router = useRouter();

  const {data:session} = useSession();

  const { id, amount, concept, date, type, account_Id, to_Account_Id } =
    transaction;
  const date2 = new Date(date);
  const formattedDate = date2.toLocaleString("es-ES", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });

  return (
    <Layout>
      <Head>
        <title>Primates - Transaction Details</title>
      </Head>

      <Paper elevation={3} style={{ padding: "20px", margin: "20px" }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Transaction Details {id}
        </Typography>
        <TableContainer component={Paper}>
          <Table aria-label="transaction details">
            <TableBody>
              <TableRow>
                <TableCell component="th" scope="row">
                  Amount
                </TableCell>
                <TableCell align="left">${amount}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell component="th" scope="row">
                  Concept
                </TableCell>
                <TableCell align="left">{concept}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell component="th" scope="row">
                  Date
                </TableCell>
                <TableCell align="left">{formattedDate}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell component="th" scope="row">
                  Type
                </TableCell>
                <TableCell align="left">{type}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell component="th" scope="row">
                  Source Account
                </TableCell>
                <TableCell align="left">{account_Id}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell component="th" scope="row">
                  Destination Account
                </TableCell>
                <TableCell align="left">{to_Account_Id}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        <Grid container display={"flex"} justifyContent={"space-between"}>
          <Button
            variant="contained"
            onClick={() => router.back()}
            sx={{ mt: 2 }}
          >
            <Typography textTransform={"capitalize"}>Back</Typography>
          </Button>
          {session?.user?.rol == "Admin" && (
            <Button
              onClick={() => router.push(`/admin/transactions/edit/${id}`)}
              sx={{
                mt: 2,
                backgroundColor: "#eee",
                color: "black",
                ":hover": { color: "white", backgroundColor: "tertiary.main" },
              }}
            >
              {" "}
              <Typography textTransform={"capitalize"}>
                Edit Transaction
              </Typography>
              <EditIcon sx={{ backgroundColor: "tertiary" }} />
            </Button>
          )}
        </Grid>
      </Paper>
    </Layout>
  );
};

export const getServerSideProps = async (context) => {
  try {
    //Forma de obtener el id de la url
    const { id } = context.params;

    //obtener la sesión next auth
    const session = await getSession(context);

    const now = Math.floor(Date.now() / 1000);

    if (session == null || session.expires < now) {
      return {
        redirect: {
          destination: "/login",
          permanent: false,
        },
      };
    }

    const { data } = await axios.get(
      `https://localhost:7149/api/Transactions/${id}`,
      {
        headers: {
          Authorization: `Bearer ${session.user?.token}`,
          "Content-Type": "application/json",
        },
        httpsAgent: new https.Agent({
          rejectUnauthorized: false,
        }),
      }
    );

    return {
      props: {
        transaction: data.result,
      },
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        transaction: null, // Retorna null si hay algún error
      },
    };
  }
};

export default Details;
