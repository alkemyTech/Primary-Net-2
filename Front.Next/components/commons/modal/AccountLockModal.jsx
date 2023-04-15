import React from "react";
import { Modal, Box, Button } from "@mui/material";

function AccountLockModal({ open, handleClose, handleConfirm }) {
  const handleCancel = () => {
    handleClose();
  };

  return (
    <>
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="lock-account-modal-title"
      aria-describedby="lock-account-modal-description"
    >
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 400,
          bgcolor: 'background.paper',
          borderRadius: '5px',
          boxShadow: '2px 2px 5px rgba(0, 0, 0, 0.25)',
          p: 3,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          gap: '20px',
        }}
      >
        <h2
          id="lock-account-modal-title"
          sx={{
            fontFamily: 'Montserrat',
            fontSize: '24px',
            fontWeight: 600,
            margin: 0,
            textAlign: 'center',
          }}
        >
          Are you sure you want to lock this account?
        </h2>
        <div
          id="lock-account-modal-description"
          sx={{
            display: 'flex',
            flexDirection: 'row',
            justifyContent: 'space-between',
            gap: '10px'
          }}
        >
          <Button
            variant="contained"
            sx={{
              backgroundColor: '#2196f3',
              color: 'white',
              '&:hover': {
                backgroundColor: '#1976d2',
              },
            }}
            onClick={handleConfirm}
          >
            Confirm
          </Button>
          <Button
            variant="contained"
            color="error"
            sx={{
              '&:hover': {
                backgroundColor: '#d32f2f',
              },
            }}
            onClick={handleCancel}
          >
            Cancel
          </Button>
        </div>
      </Box>
    </Modal>
    </>
  );
}

export default AccountLockModal;
