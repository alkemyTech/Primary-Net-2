import { Layout } from '@/layouts/Layout'
import React from 'react'
import CatalogueTableAdmin from "../../../components/adminPanel/catalogue/CatalogueTableAdmin"
import { getSession } from 'next-auth/react';
import axios from 'axios';


const CatalogueAdmin = ({catalogue = []}) => {
    const products = catalogue?.result

  return (
    <Layout>
       <CatalogueTableAdmin rows={products}/>
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
    try {

        const session = await getSession(context);

        const res = await axios.get(`https://localhost:7149/api/Catalogue?page=1&pageSize=100`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });
        return {
            props: {
                catalogue: res.data
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                catalogue: null // Retorna null si hay algún error
            }
        };
    }
};

export default CatalogueAdmin