import { RoleDetailsView } from '@/components/adminPanel/roles/RoleDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'
import Head from 'next/head'


const RoleDetailsPage = ({ role }) => {
  return (
    <>
      <Head>
        <title>
          Primates - Admin role details
        </title>
      </Head>
      <Layout>
        {role && <RoleDetailsView {...role} />}
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
    const res = await fetch(`https://localhost:7149/api/Role/${id}`, {
      method: 'get',
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
      agent: new https.Agent({
        rejectUnauthorized: false
      })
    });
    const role = await res.json();
    return {
      props: {
        role,
      }
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        role: null
      }
    };
  }
};

export default RoleDetailsPage;