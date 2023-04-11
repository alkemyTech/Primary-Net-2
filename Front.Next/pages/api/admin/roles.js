// import axios from 'axios'
// import { useState, useEffect } from 'react'

// function RolesAdmin() {
//   const [roles, setRoles] = useState([])

//   useEffect(() => {
//     async function fetchData() {
//       const {data} = await axios.get('https://localhost:7149/api/Role')
//       console.log(data);
//       setRoles(data)
//     }
//     fetchData()
//   }, [])

//   return (
//     <div>
//       <h1>My Data</h1>
//       <ul>
//         {roles.map((item) => (
//           <li key={item.id}>{item.name}</li>
//         ))}
//       </ul>
//     </div>
//   )
// }

// export default RolesAdmin
