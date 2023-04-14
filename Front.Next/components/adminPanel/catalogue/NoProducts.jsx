import React from 'react';
import Alert from '@mui/material/Alert';
import AlertTitle from '@mui/material/AlertTitle';
import Stack from '@mui/material/Stack';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

export const PointsAlert = ({ pointsNeeded }) => {
  return (
    <Stack sx={{ width: '100%' }} spacing={2}>
      <Alert
        severity="warning"
        action={
          <IconButton
            aria-label="close"
            color="inherit"
            size="small"
          >
            <CloseIcon fontSize="inherit" />
          </IconButton>
        }
      >
        <AlertTitle>Not enough points</AlertTitle>
        You need {pointsNeeded} more points to perform this action.
      </Alert>
    </Stack>
  );
};