import DenseTableRoles from "@/components/adminPanel/roles/RolesAdmin";
import { Layout } from "@/layouts/Layout";
import axios from "axios";
import { getSession } from "next-auth/react";
import { useState } from "react";
import { useSession } from "next-auth/react";

function RolesAdmin({ roles = [] }) {
  const [data, setData] = useState(roles); //asignamos en un estado la data que viene del server
  const { data: session } = useSession();

  const handleDelete = async (rolId) => {
    /* muestra un modal generico para eliminar entidades */
    await deleteModal(
      `Do you want to delete role #${rolId} ?`, //mensaje confirmacion
      {
        url: `https://localhost:7149/api/Role/${rolId}`,
        headers: {
          Authorization: `Bearer ${session.user?.token}`,
          "Content-Type": "application/json",
        }, //ruta a la que vamos a mandar la eliminacion
      },
      "Role", //nombre de la entidad a eliminar
      () => {
        setData(data.filter((rol) => rol.id !== rolId));
      } //funcion para actualizar el estado
    );
  };

  return (
    <Layout>
      <DenseTableRoles rows={roles} handleDelete={handleDelete} />
    </Layout>
  );
}

export const getServerSideProps = async (context) => {
  try {
    const session = await getSession(context);

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
