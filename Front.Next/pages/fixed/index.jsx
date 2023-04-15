
import { UserFixedDeposits } from '@/components/users/UserFixedDeposits';
import { Layout } from '@/layouts/Layout';
import axios from 'axios';
import { getSession } from 'next-auth/react'
import Head from 'next/head';

const UserFixedTermDeposits = ({ fixed = [] }) => {

    return (
        <>
            <Head>
                <title>
                    Primates - Fixed term deposits list
                </title>
            </Head>
            <Layout>
                <UserFixedDeposits fixed={fixed} />
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