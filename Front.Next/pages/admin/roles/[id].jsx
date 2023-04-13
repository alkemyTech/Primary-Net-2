import { RoleDetailsView } from '@/components/adminPanel/roles/RoleDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'


const RoleDetailsPage = ({ role }) => {
  return (
    <Layout>
      {role && <RoleDetailsView {...role} />}
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  try {
    const session = await getSession(context);
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
    console.log(role)
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