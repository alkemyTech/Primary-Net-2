import { Typography, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Grid, IconButton } from '@mui/material';
import { useRouter } from 'next/router'
import EditIcon from '@mui/icons-material/Edit';

export const UserDetailsView = ({ first_Name, last_Name, email, points, rol, money }) => {
    const router = useRouter();
    const{ id } = router.query

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
                        <TableRow>
                            <TableCell component="th" scope="row">Balance</TableCell>
                            <TableCell align="left">$ {money}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>


            <Grid container display={"flex"} justifyContent={"space-between"}>
                <Button
                    variant="contained"
                    onClick={() => router.back()}
                    sx={{ mt: 2 }}
                >
                    <Typography textTransform={"capitalize"}>
                        Volver

                    </Typography>
                </Button>

                <Button onClick={() => router.push(`edituser/${id}`)} sx={{ mt:2,backgroundColor: "#eee", color: "black", ":hover": { color: "white", backgroundColor: "tertiary.main" } }}>
                    <Typography textTransform={"capitalize"}>
                        Edit User
                    </Typography>
                    <EditIcon sx={{ backgroundColor: "tertiary" }} />
                </Button>
            </Grid>



        </Paper>
    );
};
