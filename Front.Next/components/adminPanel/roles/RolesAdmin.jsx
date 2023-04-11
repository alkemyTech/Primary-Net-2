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
import AddCircleIcon from '@mui/icons-material/AddCircle';

export default function DenseTableRoles({ rows }) {

    const [search, setSearch] = React.useState("");
    const [currentPage, setCurrentPage] = React.useState(0);

    const handleSearch = (e) => {
        console.log(search);
        setSearch(e.target.value)
    }
    console.log(rows);


    const itemsInPage = () => {
        // if (rows.length < 10 && search.length === 0) return rows
        if (search.length === 0 && currentPage === 0) return rows.slice(currentPage, currentPage + 10)
        let filteredRows = rows.filter(c => c.name.toLowerCase().includes(search.toLowerCase()))

        if (currentPage === 0) {
            return filteredRows.slice(currentPage, currentPage + 10)
        }
        return filteredRows.slice(currentPage, currentPage + 10)

    }

    const nextPage = () => {
        if (rows.filter(c => c.name.includes(search)).length > currentPage + 10) {
            setCurrentPage(currentPage + 10)
        }
    }

    const prevPage = () => {
        if (currentPage > 0) {
            setCurrentPage(currentPage - 10)
        }
    }

    return (

        <Grid container>

            <Grid container display={"flex"} justifyContent={"center"} spacing={2} gap={1} alignItems={"center"}>
                <TextField name='search' onChange={handleSearch}
                    sx={{ width: "80%", pb: 2, height: "80%" }} placeholder='Search roles...'
                />
                <Button variant='contained' onClick={prevPage} sx={{ height: "75%", width: "8%" }}>
                    <ArrowBackIcon />
                </Button>

                <Button variant='contained' onClick={nextPage} sx={{ height: "75%", width: "8%" }}>
                    <ArrowForwardIcon />
                </Button>
            </Grid>


            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontWeight: "bold" }} align="center">Id</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Name </TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Description</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Detail</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Edit</TableCell>
                            <TableCell sx={{ fontWeight: "bold" }} align="center"> Delete</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {itemsInPage().map((row) => (
                            <TableRow
                                key={row.name}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >

                                <TableCell align="center">{row.id}</TableCell>
                                <TableCell align="center">{row.name}</TableCell>
                                <TableCell align="center">{row.description}</TableCell>
                                <TableCell align="center"><Link style={{ textDecoration: "none", color: "#000" }} href={`/role/${row.id}`}> <FmdGoodIcon /> </Link> </TableCell>
                                <TableCell align="center"> <EditIcon /> </TableCell>
                                <TableCell align="center"> <DeleteForeverIcon /> </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
                <Link href={"/roles/new"} style={{ textDecoration: "none", color: "none" }}>
                    <Button color={"tertiary"}>
                        <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
                    </Button>
                </Link>

            </Grid>
        </Grid>

    );
}