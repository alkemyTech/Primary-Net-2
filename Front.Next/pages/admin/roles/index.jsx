import DenseTableRoles from "@/components/adminPanel/roles/RolesAdmin";
import { Layout } from "@/layouts/Layout";
import axios from "axios";
import { getSession } from "next-auth/react";
import { useState } from "react";
import { useSession } from "next-auth/react";
import { deleteModal } from "@/components/commons/modal/deleteModal";
import Head from "next/head";

function RolesAdmin({ roles = [] }) {
  const [data, setData] = useState(roles); //asignamos en un estado la data que viene del server
  const { data: session } = useSession();

  const handleDelete = async (rolId) => {
    /* muestra un modal generico para eliminar entidades */
    await deleteModal(
      rolId,
      "Role", //nombre de la entidad a eliminar
      `https://localhost:7149/api/Role/${rolId}`, //ruta a la que vamos a mandar la eliminacion
      session.user?.token,
      () => {
        setData(data.filter((rol) => rol.id !== rolId));
      } //funcion para actualizar el estado
    );
  };

  return (
    <>
      <Head>
        <title>Primates - Admin roles list</title>
      </Head>
      <Layout>
        <DenseTableRoles rows={data} handleDelete={handleDelete} />
      </Layout>
    </>
  );
}

export const getServerSideProps = async (context) => {
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

    const res = await axios.get(`https://localhost:7149/api/Role`, {
      headers: {
        Authorization: `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      },
    });

    return {
      props: {
        roles: res.data,
      },
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        roles: null, // Retorna null si hay algún error
      },
    };
  }
};

export default RolesAdmin;
