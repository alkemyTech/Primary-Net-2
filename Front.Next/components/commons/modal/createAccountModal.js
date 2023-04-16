import Swal from "sweetalert2";
import axios from "axios";

const showConfirmationDialog = async (message) => {
  const result = await Swal.fire({
    title: message,
    showDenyButton: false,
    showCancelButton: true,
    confirmButtonText: "Confirm",
  });
  return result.isConfirmed;
};

export const createAccountModal = async (token) => {
  const confirmed = await showConfirmationDialog("Please confirm that you want to create your account");

  if (confirmed) {
    try {

      const response = await axios.post( "https://localhost:7149/api/Account/Create",{}, {
        headers: {
            'Authorization': `Bearer ${token}`,
            "Content-Type": "application/json"
        }
    })
      Swal.fire("Account created successfully!", "", "success");
    } catch (error) {
      console.error(error);
      Swal.fire("Account already exists.", "", "error");
    }
  }
};
