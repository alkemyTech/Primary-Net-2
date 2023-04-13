import React, { useState } from "react";
import axios from "axios";
import CustomTable from "@/components/commons/CustomTable";
import { Layout } from "@/layouts/Layout";
import useSWR from "swr";
import { useSession } from "next-auth/react";
import { getSession } from "next-auth/react";
import CircularLoading from "@/components/commons/CircularLoading";

const fetcher = (token) => async (url) => {
  const headers = {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  };
  const { data } = await axios.get(url, { headers });
  return data;
};

/*la primera pagina se carga en el server y las siguientes por medio de swr */
export default function Transactions({ transactions }) {
  const [page, setPage] = useState(1);

  const { data: session } = useSession();

  const { data, isLoading, isError } = useSWR(
    `https://localhost:7149/api/Transactions?page=${page}`,
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

  if (!data || isLoading) {
    return <CircularLoading />;
  }

  if (isError) {
    return <div>Error loading users</div>;
  }

  return (
    <>
      <Layout>
        {/*se modifico el componente para que se pueda usar la tabla tanto en vista admin como user normal */}
        <CustomTable
          rows={data.result}
          columnLabels={["Id", "Amount", "Concept", "Date", "Type", "Receiver"]}
          dataProperties={[
            "id",
            "amount",
            "concept",
            "date",
            "type",
            "to_Account_Id",
          ]}
          handlePrevPage={handlePrevPage}
          handleNextPage={handleNextPage}
          routeBase={"/transactions"}
        />
      </Layout>
    </>
  );
}

export async function getServerSideProps(context) {
  const session = await getSession(context);

  const url = "https://localhost:7149/api/Transactions?page=1";

  const response = await axios.get(url, {
    headers: {
      Authorization: `Bearer ${session.user?.token}`,
      "Content-Type": "application/json",
    },
  });

  const transactions = response.data;

  return {
    props: {
      transactions,
    },
  };
}
