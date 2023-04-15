import { Layout } from "@/Layouts/Layout";
import { MyTextInput } from "@/components/login/MyTextInput"
import { headers } from "@/next.config";
import { Grid, Button, Link, Typography } from "@mui/material"
import axios from "axios";
import { Formik } from "formik"
import { getSession } from "next-auth/react";
import Head from "next/head";
import { useRouter } from "next/router";
import Swal from "sweetalert2";
import * as Yup from "yup";


const CreateRole = () => {
    const router = useRouter();

    const onSubmit = async (e, values) => {
        e.preventDefault();
        try {
            const session = await getSession()
            const { data } = await axios.post("https://localhost:7149/api/Role", values, {
                headers: {
                    'Authorization': `Bearer ${session.user?.token}`,
                    "Content-Type": "application/json"
                }
            })
            Swal.fire(
                `${data}`,
                ``,
                'success'
            )
            router.back()
        }
        catch (error) {
            Swal.fire(
                `${error.message}`,
                ``,
                'error'
            )
        }

    }


    return (
        <>
            <Head>
                <title>
                    Primates - Admin create new role
                </title>
            </Head>
            <Layout title={"Create Role"}>

                <Typography variant="h4">
                    Create Role
                </Typography>
                <Grid
                    container
                    display={"flex"}
                    flexDirection={"column"}
                    fullWidth
                    alignItems={"center"}
                    justifyContent={"center"}
                    sx={{ p: 4, borderRadius: 5, backgroundColor: "white", mt: 4 }}
                >
                    <Grid
                        container
                        width={"50%"}
                    >



                        <Formik

                            initialValues={{ name: "", description: "" }}
                            onSubmit={onSubmit}
                            validationSchema={Yup.object({
                                name: Yup.string().max(7, "Maximun length is 7 characters long"),
                                description: Yup.string().min(10, "Please add a bried description")
                            })}
                        >
                            {({
                                values,
                                errors,
                                handleChange,
                                handleBlur
                            }) => (
                                <form onSubmit={(e) => onSubmit(e, values)}>
                                    <Grid container>
                                        <MyTextInput label={"Name"}
                                            type={"text"}
                                            placeholder={"New role name..."}
                                            name={"name"}
                                            value={values.name}
                                            onBlur={handleBlur}
                                            onChange={handleChange}
                                            errors={errors.name}
                                        />

                                        <MyTextInput label={"Description"}
                                            type={"text"}
                                            placeholder={"Please add a description"}
                                            name={"description"}
                                            value={values.description}
                                            onBlur={handleBlur}
                                            onChange={handleChange}
                                            errors={errors.description}
                                            multiline
                                            rows={4}
                                        />

                                    </Grid>

                                    <Grid container fullWidth justifyContent={"center"} alignItems={"center"}>

                                        <Grid item xs={12} sm={6}>
                                            <Button

                                                type="submit"
                                                variant="contained"
                                                fullWidth
                                                sx={{
                                                    backgroundColor: "tertiary.main",
                                                    padding: 1,
                                                    mt: 2
                                                }}
                                            >
                                                Create Role
                                            </Button>
                                        </Grid>
                                    </Grid>



                                </form>
                            )}


                        </Formik>
                    </Grid>
                </Grid>

            </Layout>
        </>

    )
}

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

    if (session.user.rol != 'Admin') {
        return {
            redirect: {
                destination: '/?invalidcredentials=true',
                permanent: false,
            },
        };
    }

    return {
        props: {
            data: null
        }
    }
}

export default CreateRole