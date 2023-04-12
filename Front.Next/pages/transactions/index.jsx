import React, { useState } from "react";
import https from "https";
import axios from "axios";
import CircularProgress from "@mui/material/CircularProgress";
import CustomTable from "@/components/commons/CustomTable";
import { Layout } from "@/layouts/Layout";
import useSWR from "swr";

const fetcher = async (url) => {
  const response = await axios.get(url);
  return response.data;
};

/*la primera pagina se carga en el server y las siguientes por medio de swr */
export default function Transactions({ transactions }) {
  const [page, setPage] = useState(1);
  const { data, isLoading, isError } = useSWR(
    `https://localhost:7149/api/Transactions?page=${page}`,
    fetcher,
    { initialData: transactions }
  );

  console.log(data)
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

  if (isLoading) {
    return <CircularProgress />;
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


export async function getServerSideProps() {
  const url = "https://localhost:7149/api/Transactions?page=1";
  const agent = new https.Agent({
    rejectUnauthorized: false,
  });

  const response = await axios.get(url, { httpsAgent: agent });
  const transactions = response.data;
  console.log(response)

  return {
    props: {
      transactions,
    },
  };
}
