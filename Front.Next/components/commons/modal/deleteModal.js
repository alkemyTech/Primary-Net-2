import { modalCallBack } from "./modalCallback";

/*
  Para que pueda ser reutilizable el modal de eliminar
  recibe la url de la api y el token,
  entityName: mensajes predeterminados solo cambia el nombre de la entidad
  callback: si la eliminacion por api es exitosa podemos pasar una funcion
            para modificar el estado y no se vea mas el elemento eliminado
*/
export const deleteModal = async (id, entityName, url, token, callback) => {
  const message = `Do you want to delete transaction #${id} ?`;
  const successMessage = `${entityName} Deleted!`;
  const errorMessage = `Error deleting ${entityName}`;

  const config = {
    apiEndpoint: {
      method: "delete",
      url: url,
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    },
    confirmButtonText: "Delete",
  };

  await modalCallBack(message, config, successMessage, errorMessage, callback);
};
