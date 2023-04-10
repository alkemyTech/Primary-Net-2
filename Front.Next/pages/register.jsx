import { MyTextInput } from "@/components/login/MyTextInput"
import { AuthLayout } from "@/layouts/AuthLayout"
import { Grid, TextField, Alert, Button, Google, Typography, Link } from "@mui/material"
import * as Yup from "yup";
import {  Formik } from "formik";

const register = () => {



    return (

        <AuthLayout title={"Register"}>
            <Formik
                initialValues={{ name: "", lastname: "", email: "", password: "", confirmPassword: "" }}
                onSubmit={ (values, {setSubmitting} ) => {console.log(values)} }
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
                                touched={touched}
                                
                            />

                            <MyTextInput label={"Lastname"}
                                type={"text"}
                                placeholder={"Primate"}
                                name={"lastname"}
                                onChange={handleChange}
                                value={values.lastname}
                                onBlur={handleBlur}
                                errors={errors.lastname}
                                touched={touched}

                            />


                            <MyTextInput label={"Email"}
                                type={"email"}
                                placeholder={"example@google.com"}
                                name={"email"}
                                onChange={handleChange}
                                value={values.email}
                                onBlur={handleBlur}
                                errors={errors.email}
                                touched={touched}

                            />

                            <MyTextInput label={"Password"}
                                type={"password"}
                                placeholder={"********"}
                                name={"password"}
                                onChange={handleChange}
                                value={values.password}
                                onBlur={handleBlur}
                                errors={errors.password}
                                touched={touched}

                            />

                            <MyTextInput label={"Confirm Password"}
                                type={"password"}
                                placeholder={"********"}
                                name={"confirmPassword"}
                                onChange={handleChange}
                                value={values.confirmPassword}
                                onBlur={handleBlur}
                                errors={errors.confirmPassword}
                                touched={touched}

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



    )
}

export default register