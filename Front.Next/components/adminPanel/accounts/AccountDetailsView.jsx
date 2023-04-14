import { Card, CardContent, Grid, Typography } from '@mui/material'
import React from 'react'

export const AccountDetailsView = ({ id, creationDate, money, isBlocked, userId, user, isDeleted, transactions, fixedTermDeposit }) => {
  return (
    <Card sx={{ width: '90%', margin: 'auto', mt: 3, boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.2)' }}>
      <CardContent>
        <Typography variant="h4" sx={{ fontSize: 38, fontWeight: 'bold', mb: 2 }}>
          Account details
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={6}>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Account ID:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {id || 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Creation date:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {creationDate ? new Date(creationDate).toLocaleDateString('en-US') : 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Money:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {money || 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Is blocked:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {isBlocked ? 'true' : 'false'}
            </Typography>
          </Grid>
          <Grid item xs={6}>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              User ID:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {userId || 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              User:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {user || 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Is deleted:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {isDeleted ? 'true' : 'false'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}>
              Transactions:
            </Typography>
            <Typography variant="body1" sx={{ fontSize: 18, marginBottom: 1 }}>
              {transactions || 'null'}
            </Typography>
            <Typography variant="subtitle1" sx={{ fontSize: 20, fontWeight: 'bold', mb: 1 }}></Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  )}
  
  
  
