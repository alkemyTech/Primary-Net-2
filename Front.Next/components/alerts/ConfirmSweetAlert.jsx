import Swal from 'sweetalert2';

export const ConfirmSweetAlert = ( {title, text, confirmButtonText, cancelButtonText, onConfirm, onCancel, onClose }) => {
    Swal.fire({
      title: title,
      text: text,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: confirmButtonText,
      cancelButtonText: cancelButtonText
    }).then((result) => {
      if (result.isConfirmed) {
        if (onConfirm) {
          onConfirm();
        }
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        if (onCancel) {
          onCancel();
        }
      }
    }).then(() => {
      onClose && onClose();
    });
  }
