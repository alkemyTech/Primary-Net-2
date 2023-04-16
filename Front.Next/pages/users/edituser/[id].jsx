import { MyTextInput } from "@/components/login/MyTextInput";
import { Layout } from "@/layouts/Layout";
import { Button, Grid } from "@mui/material";
import axios from "axios";
import { Formik } from "formik";
import { getSession, signOut, useSession } from "next-auth/react";
import Head from "next/head";
import Link from "next/link";
import { useRouter } from "next/router";
import Swal from "sweetalert2";
import * as Yup from "yup";

const EditUserPage = () => {

    const router = useRouter();
    const { id } = router.query
    const { data: session } = useSession();
    const { first_Name, last_Name, email } = session?.user;

    const logout = () => {
        signOut({ callbackUrl: 'http://localhost:3000/login' })
    }
    const handleUpdate = async (values) => {

        if (!values.First_Name) values.First_Name = first_Name;
        if (!values.Last_Name) values.Last_Name = last_Name;
        if (!values.Email) values.Email = email;


        Swal.fire({
            title: `Do you want to Update this user ? \n (You will have to login again with your new data)`,

            showDenyButton: false,
            showCancelButton: true,
            confirmButtonText: 'Yes, update my account!',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                axios.put(` https://localhost:7149/api/User/${id}`, values,
                    {
                        headers: {
                            'Authorization': `Bearer ${session.user?.token}`,
                            "Content-Type": "application/json",
                        }
                    }
                )
                    .then(Swal.fire('Account Updated!, Loggin out to renew credentials', '', 'success'))
                    .then(logout())
            } else {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })


    }

    return (
        <>
            <Head>
                <title>
                    Primates - User edit
                </title>
            </Head>
            <Layout>

                <Formik
                    initialValues={{ Email: "", First_Name: "", Last_Name: "" }}
                    onSubmit={(values) => { handleUpdate(values, id) }}
                    validationSchema={Yup.object({
                        Email: Yup.string().email("Must be a valid email"),
                        Password: Yup.string()
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
                        <form onSubmit={(e) => handleSubmit(e)}>
                            <Grid container>

                                <MyTextInput label={"Name"}
                                    type={"text"}
                                    placeholder={"Primate"}
                                    name={"First_Name"}
                                    value={values.First_Name}
                                    onBlur={handleBlur}
                                    onChange={handleChange}
                                    errors={errors.First_Name}
                                />

                                <MyTextInput label={"Lastname"}
                                    type={"text"}
                                    placeholder={"Primary"}
                                    name={"Last_Name"}
                                    value={values.Last_Name}
                                    onBlur={handleBlur}
                                    onChange={handleChange}
                                    errors={errors.Last_Name}
                                />

                                <MyTextInput label={"Email"}
                                    type={"email"}
                                    placeholder={"example@google.com"}
                                    name={"Email"}
                                    value={values.Email}
                                    onBlur={handleBlur}
                                    onChange={handleChange}
                                    errors={errors.Email}
                                />


                            </Grid>


                            <Grid
                                container
                                spacing={2}
                                sx={{ mb: 2, mt: 1 }}
                            >

                                <Grid item xs={12} sm={6}>
                                    <Button
                                        type="submit"
                                        variant="contained"
                                        fullWidth
                                        sx={{ padding: 1 }}
                                    >
                                        Update User
                                    </Button>
                                </Grid>


                                <Grid item xs={12} sm={6}>
                                    <Button
                                        onClick={() => router.back()}
                                        type="button"
                                        variant="contained"
                                        sx={{ backgroundColor: "tertiary.main", padding: 1 }}
                                        fullWidth
                                    >
                                        Return
                                    </Button>
                                </Grid>
                            </Grid>
                        </form>
                    )}
                </Formik>
            </Layout>
        </>

    )
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

        return {
            props: {
                data: null
            }
        }
    } catch (error) {
        return {
            props: {
                data: null
            }
        }
    }
}

export default EditUserPage;