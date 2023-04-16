import { CatalogueDetailsView } from "@/components/cartalogue/CatalogueDetailsView";
import { Layout } from "@/layouts/Layout";
import https from "https";
import { getSession, useSession } from "next-auth/react";
import { useRouter } from "next/router";
import { pointsToProductModal } from "@/components/cartalogue/pointsToProductModal";
import axios from "axios";
import Head from "next/head";

const CatalogueDetailsPage = ({ catalogue }) => {
  const router = useRouter();
  const { data: session } = useSession();

  const handleGetProduct = async (points) => {
    /*funcion que crea un modal y habilita token y body para axios */
    await pointsToProductModal(session.user?.token, points, async () => {
      /*callback que solo se ejecuta si lo anterior es exitoso,
       en este caso restarle puntos al usuario igual a los puntos del producto*/
      await deleteProduct(catalogue.id);
      router.back();
    });
  };

  const deleteProduct = async (productId) => {
    await axios.delete(`https://localhost:7149/api/Catalogue/${productId}`, {
      headers: {
        Authorization: `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
    });
  };

  return (
    <>
    <Head>
      <title>
        Primates - Product details
      </title>
    </Head>
      <Layout>
        {catalogue && (
          <CatalogueDetailsView
            {...catalogue}
            handleGetProduct={handleGetProduct}
          />
        )}
      </Layout>
    </>
  );
};

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
    const { id } = context.params;
    const res = await fetch(`https://localhost:7149/api/Catalogue/${id}`, {
      method: "get",
      headers: {
        Authorization: `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
      agent: new https.Agent({
        rejectUnauthorized: false,
      }),
    });
    const catalogue = await res.json();
    return {
      props: {
        catalogue,
      },
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        catalogue: null,
      },
    };
  }
};

export default CatalogueDetailsPage;
