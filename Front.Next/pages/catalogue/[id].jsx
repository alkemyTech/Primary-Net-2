import { CatalogueDetailsView } from '@/components/cartalogue/CatalogueDetailsView'
import { Layout } from '@/layouts/Layout'
import https from 'https'
import { getSession } from 'next-auth/react'


const CatalogueDetailsPage = ({ catalogue }) => {
  return (
    <Layout>
      {catalogue && <CatalogueDetailsView {...catalogue} />}
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  try {
    const session = await getSession(context);
    const { id } = context.params;
    const res = await fetch(`https://localhost:7149/api/Catalogue/${id}`, {
      method: 'get',
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
      agent: new https.Agent({
        rejectUnauthorized: false
      })
    });
    const catalogue = await res.json();
    console.log(catalogue)
    return {
      props: {
        catalogue,
      }
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        catalogue: null
      }
    };
  }
};

export default CatalogueDetailsPage;