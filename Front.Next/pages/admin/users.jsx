import { Layout } from "@/layouts/Layout";
import React, { useState } from "react";
import useSWR, { mutate } from "swr";
import axios from "axios";
import CustomTable from "@/components/commons/CustomTable";
import CircularLoading from "@/components/commons/CircularLoading";
import { getSession } from "next-auth/react";
import { useSession } from "next-auth/react";
import Head from "next/head";
import { deleteModal } from "@/components/commons/modal/deleteModal";

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
    if (page > 1) {
      //solo se puede avanzar a la siguente pagina si la actual es mayor a 1
      setPage((prevPage) => prevPage - 1);
    }
  };

  const handleNextPage = () => {
    if (data.nextPage != "None") {
      //nextPage es una propiedad que devuelve la api si existe pagina siguiente
      setPage((prevPage) => prevPage + 1);
    }
  };

  const handleDelete = async (userId) => {
    await deleteModal(
      userId,
      "User",
      `https://localhost:7149/api/User/${userId}`,
      session.user?.token,
      () => { //probar solo mutate()
        // // Obtener una nueva lista de usuarios que excluya al usuario eliminado
        const updatedUsers = data.result.filter(
          (user) => user.userId !== userId
        );
        // // Actualizar el caché de SWR con la nueva lista de usuarios
        mutate(
          `https://localhost:7149/api/User/All?page=${page}`,
          { ...data, result: updatedUsers },
          false
        );
      }
    );
  };

  if (!data || isLoading) {
    return <CircularLoading />;
  }

  if (isError) {
    return <div>Error loading users</div>;
  }

  //para poder usar bien customTable, user es la unica entidad que el id se llama distinto
  const formattedUsers = data.result.map(({ userId: id, ...rest }) => ({ id, ...rest }));
  
  return (
    <>
      <Layout>
        {/*Componente reutilizable hay que asignarle las columnas y la data que se quiera mostrar
         por ejemplo esta entidad contiene el campo password el cual no vamos a mostrar  */}
        <CustomTable
          rows={data.result} //contenido a visualizar, en este caso es un arreglo de usuarios
          columnLabels={[
            "Id",
            "First Name",
            "Last Name",
            "Email",
            "Points",
            "Rol",
          ]} //columnas
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
  } catch (error) {
    return {
      props: {
        data: null,
      },
    };
  }
}
