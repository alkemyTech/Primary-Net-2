import { UserDetailsView } from '@/components/users/UserDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession, signOut } from 'next-auth/react'
import axios from 'axios'
import Head from 'next/head'
import { date } from 'yup'


const userDetailsPage = ({ user }) => {

    console.log(user)

    return (
        <Layout>
            <Head>
                <title>
                    Primates Admin - users
                </title>
            </Head>
            <UserDetailsView {...user} />
        </Layout>
    )
}

export const getServerSideProps = async (context) => {
    try {
        //Forma de obtener el id de la url
        const { id } = context.params;

        console.log("Este es el id" + id);

        //obtener la sesión next auth
        const session = await getSession(context);
        //obtener el tiempo actual
        const now = Math.floor(Date.now() / 1000);

        if(session == null){
            return {
                redirect: {
                    destination: '/login',
                    permanent: false,
                },
            };
        }

        if (!session || (session && session.expires && session.expires < now)) { 
            return {
                redirect: {
                    destination: '/login?sessionexpired=true',
                    permanent: false,
                },
            };
        }
        if (session.user?.rol && session.user.rol != "Admin") {
            return {
              redirect: {
                destination: '/',
                permanent: false,
              },
            };
          }

          const { data } = await axios.get(
            `https://localhost:7149/api/User/${id}`,
            {
              headers:{
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",

            },
              httpsAgent: new https.Agent({
                rejectUnauthorized: false
            }),
            }
          );

        console.log(data)

        
        return {
            props: {
                user: data.result
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                user: null // Retorna null si hay algún error
            }
        };
    }
};

export default userDetailsPage;