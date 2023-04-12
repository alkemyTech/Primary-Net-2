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
import { Button, Grid, TextField } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import FmdGoodIcon from '@mui/icons-material/FmdGood';
import Link from 'next/link';
import axios from 'axios';
import Swal from 'sweetalert2'
import { useRouter } from 'next/router';


export default function DenseTableAdmin({ rows }) {



    let { result = [], page, nextPage, previousPage } = rows;

    const router = useRouter();

    const nextPageRequest = async () => {
        if (typeof(nextPage=="undefined") || nextPage == "None") return result
        const { data } = await axios.get(`https://localhost:7149/api/Account?page={${page + 1}}&pageSize=10`)
        result = data.result;
    }

    const prevPageRequest = async () => {
        if (typeof(previousPage == "undefined") || previousPage == "None") return result
        const { data } = await axios.get(`https://localhost:7149/api/Account?page={${page - 1}}&pageSize=10`)
        result = data.result;
    }


            const handleDeleteAccount =async(accountId)=> {
                Swal.fire({
                    title:  `Do you want to delete account #${accountId} ?`,
                    showDenyButton: false,
                    showCancelButton: true,
                    confirmButtonText: 'Delete',
                  }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        axios.delete(` https://localhost:7149/api/Account/${accountId}`)
                        .then( Swal.fire('Account Deleted!', '', 'success'))
                        .then( router.reload(window.location.pathname))
                    } else if (result.isDenied) {
                      Swal.fire('Changes are not saved', '', 'info')
                    }
                  })
            }


    return (

        <Grid container >



            <Grid container display={"flex"} position={"fixed"} justifyContent={"center"} alignItems={"flex-end"} spacing={2} gap={1} bottom={1}>
                <Button variant='contained' disabled={previousPage == "None" || typeof(previousPage == "undefined")} onClick={prevPageRequest} sx={{ height: "75%", width: "8%" }}>
                    <ArrowBackIcon />
                </Button>
                <Button variant='contained'  disabled={nextPage == "None" || typeof(nextPage=="undefined")} onClick={nextPageRequest} sx={{ height: "75%", width: "8%" }}>
                    <ArrowForwardIcon />
                </Button>
            </Grid>




            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontWeight: "bold" }} align="center">Id</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Owner </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Money </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Deposits</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Transactions</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Detail</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Edit</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Block / Activate </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Delete</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {result.map((row) => (
                            <TableRow
                                key={row.name}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >

                                <TableCell align="center">{row.id}</TableCell>
                                <TableCell align="center"> {`${row.name} ${row.lastName}`}</TableCell>
                                <TableCell align="center"> $ {row.money}</TableCell>
                                <TableCell align="center">
                                    <Link style={{ textDecoration: "none", color: "#000" }} href={`/fixedDeposits/${row.id}`}> See user Deposits </Link> 
                                </TableCell>
                                <TableCell align="center">
                                    <Link style={{ textDecoration: "none", color: "#000" }} href={`/transactions/${row.id}`}> See user Transactions </Link> 
                                </TableCell>
                                <TableCell align="center">
                                    <Link style={{ textDecoration: "none", color: "#000" }} href={`/account/${row.id}`}> <FmdGoodIcon /> </Link>
                                </TableCell>
                                <TableCell align="center"> <EditIcon /> </TableCell>
                                <TableCell align="center">{row.isBlocked === true ? <Button variant='outlined' sx={{backgroundColor:"green", color:"white"}}> Activate </Button> : <Button variant='outlined' sx={{backgroundColor:"red", color:"gray"}}> Block </Button>}</TableCell>
                                <TableCell align="center"> <Button onClick={()=>handleDeleteAccount(row.id)}> <DeleteForeverIcon /> </Button>  </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Grid>

    );
}