import { UserDetailsView } from '@/components/users/UserDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'
import Head from 'next/head'


const userDetailsPage = ({ user }) => {
  return (
    <>
      <Head>
        <title>
          Primates - My profile
        </title>
      </Head>
      <Layout>
        <UserDetailsView {...user} />
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
    const { params } = context
    const { id } = params

    const res = await fetch(`https://localhost:7149/api/User/${id}`, {
      method: 'get',
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",

      },
      agent: new https.Agent({
        rejectUnauthorized: false
      })
    });

    const { result } = await res.json();
    return {
      props: {
        user: result
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