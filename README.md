# <img src="https://img.icons8.com/color/30/null/project-management.png"/> Proyecto : Wallet

### <img src="https://img.icons8.com/external-flaticons-flat-flat-icons/64/null/external-starting-auction-house-flaticons-flat-flat-icons-2.png"/> Situación inicial

Como parte de un equipo de programadores recibimos el pedido de un cliente nuevo que
necesita desarrollar una billetera virtual (Wallet), la cual permitirá a sus usuarios realizar
transacciones entre ellos.

### <img src="https://img.icons8.com/color/48/null/goal--v1.png"/> Tu objetivo

Nuestra tarea consiste en desarrollar una API en C# con .NET que permita realizar
transacciones de dinero entre usuarios como también depósitos en cuenta, contar con
funcionalidades de administración y manejo de errores. Para documentar la API, se debe
utilizar la librería Swagger, que nos permitirá probar su funcionamiento.
Complementariamente desarrollar un Frontend en NextJS que proporcione una interfaz
de usuario intuitiva y fácil de usar, con un diseño atractivo y funcional.

### <img src="https://img.icons8.com/external-flaticons-lineal-color-flat-icons/64/null/external-requirement-agile-flaticons-lineal-color-flat-icons.png"/> Requerimientos

La API deberá cumplir con una serie de características y requerimientos técnicos para
garantizar la calidad y funcionalidad de la misma.
Primero, como requerimiento general tenemos que poder implementar todas las
funcionalidades básicas que un usuario necesita para usar una billetera virtual, las
cuales son:
- Iniciar sesión
- Realiza depósitos en su cuenta
- Realizar transferencias a otras cuentas
- Actualizar y visualizar sus datos
- Visualizar el catálogo de productos.

También tenemos que generar un rol de usuarios administradores que van a tener
permisos más elevados y van a realizar tareas administrativas y de mantenimientos
sobre los otros usuarios, estos usuarios deberían poder:
- Crear y eliminar usuarios
- Actualizar y visualizar los datos de otros usuarios
- Actualizar y visualizar los datos del catálogo de productos
- Actualizar, listar y eliminar transacciones realizadas por otros usuarios

El Frontend debe proporcionar los componentes Pages con vistas que permitan a los
usuarios crear cuentas, gestionar sus transacciones y realizar transferencias de manera
segura y rápida.

---

## <img src="https://img.icons8.com/color/26/null/person-male.png"/> Integrantes del grupo:

- Cespedes Matias [<img src="https://img.icons8.com/material-rounded/24/null/github.png"/>](https://github.com/cespedesmati) [<img src="https://img.icons8.com/fluency/24/null/linkedin.png"/>](https://www.linkedin.com/in/matiascespedes/)

- Escobar Luciano [<img src="https://img.icons8.com/material-rounded/24/null/github.png"/>](https://github.com/Lucianoesc) [<img src="https://img.icons8.com/fluency/24/null/linkedin.png"/>](https://www.linkedin.com/in/lucianoesc/)

- Pirovano Gonzalo [<img src="https://img.icons8.com/material-rounded/24/null/github.png"/>](https://github.com/gnz6) [<img src="https://img.icons8.com/fluency/24/null/linkedin.png"/>](https://www.linkedin.com/in/gonzalo-pirovano/)

- Tribulo Samuel [<img src="https://img.icons8.com/material-rounded/24/null/github.png"/>](https://github.com/samueltribulo) [<img src="https://img.icons8.com/fluency/24/null/linkedin.png"/>](https://www.linkedin.com/in/samueltribulo/)

---

## <img src="https://img.icons8.com/external-flaticons-flat-flat-icons/26/null/external-scrum-agile-flaticons-flat-flat-icons-3.png"/> Metodología

Utilizamos la metodología **scrum** durante todo el proceso de creación de la aplicación.

Durante todo el proceso de creación de la aplicación, utilizamos la metodología Scrum, la cual nos permitió trabajar de manera ágil y eficiente en equipo.

Para planificar el trabajo, realizamos una reunión de Planning al inicio de cada Sprint, donde definimos las tareas a realizar y establecimos los objetivos a alcanzar. Además, cada mañana a las 8.00 am llevamos a cabo la Daily, una reunión breve donde cada integrante cuenta el estado de sus tareas y si existe algún bloqueo.

También, una vez a la semana llevamos a cabo la Retro, una reunión donde hacemos un balance de la semana y analizamos lo positivo para seguir haciendo, así como también identificamos los problemas y obstáculos para poder corregirlos y mejorar en el siguiente Sprint.

<img src="https://img.icons8.com/external-flaticons-flat-flat-icons/24/null/external-scrum-agile-flaticons-flat-flat-icons-7.png"/> Tablero en Jira donde podíamos ver el **backlog**, la **hoja de ruta** y el **sprint activo**:

<img src="https://user-images.githubusercontent.com/45923696/230903706-bb2dc96f-1310-49d1-aa69-b0ccaad10de0.png" widht=200 alt="imagen de un sprint" />

---

## <img src="https://img.icons8.com/bubbles/30/null/gender-neutral-user.png"/> Login

Para loguearse como administrador creamos el siguiente usuario de prueba:

nombre: **Leo Messi**

email: **LeoMessi@gmail.com**

password: **campeonMund14l!**


---

## <img src="https://img.icons8.com/office/30/null/console.png"/> Scripts disponibles

En este proyecto, podés correr:

### `npm install`

Para instalarte todos las dependencias que utiliza el proyecto.

### `npm start`

Para correr la app.

Para poder verlo en el navegador, abrir: [http://localhost:3000](http://localhost:3000).

### `dotnet restore`

Para restaurar las dependencias del proyecto.

### `dotnet build`

Para compilar el proyecto y generar los archivos de salida.

### `npm start`

Para ejecutar la aplicación.

Para poder verlo en el navegador, abrir: [https://localhost:7149/swagger](https://localhost:7149/swagger).

---
