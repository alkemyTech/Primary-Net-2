import { AccountDetailsView } from '@/components/adminPanel/accounts/AccountDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'


const AccountDetailsPage = ({ account }) => {
  return (
    <Layout>
      {account && <AccountDetailsView {...account} />}
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  try {
    const session = await getSession(context);
    const { id } = context.params;
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
    console.log(account)
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