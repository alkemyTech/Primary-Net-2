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
import axios from 'axios';
import Swal from 'sweetalert2'
import { useRouter } from 'next/router';


export default function DenseTableAdmin({ rows }) {


    return (

        <Grid container >
        <Typography variant='h4' sx={{pb:2}}>
                Accounts
            </Typography>

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
                        {rows.result.map((row) => (
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
                                <TableCell align="center">{row.isBlocked === true ? <Button variant='outlined' sx={{backgroundColor:"green", color:"white"}}> Activate </Button> : <Button variant='outlined' sx={{backgroundColor:"red", color:"#eee"}}> Block </Button>}</TableCell>
                                <TableCell align="center"> <Button onClick={()=>handleDeleteAccount(row.id)}> <DeleteForeverIcon /> </Button>  </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Grid>

    );
}