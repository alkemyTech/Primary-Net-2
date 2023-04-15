import { Card, CardContent, Grid, Typography, Button } from '@mui/material';
import React from 'react';
import { useSession } from 'next-auth/react';
import { useRouter } from 'next/router';

export const FixedDetailsView = ({ creationDate, closingDate, amount }) => {
  const { data: session } = useSession();
  const router = useRouter();

  // Convierte las fechas a objetos Date
  const creationDateObj = new Date(creationDate);
  const closingDateObj = new Date(closingDate);

  // Crea las cadenas de fecha en el formato deseado
  const creationDateStr = creationDateObj.toLocaleDateString('es-ES');
  const closingDateStr = closingDateObj.toLocaleDateString('es-ES');

  // Crea las cadenas de hora en el formato deseado
  const creationTimeStr = creationDateObj.toLocaleTimeString('en-US');
  const closingTimeStr = closingDateObj.toLocaleTimeString('en-US');

  if (!session) {
    return (
      <Typography variant="h6">Please sign in to view this deposit</Typography>
    );
  }

  return (
    <Card
      sx={{
        width: '90%',
        margin: 'auto',
        mt: 3,
        boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.2)',
        borderRadius: '10px',
        border: '1px solid black',
        padding: '10px',
        maxWidth: 600,
      }}
    >
      <CardContent>
        <Typography
          variant="h4"
          sx={{
            fontSize: 28, 
            fontWeight: 'bold',
            mb: 2,
            borderBottom: '1px solid black',
            paddingBottom: '10px',
            textAlign: 'center', 
          }}
        >
          Fixed Term Deposit Details
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Typography
              variant="subtitle1"
              sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }} 
            >
              Creation Date:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}> 
              {creationDateStr} at {creationTimeStr}
            </Typography>
            <Typography
              variant="subtitle1"
              sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }} 
            >
              Closing Date:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}> 
              {closingDateStr} at {closingTimeStr}
            </Typography>
            <Typography
              variant="subtitle1"
              sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}
            >
              Amount:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}> 
              {amount}
            </Typography>
            <Typography
              variant="subtitle1"
              sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }} 
            >
              User:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}> 
              {session.user.first_Name} {session.user.last_Name}
            </Typography>
          </Grid>
        </Grid>
        <Button
          variant="contained"
          sx={{ mt: 2, ml: 0, width: 'auto', alignSelf: 'flex-start', fontSize: 18 }} 
          onClick={() => router.back()}
        >
          Go back
        </Button>
      </CardContent>
    </Card>
  );
};
