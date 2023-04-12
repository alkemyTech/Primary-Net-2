
import { UserFixedDeposits } from '@/components/users/UserFixedDeposits';
import { Layout } from '@/layouts/Layout';
import axios from 'axios';
import { getSession } from 'next-auth/react'

const UserFixedTermDeposits = ({fixed = []}) => {

    return (
    <Layout>
        <UserFixedDeposits fixed={fixed}/>
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
    try {

        const session = await getSession(context);

        const res = await axios.get(`https://localhost:7149/api/FixedDeposit/UserDeposits`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });


        return {
            props: {
                fixed: res.data.result
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                fixed: null // Retorna null si hay algún error
            }
        };
    }
};

export default UserFixedTermDeposits