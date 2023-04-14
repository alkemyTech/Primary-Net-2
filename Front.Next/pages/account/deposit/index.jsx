import { Card, CardContent, TextField, Button, Typography, Box, Grid } from '@mui/material';
import { CheckCircleOutline } from '@mui/icons-material';
import { useState } from 'react';
import { Layout } from '@/layouts/Layout';
import { useSession } from 'next-auth/react';
import axios from 'axios';
import { headers } from '@/next.config';



export default function DepositPage() {
  const [amount, setAmount] = useState('');
  const [success, setSuccess] = useState(false);
  const { data: session } = useSession()

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (amount > 1000000000) return
    try {
      const res = await axios.post(`https://localhost:7149/api/Account/Deposit`, { money: amount, concept: "Deposit" }, {headers: {
        'Authorization': `Bearer ${session?.user?.token}`,
        "Content-Type": "application/json",
      }})
      setSuccess(true);
      
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <Layout>
      <Grid container width={"100%"} height={"60vh"} mt={6} component={"div"} display={"flex"} justifyContent={"center"} alignItems={"center"}>

        <Card sx={{ width: "80%", height: "80%", margin: 'auto', boxShadow: 4 }}>
          <CardContent sx={{ justifyContent: "center", alignItems: "center" }}>
            <Typography variant="h5" component="h2" sx={{ marginBottom: 2, pt: 10 }}>
              Deposit to Account
            </Typography>
            <form onSubmit={handleSubmit}>
              <TextField
                label="Amount to deposit"
                variant="outlined"
                type="number"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
                error={amount <= 0 || isNaN(amount)}
                helperText={amount <= 0 || isNaN(amount) ? 'Enter a valid value' : ''}
                sx={{ marginBottom: 2, textAlign: "center" }}
                InputProps={{
                  sx: { fontSize: 20 }
                }}
                fullWidth
              />
              <Button
                variant="contained"
                color="primary"
                type="submit"
                disabled={amount <= 0 || isNaN(amount)}
                endIcon={<CheckCircleOutline />}
                sx={{ fontSize: 16 }}
                fullWidth
              >
                Deposit
              </Button>
            </form>
            {success && (
              <Box sx={{ marginTop: 2 }}>
                <Typography color="primary">
                  Deposit successful!
                </Typography>
              </Box>
            )}
          </CardContent>
        </Card>
      </Grid>

    </Layout>
  );
}