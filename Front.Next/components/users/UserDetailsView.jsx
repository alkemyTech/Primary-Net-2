import { Card, CardContent, Typography } from '@mui/material'
import React from 'react'

export const UserDetailsView = ({ first_Name, last_Name, email, points, rol }) => {
  return (
    <Card sx={{
        width: '90%',
        margin: 'auto',
        mt: 5,
        boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.2)'
    }}>
        <CardContent >
            <Typography 
                variant='h4'
                sx={{
                    fontSize: 38,
                    fontWeight: 'bold',
                    marginBottom: 2
                }}    
            >
                {`${first_Name} ${last_Name}`}
            </Typography>
            <Typography 
                variant='subtitle1'
                sx={{
                    fontSize: 20,
                    fontWeight: 'bold',
                    mb: 1
                }}
            >
                Email:
            </Typography>
            <Typography 
                variant="body1" 
                sx={{ 
                    fontSize: 18,
                    marginBottom: 1 
                    }}
            >
                {email}
            </Typography>
            <Typography 
                variant='subtitle1'
                sx={{
                    fontSize: 20,
                    fontWeight: 'bold',
                    mb: 1
                }}
            >
                Points:
            </Typography>
            <Typography 
                variant="body1" 
                sx={{ 
                    fontSize: 18,
                    marginBottom: 1 
                    }}
            >
                {points}
            </Typography>
            <Typography 
                variant='subtitle1'
                sx={{
                    fontSize: 20,
                    fontWeight: 'bold',
                    mb: 1
                }}
            >
                Rol:
            </Typography>
            <Typography 
                variant="body1" 
                sx={{ 
                    fontSize: 18,
                    marginBottom: 1 
                    }}
            >
                {rol}
            </Typography>
        </CardContent>
    </Card>
  )
}
