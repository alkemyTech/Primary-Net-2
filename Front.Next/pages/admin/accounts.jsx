import DenseTableAccounts from '@/components/adminPanel/accounts/AccountsAdmin';
import { useState, useEffect } from 'react';
import { Layout } from '@/layouts/Layout';
import axios from 'axios';



function AccountsAdmin() {
  const [accounts, setAccounts] = useState([])

  useEffect(() => {
    async function fetchData() {
      const {data} = await axios.get('https://localhost:7149/api/Account')
      console.log(data);
      setAccounts(data)
    }
    fetchData()
  }, [])

  return (
    <Layout>
      <DenseTableAccounts rows={accounts}/>
    </Layout>
  )
}

export default AccountsAdmin
