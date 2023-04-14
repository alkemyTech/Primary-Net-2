import { Layout } from "@/layouts/Layout";
import { Button, Grid, Typography } from "@mui/material";
import { Formik } from "formik";
import { MyTextInput } from "../login/MyTextInput";

/*
  Componente reutilizable para crear formularios de altas
  entity: nombre de la entidad para usar en los mensajes
  fields: cambos de la entidad que vamos a agregar al form
  validations: la podemos definir externas al componente de distintas formas
  onSubmit: accion a ejecutar
*/
function CreateForm({entity, fields, validations, onSubmit }) {

  //fields recibe el nombre de los atributos con reduce podemos pasar de po ej : id a id:""
  const initialValues = fields.reduce(
    (acc, field) => ({ ...acc, [field]: "" }),
    {}
  );

  return (
    <>
      return (
      <Layout title={"Create Role"}>
        <Typography variant="h4">{`Create ${entity}`}</Typography>
        <Grid
          container
          display={"flex"}
          flexDirection={"column"}
          fullWidth
          alignItems={"center"}
          justifyContent={"center"}
          sx={{ p: 4, borderRadius: 5, backgroundColor: "white", mt: 4 }}
        >
          <Grid container width={"50%"}>
            <Formik
              initialValues={initialValues} //valores iniciales que obtenemos con el reduce
              onSubmit={onSubmit}
              validationSchema={validations} //validaciones para los campos
            >
              {({ values, errors, handleChange, handleBlur }) => (
                <form onSubmit={(e) => onSubmit(e, values)}>
                  <Grid container>
                    {fields.map((field) => ( //para crear un input por atributo
                      <MyTextInput
                        key={field}
                        label={field}
                        type={"text"}
                        placeholder={`New ${field}...`}
                        name={field}
                        value={values[field]}
                        onBlur={handleBlur}
                        onChange={handleChange}
                        errors={errors[field]}
                      />
                    ))}
                  </Grid>

                  <Grid
                    container
                    fullWidth
                    justifyContent={"center"}
                    alignItems={"center"}
                  >
                    <Grid item xs={12} sm={6}>
                      <Button
                        type="submit"
                        variant="contained"
                        fullWidth
                        sx={{
                          backgroundColor: "tertiary.main",
                          padding: 1,
                          mt: 2,
                        }}
                      >
                        {`Create ${entity}`}
                      </Button>
                    </Grid>
                  </Grid>
                </form>
              )}
            </Formik>
          </Grid>
        </Grid>
      </Layout>
      );
    </>
  );
}

export default CreateForm;
