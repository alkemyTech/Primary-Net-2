import React, { useEffect } from 'react';
import Swal from 'sweetalert2';

export const SweetAlert = ({ title, text, icon, timer, onClose }) => {
    useEffect(() => {
      const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
          confirmButton: 'btn btn-success',
          cancelButton: 'btn btn-danger',
        },
        buttonsStyling: false,
      });
  
      swalWithBootstrapButtons
        .fire({
          title,
          text,
          icon,
          timer,
          showConfirmButton: false,
        })
        .then(() => {
          onClose && onClose();
        });
  
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
  
    return null;
  };