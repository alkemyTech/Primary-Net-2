import DenseTableRoles from '@/components/adminPanel/roles/RolesAdmin'
import DenseTable from '@/components/adminPanel/roles/RolesAdmin'
import { Layout } from '@/layouts/Layout'
import { Grid, List, ListItem } from '@mui/material'
import axios from 'axios'
import { useState, useEffect } from 'react'

const columns = []



function RolesAdmin() {
  const [roles, setRoles] = useState([])

  useEffect(() => {
    async function fetchData() {
      const {data} = await axios.get('https://localhost:7149/api/Role')
      console.log(data);
      setRoles(data)
    }
    fetchData()
  }, [])

  return (
    <Layout>
      <DenseTableRoles rows={roles}/>
    </Layout>
  )
}

export default RolesAdmin
