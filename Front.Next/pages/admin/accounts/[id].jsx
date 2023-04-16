import { AccountDetailsView } from '@/components/adminPanel/accounts/AccountDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'
import Head from 'next/head'


const AccountDetailsPage = ({ account }) => {
  return (
    <>
      <Head>
        <title>
          Primates- Admin account details
        </title>
      </Head>
      <Layout>
        {account && <AccountDetailsView {...account} />}
      </Layout>
    </>
  )
}

export const getServerSideProps = async (context) => {
  try {
    const { id } = context.params;

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

    const res = await fetch(`https://localhost:7149/api/Account/${id}`, {
      method: 'get',
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
      agent: new https.Agent({
        rejectUnauthorized: false
      })
    });
    const account = await res.json();
    return {
      props: {
        account,
      }
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        account: null
      }
    };
  }
};

export default AccountDetailsPage;