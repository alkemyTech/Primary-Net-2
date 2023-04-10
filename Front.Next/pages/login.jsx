import { MyTextInput } from "@/components/login/MyTextInput"
import { AuthLayout } from "@/layouts/AuthLayout"
import { Grid, TextField, Alert, Button, Google, Typography, Link } from "@mui/material"
import { Formik, Form, useFormik } from "formik"
import * as Yup from "yup";

const login = () => {

  return (
    <AuthLayout title={"Login"}>

      <Formik
        initialValues={{ email: "", password: "" }}
        onSubmit={(values, { setSubmitting }) => { console.log(values) }}
        validationSchema={Yup.object({
          email: Yup.string().email("Must be a valid email").required(),
          password: Yup.string().required()
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
              <MyTextInput label={"Email"}
                type={"email"}
                placeholder={"example@google.com"}
                name={"email"}
                value={values.email}
                onBlur={handleBlur}
                touched
                onChange={handleChange}
                errors={errors.email}
              />

              <MyTextInput label={"Password"}
                type={"password"}
                placeholder={"********"}
                name={"password"}
                value={values.password}
                onBlur={handleBlur}
                touched
                onChange={handleChange}
                errors={errors.password}
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

export default login