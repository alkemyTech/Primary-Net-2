import { UserDetailsView } from '@/components/users/UserDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'


const userDetailsPage = ({ user }) => {
  return (
    <Layout>
        <UserDetailsView {...user} />
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
    try {

      const session = await getSession(context);
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
    
    const  { result }  = await res.json();
    console.log(result)
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