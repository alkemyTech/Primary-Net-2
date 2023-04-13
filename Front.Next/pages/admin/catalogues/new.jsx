import CreateForm from "@/components/commons/CreateForm";
import axios from "axios";
import { useRouter } from "next/router";
import Swal from "sweetalert2";
import * as Yup from "yup";

const CreateProduct = () => {
  const router = useRouter();

  // accion despues de que se completa el formulario
  const onSubmit = async (e, values) => {
    values.points = parseInt(values.points);
    e.preventDefault();
    try {
      const url = "https://localhost:7149/api/Catalogue";

      await axios.post(url, values);

      Swal.fire("New product created successfully.", ``, "success");
      router.back(); //volvemos a la pag anterior en este caso la lista de productos
    } catch (error) {
      Swal.fire(`${error.message}`, ``, "error");
    }
  };

  //Validaciones con yup, como lo definimos fuera del componente se puede cambiar por otra alternativa
  const validations = Yup.object({
    productDescription: Yup.string()
      .required("Product description is required")
      .max(200, "Product description can't be longer than 200 characters"),
    image: Yup.string()
      .required("Image is required")
      .url("Image must be a valid URL"),
    points: Yup.number()
      .required("Points are required")
      .min(1, "Points must be at least 1"),
  });

  return (
    <>
      <CreateForm
        entity={"Product"}
        fields={["productDescription", "image", "points"]}
        validations={validations}
        onSubmit={onSubmit}
      />
    </>
  );
};

export default CreateProduct;
