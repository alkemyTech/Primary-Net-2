import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Button, Grid, TextField, Typography } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import FmdGoodIcon from '@mui/icons-material/FmdGood';
import Link from 'next/link';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { getSession } from 'next-auth/react';
import axios from 'axios';
import Swal from 'sweetalert2';
import { useRouter } from 'next/router';

export default function DenseTableCatalogue({ rows = [] }) {

    const router = useRouter();

    const handleDeleteProduct = async (productId) => {
        const session = await getSession();

        Swal.fire({
            title: `Do you want to delete product #${productId} ?`,
            showDenyButton: false,
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                axios.delete(` https://localhost:7149/api/Catalogue/${productId}`, {
                    headers: {
                        'Authorization': `Bearer ${session.user?.token}`,
                        "Content-Type": "application/json",
                    }
                }
                )
                .then(Swal.fire('Product Deleted!', '', 'success'))
                .then(setTimeout(() => { router.reload() }, 3000))
            } else {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    }


    return (

        <Grid container>

            <Typography variant='h4' sx={{ pb: 2 }}>
                Catalogue
            </Typography>

            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontWeight: "bold" }} align="center">Id</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Name </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Points</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Detail</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Edit</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Delete</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (
                            <TableRow
                                key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >

                                <TableCell align="center">{row.id}</TableCell>
                                <TableCell align="center">{row.productDescription}</TableCell>
                                <TableCell align="center">{row.points}</TableCell>
                                <TableCell align="center"><Link style={{ textDecoration: "none", color: "#000" }} href={`/catalogue/${row.id}`}> <FmdGoodIcon /> </Link> </TableCell>
                                <TableCell align="center"> <EditIcon /> </TableCell>
                                <TableCell align="center">  <Button onClick={() => handleDeleteProduct(row.id)}> <DeleteForeverIcon />  </Button> </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
                <Button color={"tertiary"} onClick={() => router.push('admin/catalogues/new')}>
                    <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
                </Button>
            </Grid>
        </Grid>

    );
}

