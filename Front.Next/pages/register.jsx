import { MyTextInput } from "@/components/login/MyTextInput"
import { AuthLayout } from "@/layouts/AuthLayout"
import { Grid, TextField, Alert, Button, Google, Typography, Link } from "@mui/material"
import * as Yup from "yup";
import { Formik } from "formik";
import { getSession, useSession } from "next-auth/react";
import https from 'https';
import axios from "axios";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { SweetAlert } from "@/components/alerts/SweetAlert";
import Head from "next/head";

const Register = () => {

    const router = useRouter();

    const [showAlert, setShowAlert] = useState(false)
    const [success, setSuccess] = useState(false)
    const [emailAlreadyRegistered, setEmailAlreadyRegistered] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    const handleRegister = async ({ First_Name, Last_Name, Email, Password }) => {
        try {

            const res = await axios.post(`https://localhost:7149/api/User/signup`,
                { First_Name, Last_Name, Email, Password },
                {
                    httpsAgent: new https.Agent({
                        rejectUnauthorized: false
                    }),
                }
            );

            if (res && res.data.statusCode == 201) {
                setSuccess(true);
                router.push('/login');
            }
        } catch (error) {
            const { response: data } = error;
            const message = data.data.errors[0].error;
            if (message && message == "Email already registered.") setEmailAlreadyRegistered(true);
            else {
                setShowAlert(true);
                setErrorMessage(message);
            }
        }

    }

    return (
        <>
            {
                success && <SweetAlert
                    title="Successful Registration"
                    text="Your account has been registered successfully."
                    icon="success"
                    timer={5000}
                    onClose={() => setSuccess(false)}

                />
            }
            {
                emailAlreadyRegistered && <SweetAlert
                    title="Registration Error"
                    text="The email address you entered is already registered. Please try again with a different email address."
                    icon="error"
                    timer={5000}
                    onClose={() => setEmailAlreadyRegistered(false)}
                />
            }
            {
                showAlert && <SweetAlert
                    title="Error"
                    text={errorMessage}
                    icon="error"
                    timer={5000}
                    onClose={() => setShowAlert(false)}
                />
            }
            <Head>
                <title>
                    Primates - Register
                </title>
            </Head>
            <AuthLayout title={"Register"}>
                <Formik
                    initialValues={{ name: "", lastname: "", email: "", password: "", confirmPassword: "" }}
                    onSubmit={async (values, { setSubmitting }) => {
                        await handleRegister({ First_Name: values.name, Last_Name: values.lastname, Email: values.email, Password: values.password })
                        //TODO redirect to login.
                    }}
                    validationSchema={Yup.object({
                        name: Yup.string()
                            .max(50, "Must be 50 chars long or shorter")
                            .required("Required"),
                        lastname: Yup.string()
                            .max(15, "Must be 15 chars long or shorter")
                            .required("Required"),
                        email: Yup.string().email("Invalid Email")
                            .required("Required")
                            .max(100, "Max length is 100"),
                        password: Yup.string().required().matches(
                            /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/,
                            "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number and One Special Case Character"
                        ),
                        confirmPassword: Yup.string()
                            .oneOf([Yup.ref('password'), null], 'Passwords must match')
                    })}
                >
                    {({ values,
                        errors,
                        touched,
                        handleChange,
                        handleBlur,
                        handleSubmit,
                        isSubmitting,
                    }) => (
                        <form onSubmit={handleSubmit}>
                            <Grid container>

                                <MyTextInput label={"Name"}
                                    type={"text"}
                                    placeholder={"Primary"}
                                    name={"name"}
                                    onChange={handleChange}
                                    value={values.name}
                                    onBlur={handleBlur}
                                    errors={errors.name}
                                    touched={touched.name}

                                />

                                <MyTextInput label={"Lastname"}
                                    type={"text"}
                                    placeholder={"Primate"}
                                    name={"lastname"}
                                    onChange={handleChange}
                                    value={values.lastname}
                                    onBlur={handleBlur}
                                    errors={errors.lastname}
                                    touched={touched.lastname}

                                />


                                <MyTextInput label={"Email"}
                                    type={"email"}
                                    placeholder={"example@google.com"}
                                    name={"email"}
                                    onChange={handleChange}
                                    value={values.email}
                                    onBlur={handleBlur}
                                    errors={errors.email}
                                    touched={touched.email}

                                />

                                <MyTextInput label={"Password"}
                                    type={"password"}
                                    placeholder={"********"}
                                    name={"password"}
                                    onChange={handleChange}
                                    value={values.password}
                                    onBlur={handleBlur}
                                    errors={errors.password}
                                    touched={touched.password}

                                />

                                <MyTextInput label={"Confirm Password"}
                                    type={"password"}
                                    placeholder={"********"}
                                    name={"confirmPassword"}
                                    onChange={handleChange}
                                    value={values.confirmPassword}
                                    onBlur={handleBlur}
                                    errors={errors.confirmPassword}
                                />
                            </Grid>


                            <Grid
                                container
                                spacing={2}
                                sx={{ mb: 2, mt: 1 }}
                            >

                                <Grid item xs={12}>
                                    <Button
                                        type="submit"
                                        variant="contained"
                                        fullWidth
                                        sx={{ padding: 1 }}
                                    >
                                        Register
                                    </Button>
                                </Grid>

                                <Grid
                                    container
                                    direction={"row"}
                                    justifyContent={"end"}
                                    sx={{ mt: 1 }}
                                >

                                    <Typography textAlign={"center"} sx={{ mr: 1 }}>
                                        Already have an account?  <Link href="/login" fontWeight={"bold"} color="#FB923C">Login here</Link>
                                    </Typography>
                                </Grid>

                            </Grid>

                        </form>

                    )}
                </Formik>
            </AuthLayout>
        </>




    )
}

export async function getServerSideProps(context) {

    const session = await getSession(context);

    if (session && session.expires > Math.floor(Date.now() / 1000)) {
        return {
            redirect: {
                destination: '/',
                permanent: false,
            },
        };
    }

    return { props: {} };
}


export default Register;
