import { MyTextInput } from "@/components/MyTextInput"
import { AuthLayout } from "@/layouts/AuthLayout"
import { Grid, Button, Link } from "@mui/material"
import axios from "axios";
import { Formik } from "formik"
import { useRouter } from "next/router";
import * as Yup from "yup";


export default function LoginPage(){

    const router = useRouter();
    const onSubmit = async (e, values) => {
        e.preventDefault();
        const response = await axios.post("https://localhost:7149/api/Auth/login", values)
        console.log(response);
        router.push("/")


    }


    return (
        <AuthLayout title={"Login"}>

            <Formik
                initialValues={{ UserName: "", Password: "" }}
                onSubmit={onSubmit}
                validationSchema={Yup.object({
                    UserName: Yup.string().email("Must be a valid email").required(),
                    Password: Yup.string().required()
                })}
            >
                {({ values,
                    errors,
                    handleChange,
                    handleBlur,
                }) => (
                    <form onSubmit={(e)=>onSubmit(e, values)}>
                        <Grid container>
                            <MyTextInput label={"Email"}
                                type={"email"}
                                placeholder={"example@google.com"}
                                name={"UserName"}
                                value={values.UserName}
                                onBlur={handleBlur}
                                onChange={handleChange}
                                errors={errors.UserName}
                            />

                            <MyTextInput label={"Password"}
                                type={"password"}
                                placeholder={"********"}
                                name={"Password"}
                                value={values.Password}
                                onBlur={handleBlur}
                                onChange={handleChange}
                                errors={errors.Password}
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
                                    Login
                                </Button>
                            </Grid>


                            <Grid item xs={12} sm={6}>
                                <Link href="/register">
                                    <Button
                                        type="button"
                                        variant="contained"
                                        sx={{ backgroundColor: "tertiary.main", padding: 1 }}
                                        fullWidth
                                    >
                                        Register
                                    </Button>
                                </Link>
                            </Grid>
                        </Grid>
                    </form>
                )}
            </Formik>

        </AuthLayout>
    )
}

