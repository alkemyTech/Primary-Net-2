import { ConfirmSweetAlert } from "@/components/alerts/ConfirmSweetAlert";
import { SweetAlert } from "@/components/alerts/SweetAlert";
import { Layout } from "@/layouts/Layout";
import { Box, Button, FormControl, Grid, Input, InputLabel, Select, Typography } from "@mui/material";
import axios from "axios";
import { getSession, useSession } from "next-auth/react";
import Head from "next/head";
import { useRouter } from "next/router";
import { useState } from "react";

const DepositComponent = ({ account }) => {
    const [amount, setAmount] = useState('');
    const [term, setTerm] = useState('1-month');
    const [error, setError] = useState(null);
    const [result, setResult] = useState(0);
    const [showResult, setShowResult] = useState(false);
    const [disableInput, setDisableInput] = useState(false);
    const [showError, setShowError] = useState(false);
    const [showErrorCero, setShowErrorCero] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [showConfirm, setShowConfirm] = useState(false)
    const { data: session } = useSession();

    const router = useRouter();

    const handleAmountChange = (e) => {
        const value = e.target.value;
        setAmount(value);
        setResult(0);
        setShowResult(false);
        setShowError(false);
    };

    const handleTermChange = (e) => {
        const value = e.target.value;
        setTerm(value);
        setResult(0);
        setShowResult(false);
        setShowError(false);
    };

    const handleCalculate = async () => {
        if (account.money < amount) {
            setError('Insufficient funds. Please enter a smaller amount.');
            setShowError(true);
            return;
        }
        if (amount <= 0) {
            setError("The deposit amount must be greater than 0.");
            setShowError(true);
            return;
        }
        let rate;
        switch (term) {
            case '1-month':
                rate = 0.05;
                break;
            case '3-months':
                rate = 0.19;
                break;
            case '1-year':
                rate = 0.85;
                break;
            default:
                rate = 0;
                break;
        }
        setResult(amount * rate);
        setShowResult(true);
        setDisableInput(true);
    };

    const handleDeposit = async () => {
        setShowConfirm(false)
        const creation_date = new Date(); // Fecha y hora actual
        let closing_date = new Date(); // Inicializamos la fecha de cierre con la fecha actual

        // Calculamos la fecha de cierre según la opción seleccionada
        if (term === "1-month") {
            closing_date.setMonth(creation_date.getMonth() + 1);
        } else if (term === "3-months") {
            closing_date.setMonth(creation_date.getMonth() + 3);
        } else if (term === "1-year") {
            closing_date.setFullYear(creation_date.getFullYear() + 1);
        }
        const formattedCreationDate = creation_date.toISOString().slice(0, 23);
        const formattedClosingDate = closing_date.toISOString().slice(0, 23);

        try {
            const { data } = await axios.post("https://localhost:7149/api/FixedDeposit",
                {
                    amount: amount,
                    creation_Date: formattedCreationDate,
                    closing_Date: formattedClosingDate
                },
                {
                    headers: {
                        'Authorization': `Bearer ${session.user?.token}`,
                        "Content-Type": "application/json",

                    }
                })
            if (data.result == true) {
                setShowSuccess(true);
            }
        } catch (error) {
            setError("An error ocurred, please try again later.")
            setShowError(true);
        }



    };

    const handleReset = () => {
        setAmount('');
        setTerm('1-month');
        setResult(0);
        setShowResult(false);
        setDisableInput(false);
        setShowError(false);
        setShowErrorCero(true);
        setShowConfirm(false);
    };
    return (
        <>
            <Head>
                <title>
                    Primates - Nex fixed term deposit
                </title>
            </Head>
            {
                showSuccess && <SweetAlert
                    title="Success!"
                    text="The fixed term deposit was created successfully."
                    icon="success"
                    timer={3000}
                    onClose={() => router.push('/fixed')}
                />
            }
            {
                showConfirm && <ConfirmSweetAlert
                    title="Confirm Deposit Creation"
                    text={`Are you sure you want to create a fixed term deposit with the amount of ${amount}?`}
                    confirmButtonText="Yes, Create Deposit"
                    cancelButtonText="No, Cancel"
                    onConfirm={async () => await handleDeposit()}
                    onCancel={() => setShowConfirm(false)}
                    onClose={() => setShowConfirm(false)}
                />
            }
            {
                showError && <SweetAlert
                    title="Error!"
                    text={error}
                    icon="error"
                    timer={3000}
                    onClose={() => setShowError(false)}
                />
            }
            <Head>
                <title>
                    Primates - Make a fixed term
                </title>
            </Head>
            <Layout>
                <Box maxWidth="600px"
                    margin="0 auto"
                    boxShadow={3}
                    borderRadius={8}
                    border="1px solid #ccc"
                    p={6}
                    sx={{
                        backgroundImage: 'linear-gradient(to bottom, #ffffff, #f9f9f9)',
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}>
                    <Typography variant="h4" align="center" gutterBottom sx={{ fontWeight: '500', fontFamily: 'Helvetica Neue, sans-serif', letterSpacing: '1px' }}>
                        Make a Deposit
                    </Typography>
                    <Box display="flex" flexDirection="column" alignItems="center">
                        <FormControl margin="normal" fullWidth sx={{ marginBottom: '20px' }}>
                            <InputLabel htmlFor="amount" sx={{ color: 'gray', fontSize: '18px' }}>
                                Amount to Deposit
                            </InputLabel>
                            <Input
                                type="number"
                                id="amount"
                                disabled={disableInput}
                                value={amount}
                                onChange={handleAmountChange}
                                sx={{
                                    fontSize: '24px',
                                    fontWeight: 'bold',
                                    letterSpacing: '1px',
                                    padding: '12px',
                                    borderRadius: '5px',
                                    border: '1px solid lightgray'
                                }}
                            />
                        </FormControl>
                        <FormControl margin="normal" fullWidth>
                            <InputLabel htmlFor="term" style={{ fontWeight: 600 }}>Term</InputLabel>
                            <Select native inputProps={{ id: 'term' }} value={term} onChange={handleTermChange}
                                style={{ marginTop: '8px', fontWeight: 600 }}>
                                <option value="1-month" style={{ fontWeight: 600 }}>1 month - return %5</option>
                                <option value="3-months" style={{ fontWeight: 600 }}>3 months - return %19</option>
                                <option value="1-year" style={{ fontWeight: 600 }}>1 year - return %85</option>
                            </Select>
                        </FormControl>
                        {showResult ? (
                            <Box sx={{ margin: '1.5rem 0' }} display="flex" flexDirection="column" alignItems="center" textAlign="center">
                                <Typography variant="h6" gutterBottom sx={{ color: '#28a745', mb: '1rem' }}>
                                    Your return would be: ${result.toFixed(2)}
                                </Typography>
                                <Typography variant="subtitle1" gutterBottom sx={{ color: '#0077be', mb: '1rem' }}>
                                    Total amount: ${(Number(amount) + Number(result)).toFixed(2)}
                                </Typography>
                                <Button variant="contained" color="primary" onClick={() => setShowConfirm(true)} sx={{ minWidth: '160px', mb: '1rem' }}>
                                    Make deposit
                                </Button>
                                <Button variant="contained" color="secondary" onClick={handleReset} sx={{ minWidth: '160px' }}>
                                    Reset
                                </Button>
                            </Box>
                        ) : (
                            <Grid container
                                sx={{
                                    mt: 5,
                                    display: 'flex',
                                    justifyContent: 'space-evenly',
                                    alignItems: 'center',
                                    width: '80%',
                                    gap: '16px',
                                }}>
                                <Button variant="contained" color="primary" onClick={handleCalculate} sx={{ minWidth: '150px', minHeight: '50px', fontSize: '1.2rem' }}>
                                    Calculate
                                </Button>
                                <Button variant="contained" color="secondary" onClick={handleReset} sx={{ minWidth: '150px', minHeight: '50px', fontSize: '1.2rem' }}>
                                    Reset
                                </Button>
                            </Grid>
                        )}
                        <Box mt={6} sx={{ textAlign: 'center' }}>
                            <Typography variant="h6" gutterBottom>
                                Current account balance:
                            </Typography>
                            <Typography variant="h5" sx={{ fontWeight: 'bold', color: '#0077be' }}>
                                ${account.money}
                            </Typography>
                        </Box>
                    </Box>
                </Box>
            </Layout>
        </>
    );
};

export const getServerSideProps = async (context) => {

    const session = await getSession(context);
    const now = Math.floor(Date.now() / 1000);

    if (session == null || session.expires < now) {
        return {
            redirect: {
                destination: '/login',
                permanent: false,
            },
        };
    }
    const { data } = await axios.get(
        `https://localhost:7149/api/Account/${session?.user.accountId}`,
        {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",

            }
        }
    );

    return {
        props: {
            account: data
        }
    }

}

export default DepositComponent;