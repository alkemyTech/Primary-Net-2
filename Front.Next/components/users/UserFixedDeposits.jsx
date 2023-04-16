import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Button, Grid, IconButton, TextField, Typography } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import FmdGoodIcon from '@mui/icons-material/FmdGood';
import Link from 'next/link';
import axios from 'axios';
import Swal from 'sweetalert2'
import { useRouter } from 'next/router';
import { getSession, useSession } from 'next-auth/react';
import { useState } from 'react';


export const UserFixedDeposits = ({ fixed }) => {
  const [fixedDeposits, setFixedDeposits] = useState(fixed);
  const router = useRouter();
  const { data: session } = useSession();
  const userName = session?.user?.first_Name || "User";


  const nonDeletedFixedDeposits = fixedDeposits.filter(fixedDeposit => !fixedDeposit.isDeleted);
  const formatDate = (date = "") => {
    const day = date.slice(0, 10 )
    const time=  date.slice(11, 16 )
    return `${day} at ${time}`
  }


  const handleDeletefixedDeposit = async (fixedDepositId) => {
    const session = await getSession();

    Swal.fire({
      title: `Do you want to delete fixedDeposit #${fixedDepositId} ?`,
      showDenyButton: false,
      showCancelButton: true,
      confirmButtonText: 'Delete',
    }).then((result) => {
      if (result.isConfirmed) {
        axios
          .delete(` https://localhost:7149/api/FixedDeposit/${fixedDepositId}`, {
            headers: {
              Authorization: `Bearer ${session.user?.token}`,
              'Content-Type': 'application/json',
            },
          })
          .then((res) => {
            const updatedFixedDeposits = fixedDeposits.filter((fd) => fd.id !== fixedDepositId);
            setFixedDeposits(updatedFixedDeposits);
            Swal.fire('Deleted!', '', 'success')
            router.reload();
          });
      } else {
        Swal.fire('Changes are not saved', '', 'info');
      }
    });
  };

  return (
    <Grid container >
    <Typography variant='h4' sx={{pb:2}}>
            {`${userName}`} Fixed Term Deposits
        </Typography>

        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                <TableHead>
                    <TableRow>
                        <TableCell sx={{ fontWeight: "bold" }} align="center">Deposit Id</TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Creation Date </TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Closing Date </TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Return Amount </TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Detail</TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Edit</TableCell>
                        <TableCell sx={{ fontWeight: "bold" }} align="center"> Delete</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                {nonDeletedFixedDeposits?.map((row) => ( 
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >

                            <TableCell align="center">{row.id}</TableCell>
                            <TableCell align="center">  {formatDate(row.creation_Date)}</TableCell>
                            <TableCell align="center">  {formatDate(row.closing_Date)}</TableCell>
                            <TableCell align="center"> $ {row.amount} </TableCell>
                        
                            <TableCell align="center">
                                <Link style={{ textDecoration: "none", color: "#000" }} href={`/fixed/${row.id}`}> <FmdGoodIcon /> </Link>
                            </TableCell>
                            <TableCell align="center"> <IconButton onClick={()=> {router.push(`fixed/edit/${row.id}`)}}><EditIcon /> </IconButton>  </TableCell>
                            <TableCell align="center"> <Button onClick={() => handleDeletefixedDeposit(row.id)}> <DeleteForeverIcon /> </Button>  </TableCell>

                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    </Grid>
  )
}
