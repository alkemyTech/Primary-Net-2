import { modalCallBack } from "../commons/modal/modalCallback";
/*
  configuracion de endpoint y mensajes para el modal
*/
export const pointsToProductModal = async (token, points, callback) => {
  const message =
    "Are you sure you want to redeem your points for this product? This action cannot be undone.";

  const config = {
    apiEndpoint: {
      method:'put',
      url: `https://localhost:7149/api/User/Product`,
      data: { points },
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      }
    },
    confirmButtonText: "Confirm"
  };

  let succesMessage =
    "Congratulations! You have redeemed your points for this product. You will receive shipping information soon.";
  let errorMessage =
    "Sorry, you do not have enough points to redeem for this product. Keep accumulating points to be able to acquire it.";

  /*tomando en cuenta los modales anteriores que recibian una callback se realizo uno mas generico para repetir menos codigo */
  await modalCallBack(message,config,succesMessage,errorMessage,callback);
};
