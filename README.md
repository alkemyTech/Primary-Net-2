# ALKEMY JAVA TECHNICAL CHALLENGE - WALLET

### PROJECT SETUP & TOOLS
3. [Postman](https://www.postman.com/downloads/) para testear los endpoints.

### CODE STANDARDS
- Los controladores deben terminar con el sufijo "Controller". Ejemplo: UserController.
- Los servicios deben terminar con el sufijo "Service". Ejemplo: UserService.
- Los repositorios deben terminar con el sufijo "Repository". Ejemplo: UserRepository.
- Las interfaces deben comenzar con el prefijo "I". Ejemplo: IUserRepository.
- Los DTOs deben terminar con el sufijo "Dto". Ejemplo: UserDto, UserRequestDto.
- El uso de DTOs es imprescindible. Puede tener DTOs para petición y respuesta.
- Los nombres de los paquetes deben estar en singular.
- Los nombres de atributos/campos de clases C# deben escribirse en camel case. Ejemplo: firstName.

### GIT STANDARDS

#### FORMAT
- Crear siempre la rama desde develop
- El formato del nombre de la rama es: `feature/{jiraTicket#}`.
- El formato del título del pull request es: `{jiraTicket#}: {jiraTitle}`.
- El formato de commits es: `{jiraTicket#}: {commitDescription}`. Es bueno tener commits pequeños.
- El pull request tiene que contener sólo los cambios relacionados con el ámbito definido en el ticket.
- Pull request siempre debe ser de su rama actual a desarrollar.

#### EVIDENCE
- Si no escribes pruebas unitarias o pruebas de integración como parte de tus cambios de código, deberías añadir la petición y respuesta HTTP como evidencia de que el código está funcionando como se esperaba.
- Las capturas de pantalla de Postman con diferentes escenarios son una buena forma de mostrar tu trabajo.

#### BRANCHES
En el repositorio actual verán tres ramas diferentes
- `master` -> esta rama es sólo para versiones productivas, tiene historia de lanzamiento oficial.
- `develop` -> esta rama sirve como rama de integración para las funcionalidades. Todas las funcionalidades deben empezar desde esta rama y una vez terminadas se fusionan de nuevo en "develop".

Para entender más sobre git y cómo trabajar con diferentes ramas, recomiendo leer sobre el flujo de trabajo Gitflow. [Aquí](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) tienes una pequeña explicación que te puede servir de introducción.
