import { ConfirmSweetAlert } from '@/components/alerts/ConfirmSweetAlert';
import { SweetAlert } from '@/components/alerts/SweetAlert';
import { Layout } from '@/layouts/Layout';
import { Box, Button, Divider, FormControl, Grid, Input, InputLabel, ListSubheader, Select, Typography } from '@mui/material';
import axios from 'axios';
import { getSession, useSession } from 'next-auth/react';
import { useRouter } from 'next/router';
import React, { useState } from 'react'

const EditFixedTerm = ({ fixed, account }) => {


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
    const { id } = router.query;

    const formatDate = (date = "") => {
        const day = date.slice(0, 10)
        // const time = date.slice(11, 16)
        return `${day}`
    }

    const calcutatePreviousInterest = (creation, end) => {
        const initDate = new Date(creation);
        const closeDate = new Date(end);

        // Calcular la diferencia en milisegundos
        const msDifference = closeDate - initDate;

        // Convertir la diferencia de milisegundos a días
        const days = Math.floor(msDifference / (1000 * 60 * 60 * 24));

        let interest = 0
        if (days >= 365) interest = 0.85
        if (days >= 90) interest = 0.19
        if (days >= 30) interest = 0.05
        // Retornar el interes segun el tiempo del plazo fijo
        return interest;
    }



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
        let creation_date = new Date(); // Fecha y hora actual
        let closing_date = new Date(); // Inicializamos la fecha de cierre con la fecha actual
        let prev_creation_date = new Date(fixed.creation_Date).toISOString(); // Inicializamos la fecha de cierre con la fecha actual
        let prev_closing_date = new Date(fixed?.closing_Date).toISOString(); // Inicializamos la fecha de cierre con la fecha actual

        // Calculamos la fecha de cierre según la opción seleccionada
        if (term === "1-month") {
            closing_date.setMonth(creation_date.getMonth() + 1);
        } else if (term === "3-months") {
            closing_date.setMonth(creation_date.getMonth() + 3);
        } else if (term === "1-year") {
            closing_date.setFullYear(creation_date.getFullYear() + 1);
        }
        const formattedCreationDate = creation_date.toISOString().slice(0, 23);
        const formattedPreviousCreationDate = prev_creation_date
        const formattedClosingDate = closing_date.toISOString().slice(0, 23);
        const formattedPreviousClosingDate = prev_closing_date
        try {
            // Por si no recibe modificaciones del usuario en los cambios toma los anteriores
            if (!amount) setAmount(fixed.amount / (1 + calcutatePreviousInterest(fixed.creation_Date, fixed.closing_Date)));
            if (!creation_date) creation_date = formattedPreviousCreationDate;
            if (!closing_date) closing_date = formattedPreviousClosingDate;


            const { data } = await axios.put(`https://localhost:7149/api/FixedDeposit/${id}`,
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


            <Layout >



                <Grid container sx={{ backgroundColor: "#fff", width: "80vw", borderRadius: 5, p: 1 }}>

                    <Typography variant='h4' pb={2}>
                        Edit Fixed Term Deposit #{id}
                    </Typography>

                    <Grid container sx={{ width: "100%" }}>
                        <Typography variant='h5'>Previous Values:</Typography>

                        <Grid container display={"flex"} direction={"row"} sx={{ width: "100%", borderRadius:2  }}>
                            <Grid copntainer display={"grid"} direction={"column"} sx={{ border: "1px solid #eee", width: "33%" }}>
                                <Grid item sx={{ width: "100%", backgroundColor: "#DDD" }}>
                                    Return Amount:
                                </Grid>
                                <Grid item p={1}>
                                    <Typography variant="h6" sx={{ fontWeight: "bold", textAlign: "center" }}>${fixed.amount}</Typography>
                                </Grid>
                            </Grid>

                            <Grid copntainer display={"grid"} direction={"column"} sx={{ border: "1px solid #eee", width: "33%" }}>
                                <Grid item sx={{ width: "100%", backgroundColor: "#DDD" }}>
                                    Creation Date
                                </Grid>
                                <Grid item p={1}>
                                    <Typography variant="h6" sx={{ fontWeight: "bold", textAlign: "center" }}> {formatDate(fixed.creation_Date)}</Typography>
                                </Grid>
                            </Grid>

                            <Grid copntainer display={"grid"} direction={"column"} sx={{ border: "1px solid #eee", width: "33%", borderRadius:1 }}>
                                <Grid item sx={{ width: "100%", backgroundColor: "#DDD" }}>
                                    Closing Date
                                </Grid>
                                <Grid item p={1}>
                                    <Typography variant="h6" sx={{ fontWeight: "bold", textAlign: "center" }}> {formatDate(fixed.closing_Date)}</Typography>
                                </Grid>
                            </Grid>

                        </Grid>



                                <Typography variant="h5" align="center" gutterBottom sx={{ pt:2 ,fontWeight: '500', fontFamily: 'Helvetica Neue, sans-serif', letterSpacing: '1px' }}>
                                    New Values:
                                </Typography>

                        <Grid container sx={{ width: "100%", pt: 2, border: "0.5px solid #ddd", borderRadius: 2, p: 1, justifyContent:"center", alignItems:"center" }}>

                       
                            <Box display="flex" flexDirection="row" alignItems="center" justifyContent={"center"}>

                                <Grid container flexDirection={"column"}>
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

                                </Grid>

                               
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
                                            flexDirection:"column"
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
                            {/* </Box>






                        </Grid> */}

                        </Grid>

                    </Grid>
                </Grid>


            </Layout>
        </>
    )
}


export const getServerSideProps = async (context) => {
    try {

        const { id } = context.query;
        const session = await getSession(context);

        const { data } = await axios.get(
            `https://localhost:7149/api/Account/${session?.user.accountId}`,
            {
                headers: {
                    'Authorization': `Bearer ${session.user?.token}`,
                    "Content-Type": "application/json",

                }
            }
        );


        const res = await axios.get(`https://localhost:7149/api/FixedDeposit/${id}`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });


        return {
            props: {
                account: data,
                fixed: res.data.result
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                fixed: null // Retorna null si hay algún error
            }
        };
    }
};


export default EditFixedTerm;