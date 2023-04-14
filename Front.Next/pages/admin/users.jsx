import { Layout } from "@/layouts/Layout";
import React, { useState } from "react";
import useSWR from "swr";
import axios from "axios";
import CustomTable from "@/components/commons/CustomTable";
import CircularLoading from "@/components/commons/CircularLoading";
import { getSession } from "next-auth/react";
import { useSession } from "next-auth/react";

const fetcher = (token) => async (url) => {
  const headers = {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
  };
  const { data } = await axios.get(url, { headers });
  return data;
};

export default function Users({ users }) {
  const [page, setPage] = useState(1);
  const { data: session } = useSession();
  /*
    El componente al iniciar trae la primera pagina con getServerSideProps, la cual ingresa al componente
    desde el parametro como users, esta primera pagina se setea como data inicial de SWR para que no vuelva a cargar la pagina 1,
    luego si maneja las solicitudes de la siguientes paginas
  */
  const { data, isLoading, isError } = useSWR(
    `https://localhost:7149/api/User/All?page=${page}`,
    fetcher(session?.user?.token),
    { initialData: users }
  );

  const handlePrevPage = () => {
    if (page > 1) { //solo se puede avanzar a la siguente pagina si la actual es mayor a 1
      setPage((prevPage) => prevPage - 1);
    }
  };

  const handleNextPage = () => {
    if (data.nextPage != "None") { //nextPage es una propiedad que devuelve la api si existe pagina siguiente
      setPage((prevPage) => prevPage + 1);
    }
  };

  const handleDelete = () => {};

  if (!data || isLoading) {
    return <CircularLoading />;
  }


  if (isError) {
    return <div>Error loading users</div>;
  }
  return (
    <>
      <Layout>
        {/*Componente reutilizable hay que asignarle las columnas y la data que se quiera mostrar
         por ejemplo esta entidad contiene el campo password el cual no vamos a mostrar  */}
        <CustomTable
          rows={data.result} //contenido a visualizar, en este caso es un arreglo de usuarios
          columnLabels={["Id", "First Name", "Last Name", "Email", "Rol"]} //columnas
          dataProperties={[
            "userId",
            "first_Name",
            "last_Name",
            "email",
            "points",
            "rol",
          ]} //data de usuario que se quiere mostrar
          handlePrevPage={handlePrevPage}
          handleNextPage={handleNextPage}
          routeBase={"/users"} //el componente tiene rutas predeterminadas como /users/:id o /users/new
          handleDelete={handleDelete}
        />
      </Layout>
    </>
  );
}

export async function getServerSideProps(context) {
  /*Cargamos la primera pagina en el server */
  const session = await getSession(context);
  const url = "https://localhost:7149/api/User/All?page=1";

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
}

