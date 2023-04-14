<<<<<<< HEAD
import { Typography, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button } from '@mui/material';

import { useRouter } from 'next/router'
=======
import { Button, Card, CardContent, Grid, IconButton, Typography } from '@mui/material'
import { useSession } from 'next-auth/react'
>>>>>>> develop
import React from 'react'
import EditIcon from '@mui/icons-material/Edit';
import { useRouter } from 'next/router';

<<<<<<< HEAD
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
=======
export const UserDetailsView = ({ first_Name, last_Name, email, points, rol, money }) => {

    const { data: session } = useSession();
    const router = useRouter();
    const { id } = router.query;
    const sessionId = session?.user?.userId || "";

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

                <Typography variant='subtitle1'
                    sx={{
                        fontSize: 20,
                        fontWeight: 'bold',
                        mb: 1
                    }}>
                    Balance: 
                </Typography>
                <Typography
                    variant="body1"
                    sx={{
                        fontSize: 18,
                        marginBottom: 1
                    }}
                >
                    ${money}
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
                {
                    !sessionId || sessionId != id ?
                        null
                        :
                        <Grid container display={"flex"} justifyContent={"flex-end"}>
                            <Button onClick={()=>router.push(`edituser/${id}`)} sx={{ backgroundColor: "#eee", color: "black", ":hover": { color: "white", backgroundColor: "tertiary.main" } }}>
                                <Typography textTransform={"capitalize"}>
                                    Edit User
                                </Typography>
                                <IconButton>
                                    <EditIcon sx={{ backgroundColor: "tertiary" }} />
                                </IconButton>
                            </Button>
                        </Grid>
                }

            </CardContent>

        </Card>
    )
}
>>>>>>> develop
