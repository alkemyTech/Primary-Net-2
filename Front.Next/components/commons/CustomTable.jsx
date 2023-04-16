import * as React from "react";
import {
  Button,
  Grid,
  TextField,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
} from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import ArrowForwardIcon from "@mui/icons-material/ArrowForward";
import FmdGoodIcon from "@mui/icons-material/FmdGood";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import Link from "next/link";
import AddCircleIcon from "@mui/icons-material/AddCircle";
import contentTable from "./contentTable";
import { useSession } from "next-auth/react";

/*
  componente base para completar tablas de vistas
  necesita el contenido a llenar en la tabla, el nombre de las columnas, los atributos a mostrar de la entidad, funciones para cambiar de pagina, eliminar y el nombre base de la ruta
*/

function AdminTable({
  columnLabels,
  rows,
  handlePrevPage,
  handleNextPage,
  handleDelete,
  routeBase,
  dataProperties,
  newButton = false,
}) {
  const { data: session } = useSession();

  //como es una tabla reutilizable definimos las acciones que puede realizar un admin y un usuario normal
  let labelActions =
    session?.user?.rol === "Admin" ? ["Detail", "Edit", "Delete"] : ["Detail"];

  return (
    <>
      <Grid container>
        <Grid
          container
          display={"flex"}
          justifyContent={"flex-end"}
          spacing={2}
          gap={1}
          alignItems={"center"}
        >
          <Button
            variant="contained"
            onClick={handlePrevPage}
            sx={{ height: "75%", width: "8%" }}
          >
            <ArrowBackIcon />
          </Button>

          <Button
            variant="contained"
            onClick={handleNextPage}
            sx={{ height: "75%", width: "8%" }}
          >
            <ArrowForwardIcon />
          </Button>
        </Grid>

        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                {columnLabels.map((label) => (
                  <TableCell
                    key={label}
                    sx={{ fontWeight: "bold" }}
                    align="center"
                  >
                    {label}
                  </TableCell>
                ))}
                {labelActions.map((label) => (
                  <TableCell
                    key={label}
                    sx={{ fontWeight: "bold" }}
                    align="center"
                  >
                    {label}
                  </TableCell>
                ))}
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map((row) => (
                <TableRow
                  key={row.id ? row.id : row.userId}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  {/*llama a una funcion para mostrar solo los atributos necesarios ya que hay entidades con datos que no queremos mostrar */}
                  {contentTable(row, dataProperties)}

                  <TableCell align="center">
                    <Link
                      style={{ textDecoration: "none", color: "#000" }}
                      href={`${routeBase}/${row.id ? row.id : row.userId}`}
                    >
                      {" "}
                      <FmdGoodIcon />{" "}
                    </Link>{" "}
                  </TableCell>
                  {/* ahora se puede tambien reutilizar el componente para las vistas de usuario normal */}
                  {session.user.rol == "Admin" && (
                    <>
                      <TableCell align="center">
                        <Link
                          style={{ textDecoration: "none", color: "#000" }}
                          href={`${routeBase}/${row.id ? row.id : row.userId}`}
                        >
                          {" "}
                          <EditIcon />{" "}
                        </Link>{" "}
                      </TableCell>
                      <TableCell align="center">
                        <Button
                          style={{ textDecoration: "none", color: "#000" }}
                          onClick={() =>
                            handleDelete(row.id ? row.id : row.userId)
                          }
                        >
                          {" "}
                          <DeleteForeverIcon />{" "}
                        </Button>{" "}
                      </TableCell>
                    </>
                  )}
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
        {/*Solo tendra el boton de agregar uno nuevo desde esta vista si es admin */}
        {(session.user.rol == "Admin" && newButton ) && (
          <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
            <Link
              href={`{routeBase}/new`}
              style={{ textDecoration: "none", color: "none" }}
            >
              <Button color={"tertiary"}>
                <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
              </Button>
            </Link>
          </Grid>
        )}
      </Grid>
    </>
  );
}

export default AdminTable;
