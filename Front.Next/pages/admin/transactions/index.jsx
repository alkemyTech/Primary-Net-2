import { Layout } from "@/layouts/Layout";
import React, { useState } from "react";
import useSWR from "swr";
import axios from "axios";
import CustomTable from "@/components/commons/CustomTable";
import CircularLoading from "@/components/commons/CircularLoading";
import { getSession } from "next-auth/react";
import { useSession } from "next-auth/react";
import { deleteModal } from "@/components/commons/modal/deleteModal";
import Head from "next/head";

const fetcher = (token) => async (url) => {
  const headers = {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  };
  const { data } = await axios.get(url, { headers });
  return data;
};

export default function TransactionsAdmin({ transactions }) {
  const [page, setPage] = useState(1);
  const { data: session } = useSession();

  const { data, isLoading, isError, mutate } = useSWR(
    `https://localhost:7149/api/Transactions/All?page=${page}`,
    fetcher(session?.user?.token),
    { initialData: transactions }
  );

  const handlePrevPage = () => {
    if (page > 1) {
      setPage((prevPage) => prevPage - 1);
    }
  };

  const handleNextPage = () => {
    if (data.nextPage != "None") {
      setPage((prevPage) => prevPage + 1);
    }
  };

  const handleDelete = async (transactionId) => {
    //podemos usar la funcion reutilizable del ticket anterior
    await deleteModal(
      transactionId,
      "Transaction",
      `https://localhost:7149/api/Transactions/${transactionId}`,
      session.user?.token,
      /* deleteModal puede recibir cualquier funcion que se ejecute luego del llamado a la api
            en este caso como usamos swr, podemos directamente actualizar el cache sin llamar nuevamente a la api y como la callback se ejecuta solo si la eliminacion es exitosa
            nos aseguramos que el usuario deja de ver la transaccion que efectivamente fue eliminada en la base de datos
      */
      () => {
        mutate();
      }
    );
  };

  if (!data || isLoading) {
    return <CircularLoading />;
  }

  if (isError) {
    return <div>Error loading users</div>;
  }
  return (
    <>
      <Head>
        <title>Primates - Admin transactions list</title>
      </Head>
      <Layout>
        {/*usamos el componente reusable de tabla del ticket anterior, el cual
         con useSession verifica si es admin y si lo es renderiza los botones de edicion y eliminacion */}
        <CustomTable
          rows={data.result}
          columnLabels={[
            "Id",
            "Amount",
            "Concept",
            "Date",
            "Type",
            "Sender",
            "Receiver",
          ]}
          dataProperties={[
            "id",
            "amount",
            "concept",
            "date",
            "type",
            "account_Id",
            "to_Account_Id",
          ]}
          handlePrevPage={handlePrevPage}
          handleNextPage={handleNextPage}
          routeBase={"/admin/transactions"}
          handleDelete={handleDelete}
          newButton={true}
        />
      </Layout>
    </>
  );
}

export async function getServerSideProps(context) {
  //del server buscamos la primera pagina de todas las transacciones
  try {
    const session = await getSession(context);

    const now = Math.floor(Date.now() / 1000);

    if (session == null || session.expires < now) {
      return {
        redirect: {
          destination: "/login",
          permanent: false,
        },
      };
    }

    if (session.user.rol != "Admin") {
      return {
        redirect: {
          destination: "/?invalidcredentials=true",
          permanent: false,
        },
      };
    }
    const url = "https://localhost:7149/api/Transactions/All?page=1";

    const response = await axios.get(url, {
      headers: {
        Authorization: `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
    });

    const users = response.data;

    return {
      props: {
        users,
      },
    };
  } catch (error) {
    return {
      props: {
        data: null,
      },
    };
  }
}
