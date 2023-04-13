import Swal from "sweetalert2";
import axios from "axios";

/*mensaje de confirmacion de sweetAlert */
const showConfirmationDialog = async (message) => {
  const result = await Swal.fire({
    title: message,
    showDenyButton: false,
    showCancelButton: true,
    confirmButtonText: "Delete",
  });
  return result.isConfirmed;
};

/*
  Para que pueda ser reutilizable el modal de eliminar
  message: mensage del modal de confirmacion
  config: recibe la url de la api y opcionalmente un header para el token
  entityName: mensajes predeterminados solo cambia el nombre de la entidad
  callback: si la eliminacion por api es exitosa podemos pasar una funcion
            para modificar el estado y no se vea mas el elemento eliminado
*/
export const deleteModal = async (message, config, entityName, callback) => {
  const confirmed = await showConfirmationDialog(message);

  if (confirmed) {
    try {
      await axios.delete(config.url, {
        headers: config.headers || {},
      });
      Swal.fire(`${entityName} Deleted!`, "", "success");
      callback();
    } catch (error) {
      console.error(error);
      Swal.fire(`Error deleting ${entityName}`, "", "error");
    }
  } else {
    Swal.fire("Changes are not saved", "", "info");
  }
};
