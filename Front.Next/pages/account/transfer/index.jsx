import {
  Card,
  CardContent,
  TextField,
  Button,
  Typography,
  Box,
  Grid,
} from "@mui/material";
import { CheckCircleOutline } from "@mui/icons-material";
import { useState } from "react";
import { Layout } from "@/layouts/Layout";
import { useSession } from "next-auth/react";
import axios from "axios";

export default function TransferPage() {
  const [concept, setConcept] = useState("");
  const [amount, setAmount] = useState("");
  const [email, setEmail] = useState("");

  const [success, setSuccess] = useState(null);
  const { data: session } = useSession();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(
        `https://localhost:7149/api/Account/Transfer`,
        { concept: concept, amount: amount, email: email },
        {
          headers: {
            Authorization: `Bearer ${session.user?.token}`,
            "Content-Type": "application/json",
          },
        }
      );
      setSuccess(true);
    } catch (error) {
      setSuccess(false);
    }
  };

  const isValidAmount = amount > 0 && !isNaN(amount);
  const isValidEmail = (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/).test(email)
  const isValidConcept = typeof concept === "string" || concept.lenght > 8;

  return (
    <Layout>
      <Grid
        container
        mt={6}
        component={"div"}
        display={"flex"}
        justifyContent={"center"}
        alignItems={"center"}
      >
        <Card
          sx={{ width: "70%", height: "70%", margin: "auto", boxShadow: 4 }}
        >
          <CardContent sx={{ justifyContent: "center", alignItems: "center" }}>
            <Typography
              variant="h4"
              component="h2"
              sx={{
                marginBottom: 2,
                pt: 10,
                textAlign: "center",
                fontWeight: "bold",
                color: "primary.main",
              }}
            >
              Transfer to another account
            </Typography>

            <form onSubmit={handleSubmit}>
              <TextField
                label="Transfer reason"
                variant="outlined"
                type="string"
                value={concept}
                onChange={(e) => setConcept(e.target.value)}
                error={!isValidConcept}
                sx={{ marginBottom: 2, textAlign: "center" }}
                InputProps={{
                  sx: { fontSize: 20 },
                }}
                fullWidth
              />
              <TextField
                label="Amount to transfer"
                variant="outlined"
                type="number"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
                error={!isValidAmount}
                helperText={!isValidAmount ? "Enter a valid value" : ""}
                sx={{ marginBottom: 2, textAlign: "center" }}
                InputProps={{
                  sx: { fontSize: 20 },
                }}
                fullWidth
              />
              <TextField
                label="Destination email"
                variant="outlined"
                type="string"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                error={!isValidEmail}
                helperText={!isValidEmail ? "Enter a valid email" : ""}
                sx={{ marginBottom: 2, textAlign: "center" }}
                InputProps={{
                  sx: { fontSize: 20 },
                }}
                fullWidth
              />
              <Button
                variant="contained"
                color="primary"
                type="submit"
                disabled={
                  amount <= 0 || isNaN(amount) || concept == "" || email == ""
                }
                endIcon={<CheckCircleOutline />}
                sx={{ fontSize: 16 }}
                fullWidth
              >
                Transfer
              </Button>
            </form>
            {success !== null && (
              <Box sx={{ marginTop: 2 }}>
                <Typography
                  variant="h5"
                  color={success ? "primary" : "error"}
                  sx={{ textAlign: "center", fontWeight: "bold" }}
                >
                  {success ? "Transfer successful!" : "Transfer failed!"}
                </Typography>
              </Box>
            )}
          </CardContent>
        </Card>
      </Grid>
    </Layout>
  );
}
