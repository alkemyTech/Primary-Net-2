import React from "react";
import { TableCell } from "@mui/material";
/*
  recibe una entidad y los atributos que queremos mostrar
  por ejemplo user[userId], usamos esta funcion para solo mostrar los atributos que necesitamos y excluimos por ejemplo contraseña
*/
function contentTable(data, properties) {
  return (
    <>
      {properties.map((property, index) => (
        <TableCell key={index} align="center">
          {data[property]}
        </TableCell>
      ))}
    </>
  );
}

export default contentTable;
