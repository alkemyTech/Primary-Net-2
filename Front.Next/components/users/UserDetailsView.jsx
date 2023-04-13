import { Typography, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button } from '@mui/material';

import { useRouter } from 'next/router'
import React from 'react'

export const UserDetailsView = ({ first_Name, last_Name, email, points, rol }) => {
  const router = useRouter();
  
  return (
    <Paper elevation={3} style={{ padding: "20px", margin: "20px" }}>
      <Typography variant="h4" component="h1" gutterBottom>
        {`${first_Name} ${last_Name}`}
      </Typography>
      <TableContainer component={Paper}>
        <Table aria-label="customer details">
          <TableBody>
            <TableRow>
              <TableCell component="th" scope="row">Email</TableCell>
              <TableCell align="left">{email}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell component="th" scope="row">Points</TableCell>
              <TableCell align="left">{points}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell component="th" scope="row">Role</TableCell>
              <TableCell align="left">{rol}</TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </TableContainer>
      <Button
        variant="contained"
        onClick={() => router.back()}
        sx={{ mt: 2 }}
      >
        Volver
      </Button>
    </Paper>
  );
};
