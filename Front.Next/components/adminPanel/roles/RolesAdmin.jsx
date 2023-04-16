import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { Button, Grid, TextField, Typography } from "@mui/material";
import FmdGoodIcon from "@mui/icons-material/FmdGood";
import Link from "next/link";
import AddCircleIcon from "@mui/icons-material/AddCircle";

export default function DenseTableRoles({ rows, handleDelete }) {
  return (
    <Grid container>
      <Typography variant="h4" sx={{ pb: 2 }}>
        Roles
      </Typography>

      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
          <TableHead>
            <TableRow>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                Id
              </TableCell>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                {" "}
                Name{" "}
              </TableCell>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                {" "}
                Description
              </TableCell>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                {" "}
                Detail
              </TableCell>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                {" "}
                Edit
              </TableCell>
              <TableCell sx={{ fontWeight: "bold" }} align="center">
                {" "}
                Delete
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
              <TableRow
                key={row.name}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell align="center">{row.id}</TableCell>
                <TableCell align="center">{row.name}</TableCell>
                <TableCell align="center">{row.description}</TableCell>
                <TableCell align="center">
                  <Link
                    style={{ textDecoration: "none", color: "#000" }}
                    href={`/admin/roles/${row.id}`}
                  >
                    {" "}
                    <FmdGoodIcon />{" "}
                  </Link>{" "}
                </TableCell>
                <TableCell align="center">
                  {" "}
                  <Link style={{ textDecoration: "none", color: "#000" }} href={`/admin/roles/update/${row.id}`}> <EditIcon /> </Link>
                </TableCell>
                <TableCell align="center">
                  {" "}
                  <Button onClick={() => handleDelete(row.id)}>
                    <DeleteForeverIcon />{" "}
                  </Button>{" "}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
        <Link
          href={"roles/newrole"}
          style={{ textDecoration: "none", color: "none" }}
        >
          <Button color={"tertiary"}>
            <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
          </Button>
        </Link>
      </Grid>
    </Grid>
  );
}
