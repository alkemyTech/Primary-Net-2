import { ConfirmSweetAlert } from "@/components/alerts/ConfirmSweetAlert";
import { SweetAlert } from "@/components/alerts/SweetAlert";
import { Layout } from "@/layouts/Layout";
import { Box, Button, FormControlLabel, Grid, Switch, TextField, Typography } from "@mui/material";
import axios from "axios";
import { getSession, useSession } from "next-auth/react";
import Head from "next/head";
import { useRouter } from "next/router";
import { useState } from "react";

const UpdateAccountForm = ({ account }) => {
    const [money, setMoney] = useState(account?.money);
    const [isBlocked, setIsBlocked] = useState(account?.isBlocked);
    const [showConfirm, setShowConfirm] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false)
    const [showError, setShowError] = useState(false)

    let currentBalance = account?.money;

    const { data: session } = useSession();

    const router = useRouter();

    const handleMoneyChange = (event) => {
        setMoney(event.target.value);
    };

    const handleBlockedChange = (event) => {
        setIsBlocked(event.target.checked);
    };

    const handleSubmit = async () => {

        try {
            const { data, status } = await axios.put(`https://localhost:7149/api/Account/${account?.id}`,
                {
                    money,
                    isBlocked
                },
                {
                    headers: {
                        'Authorization': `Bearer ${session.user?.token}`,
                        "Content-Type": "application/json",
                    }
                },
            )

            if(status == 200) {
                setShowSuccess(true);
                router.push('/admin/accounts');
            }

        } catch (error) {
            setShowError(true);
            console.log(error);
        }

    };

    return (
        <>
        <Head>
            <title>Primates - Admin accounts update</title>
        </Head>
            {
                showConfirm && <ConfirmSweetAlert
                    title='Confirmation'
                    text={`Are you sure you want to update the account ${account.id}?`}
                    confirmButtonText='Yes'
                    cancelButtonText='No'
                    onConfirm={handleSubmit}
                    onCancel={() => setShowConfirm(false)}
                    onClose={() => setShowConfirm(false)}
                />
            }
            {
                showSuccess && <SweetAlert
                    title='Success!'
                    text='The account has been updated successfully.'
                    icon= 'success'
                    timer= {3000}
                    onClose={() => setShowSuccess(false)}
                />
            }
            {
                showError && <SweetAlert
                    title= 'Error!'
                    text= 'There was an error updating the account.'
                    icon= 'error'
                    timer= {3000}
                    onClose={() => setShowError(false)}
                 />
            }
            <Layout>
                <Grid container sx={{ display: 'flex', alignItems: 'center', justifyContent: 'center', mt: 4 }}>

                    <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4, bgcolor: '#F5F5F5', borderRadius: 5, boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.2)', width: '800px' }}>
                        <Typography variant="h3" component="h2" gutterBottom>
                            Update account {account.id}
                        </Typography>
                        <form onSubmit={(event) => {
                            event.preventDefault();
                            setShowConfirm(true);
                            }}>

                            <Box sx={{ display: 'flex', flexDirection: 'column', width: '100%', maxWidth: 500, mt: 2 }}>
                                <Box sx={{ display: 'flex', flexDirection: 'column', mb: 2 }}>
                                    <Typography variant="subtitle1" sx={{ fontWeight: 600, fontSize: '1.2rem' }}>
                                        Current balance:
                                    </Typography>
                                    <Typography variant="body1" sx={{ ml: 2 }}>
                                        {currentBalance}
                                    </Typography>
                                </Box>
                                <TextField
                                    sx={{ mb: 2 }}
                                    label={"New balance"}
                                    variant="outlined"
                                    type="number"
                                    value={money}
                                    onChange={handleMoneyChange}
                                />
                                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start', mb: 2 }}>
                                    <Typography variant="subtitle1" sx={{ fontWeight: 600, fontSize: '1.2rem' }}>
                                        Account blocked:
                                    </Typography>
                                    <FormControlLabel
                                        sx={{ fontWeight: 600, fontSize: '1.1rem', mt: 1 }}
                                        control={
                                            <Switch
                                                checked={isBlocked}
                                                onChange={handleBlockedChange}
                                                color="primary"
                                            />
                                        }
                                        label={isBlocked ? 'Blocked' : 'Unblocked'}
                                        labelPlacement="start"
                                    />
                                </Box>
                                <Button
                                    sx={{ mt: 3, alignSelf: 'center' }}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                >
                                    Update account
                                </Button>
                            </Box>
                        </form>
                    </Box>
                </Grid>
            </Layout>
        </>
    );
}

export const getServerSideProps = async (context) => {

    try {
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

        if (session.user.rol != 'Admin') {
            return {
                redirect: {
                    destination: '/?invalidcredentials=true',
                    permanent: false,
                },
            };
        }


        const { id } = context.params;
        const { data } = await axios.get(`https://localhost:7149/api/Account/${id}`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        }
        );
        return {
            props: {
                account: data,
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                account: null
            }
        };
    }

}

export default UpdateAccountForm;
