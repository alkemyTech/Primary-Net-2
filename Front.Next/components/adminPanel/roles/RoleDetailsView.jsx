import { Card, CardContent, Grid, Typography } from '@mui/material'
import React from 'react'

export const RoleDetailsView = ({ id, name, description, isDeleted }) => {
  return (
    <Card sx={{ width: '90%', margin: 'auto', mt: 3, boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.2)' }}>
      <CardContent>
        <Typography variant="h4" sx={{ fontSize: 38, fontWeight: 'bold', mb: 2 }}>
          Rol details
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={6}>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Name:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {name}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Description:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {description}
            </Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Rol id:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {id}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              IsDeleted:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {isDeleted ? 'true' : 'false'}
            </Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  )
}