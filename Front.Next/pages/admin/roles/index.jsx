import DenseTableRoles from '@/components/adminPanel/roles/RolesAdmin';
import { Layout } from '@/layouts/Layout';
import axios from 'axios';
import { getSession } from 'next-auth/react'


function RolesAdmin({ roles = [] }) {
    return (
        <Layout>
            <DenseTableRoles rows={roles} /> 
        </Layout>
    )
}

export const getServerSideProps = async (context) => {
    try {

        const session = await getSession(context);

        const res = await axios.get(`https://localhost:7149/api/Role`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });

        return {
            props: {
                roles: res.data
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                roles: null // Retorna null si hay algún error
            }
        };
    }
};

export default RolesAdmin
