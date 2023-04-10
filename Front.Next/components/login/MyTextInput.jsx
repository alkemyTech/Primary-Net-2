import { Grid, TextField } from "@mui/material";
import { ErrorMessage, Field, useField } from "formik";



export const MyTextInput = ({ label, ...props }) => {
  const [field] = useField(props);

  return (
    <Grid item xs={12} sx={{mt:2}} >
        <TextField 
        label={label} 
        type={props.type? props.type : "text"} 
        className="text-input" {...field} {...props} 
        fullWidth 
        sx={{backgroundColor: "#E0E7FF"}} 
        onChange={ props.onChange }
        value={props.value}
        error={!!props.errors}
        />
      <ErrorMessage name={props.name} component={"span"} />
    </Grid>
  );
};
