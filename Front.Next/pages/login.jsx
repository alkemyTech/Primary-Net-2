import { SweetAlert } from "@/components/alerts/SweetAlert";
import { MyTextInput } from "@/components/login/MyTextInput"
import { AuthLayout } from "@/layouts/AuthLayout"
import { Formik } from "formik"
import { signIn, useSession, signOut, getSession, destroy } from "next-auth/react";
import Head from "next/head";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { Grid, TextField, Alert, Button, Google, Typography, Link } from "@mui/material"
import { redirect } from "next/dist/server/api-utils";
import * as Yup from "yup";


const Login = () => {

  const [showAlert, setShowAlert] = useState(false);
  const [showInvalidCredentials, setShowInvalidCredentials] = useState(false)

  const router = useRouter();
  const { data: session } = useSession();

  useEffect(() => {

    const now = Math.floor(Date.now() / 1000);

    if (session?.expires && session.expires < now) {
      setShowAlert(true);
      signOut({ redirect: false, callbackUrl: '/login' });
    }

  }, [session, router]);

  return (
    <>
      <Head>
        <title>
          Primates - Login
        </title>
      </Head>
      {(showAlert && <SweetAlert
        title="Session Expired!"
        text="Your session has expired. Please log in again."
        icon="error"
        timer={10000}
        onClose={() => setShowAlert(false)}
      />)
      }
      {
        showInvalidCredentials && <SweetAlert
          title="Invalid Credentials"
          text="The email or password you entered is incorrect. Please try again."
          icon="error"
          timer={10000}
          onClose={() => { setShowInvalidCredentials(false) }}
        />
      }
      <AuthLayout title={"Login"}>

        <Formik
          initialValues={{ UserName: "", Password: "" }}
          onSubmit={async (values, { setSubmitting, resetForm }) => {
            let res = await signIn("credentials", { ...values, redirect: false })
            if (res.ok == false) {
              setShowInvalidCredentials(true);
              setSubmitting(false);
              await signOut({ redirect: false, callbackUrl: '/login' });
            }
            else {
              router.push('/')
            }
          }}
          validationSchema={Yup.object({
            UserName: Yup.string().email("Must be a valid email").required(),
            Password: Yup.string().required()
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
            <form onSubmit={(event) => {
              event.preventDefault();
              handleSubmit();
            }}>
              <Grid container>
                <MyTextInput label={"Email"}
                  type={"email"}
                  placeholder={"example@google.com"}
                  name={"UserName"}
                  value={values.UserName}
                  onBlur={handleBlur}
                  onChange={handleChange}
                  errors={errors.UserName}
                // touched={touched.UserName}
                />

                <MyTextInput label={"Password"}
                  type={"password"}
                  placeholder={"********"}
                  name={"Password"}
                  value={values.Password}
                  onBlur={handleBlur}
                  onChange={handleChange}
                  errors={errors.Password}
                // touched={touched.Password}
                />
              </Grid>


              <Grid
                container
                spacing={2}
                sx={{ mb: 2, mt: 1 }}
              >

                <Grid item xs={12} sm={6}>
                  <Button
                    disabled={isSubmitting}
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
                      disabled={isSubmitting}
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

export default Login
