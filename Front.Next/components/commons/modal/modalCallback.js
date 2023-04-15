import Swal from "sweetalert2";
import axios from "axios";

const showConfirmationDialog = async (message, confirmButtonText) => {
  const result = await Swal.fire({
    title: message,
    showDenyButton: false,
    showCancelButton: true,
    confirmButtonText,
  });
  return result.isConfirmed;
};

/*
  abstraccion para las funciones de modales anteriores, aca se usara axios y sweetAlert, en las otras funciones solo la configuracion
*/
export const modalCallBack = async (
  message,
  config,
  successMessage,
  errorMessage,
  callback
) => {
  const confirmed = await showConfirmationDialog(
    message,
    config.confirmButtonText
  );

  if (confirmed) {
    try {
      await axios(config.apiEndpoint);
      Swal.fire(successMessage, "", "success");
      callback();
    } catch (error) {
      console.error(error);
      Swal.fire(errorMessage, "", "error");
    }
  } else {
    Swal.fire("Changes are not saved", "", "info");
  }
};
