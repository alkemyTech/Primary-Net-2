import DenseTableRoles from '@/components/adminPanel/roles/RolesAdmin';
import { useState, useEffect } from 'react';
import { Layout } from '@/layouts/Layout';
import axios from 'axios';



function RolesAdmin() {
    const [roles, setRoles] = useState([])

    useEffect(() => {
        async function fetchData() {
            const { data } = await axios.get('https://localhost:7149/api/Role')
            setRoles(data)
        }
        fetchData()
    }, [])

    return (
        <Layout>
            <DenseTableRoles rows={roles} />
        </Layout>
    )
}

export default RolesAdmin
