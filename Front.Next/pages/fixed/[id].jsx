import { FixedDetailsView } from '@/components/fixed/FixedDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'

const FixedDetailsPage = ({ fixed, error, session }) => {
  if (error) {
    return (
      <Layout>
        <p>{error}</p>
      </Layout>
    )
  }

  if (!fixed) {
    return (
      <Layout>
        <p>Loading...</p>
      </Layout>
    )
  }

  const { creation_Date, closing_Date, amount, user_Id } = fixed.result;

  // Get the user ID from the session
  const userId = session?.user?.id;

  // Show the deposit details only if the deposit was made by the currently logged-in user
  if (user_Id !== userId) {
    return (
      <Layout>
        <p>Deposit not found for the logged in user</p>
      </Layout>
    )
  }

  return (
    <Layout>
      <FixedDetailsView creationDate={creation_Date} closingDate={closing_Date} amount={amount} />
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  const session = await getSession(context);
  const { id } = context.params;

  try {
    const res = await fetch(`https://localhost:7149/api/FixedDeposit/${id}`, {
      method: 'get',
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
      agent: new https.Agent({
        rejectUnauthorized: false
      })
    });
    const fixed = await res.json();

    return {
      props: {
        fixed,
        session,
      }
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        fixed: null,
        error: `Deposit with id ${id} not found for the logged in user`,
        session,
      }
    };
  }
};

export default FixedDetailsPage;
