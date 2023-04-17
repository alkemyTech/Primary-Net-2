import { ConfirmSweetAlert } from '@/components/alerts/ConfirmSweetAlert';
import { SweetAlert } from '@/components/alerts/SweetAlert';
import { Layout } from '@/layouts/Layout';
import { Box, Button, Grid, TextField, Typography } from '@mui/material';
import axios from 'axios';
import { Formik } from 'formik';
import { getSession, useSession } from 'next-auth/react';
import Head from 'next/head';
import { useRouter } from 'next/router';
import React, { useState } from 'react'
import * as Yup from "yup";

const UpdateCatalogue = ({ product }) => {

    const [newProduct, setNewProduct] = useState(null);
    const [showConfirm, setShowConfirm] = useState(false);
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState(false)
    const router = useRouter();

    const { data: session } = useSession();

    const { id, productDescription, image, points } = product;


    const handleUpdateProduct = async () => {
        try {
            const { data, status } = await axios.put(`https://localhost:7149/api/Catalogue/${id}`, newProduct, {
                headers: {
                    'Authorization': `Bearer ${session.user?.token}`,
                    "Content-Type": "application/json",
                }
            });

            setSuccess(true);
        } catch (error) {
            setError(true);
            console.log(error);
        }
    }


    const handleCancel = () => {
        router.back(); // Regresar a la página anterior
    };

    return (
        <>
            {
                showConfirm && <ConfirmSweetAlert cancelButtonText={"Cancel"} confirmButtonText={"Yes"} title={"Update product"} text={"Are you sure you want to update this product?"} onConfirm={handleUpdateProduct} onClose={() => setShowConfirm(false)} onCancel={() => setShowConfirm(false)} />
            }
            {
                success && <SweetAlert title={"Success"} text={"Updated product succesfully"} icon={"success"} timer={3000}
                    onClose={() => {
                        setSuccess(false)
                        router.push('/admin/catalogues')
                    }} />
            }
            {
                error && <SweetAlert title={"Error"} text={"An error ocurred, please try later"} icon={"error"} timer={3000} onClose={() => setError(false)} />
            }
            <Head>
                <title>
                    Primates - Admin update product
                </title>
            </Head>
            <Layout>
                <Grid container sx={{ width: '100%', display: 'flex', justifyContent: 'center', mt:3}}>

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
                        <Typography variant="h4" sx={{ mb: 4 }}>
                            Update Product
                        </Typography>
                        <Formik
                            initialValues={{ description: productDescription, url: image, value: points }}
                            onSubmit={async (values, { setSubmitting }) => {
                                setNewProduct({
                                    id,
                                    productDescription: values.description,
                                    image: values.url,
                                    points: values.value
                                });
                                setShowConfirm(true);
                            }}
                            validationSchema={Yup.object({
                                description: Yup.string()
                                    .max(50, "Must be 50 chars long or shorter")
                                    .required("Required"),
                                url: Yup.string()
                                    .max(500, "Must be 500 chars long or shorter")
                                    .required("Required"),
                                value: Yup.number()
                                    .required("Required"),
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
                                    <Grid container spacing={3}>
                                        <Grid item xs={12}>
                                            <Typography variant="body1" sx={{ fontWeight: "bold", mb: 1 }}>
                                                Current Description: {productDescription}
                                            </Typography>
                                            <TextField
                                                name="description"
                                                fullWidth
                                                label="Product Description"
                                                variant="outlined"
                                                value={values.description}
                                                onChange={handleChange}
                                                error={errors.description
                                                }
                                                required
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <TextField
                                                name="url"
                                                fullWidth
                                                label="Image URL"
                                                variant="outlined"
                                                value={values.url}
                                                onChange={handleChange}
                                                error={errors.url}
                                                required
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Typography variant="body1" sx={{ fontWeight: "bold", mb: 1 }}>
                                                Current Points: {points}
                                            </Typography>
                                            <TextField
                                                name="value"
                                                fullWidth
                                                label="Points"
                                                variant="outlined"
                                                type="number"
                                                value={values.value}
                                                onChange={handleChange}
                                                error={errors.value}
                                                required
                                            />
                                        </Grid>
                                        <Grid item xs={12} sx={{ display: "flex", justifyContent: "center" }}>
                                            <Button variant="outlined" color="tertiary" onClick={handleCancel} sx={{ mr: 2 }}>
                                                Back
                                            </Button>
                                            <Button type="submit" variant="contained" color="primary">
                                                Update
                                            </Button>
                                        </Grid>
                                    </Grid>
                                </form>
                            )}
                        </Formik>
                    </Box>
                </Grid>
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

        if (session.user.rol != 'Admin') {
            return {
                redirect: {
                    destination: '/?invalidcredentials=true',
                    permanent: false,
                },
            };
        }
        const { id } = context.params;

        const { data } = await axios.get(`https://localhost:7149/api/Catalogue/${id}`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });


        return {
            props: {
                product: data
            }
        }

    } catch (error) {
        return {
            props: {
                product: null
            }
        }
    }
}

export default UpdateCatalogue;