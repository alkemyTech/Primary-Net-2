import DenseTableAccounts from '@/components/adminPanel/accounts/AccountsAdmin';
import { Layout } from '@/layouts/Layout';
import { Button, Grid } from '@mui/material';
import axios from 'axios';
import { getSession } from 'next-auth/react';
import { useState } from 'react';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';

function AccountsAdmin({ accounts }) {

  const [currentPage, setCurrentPage] = useState(0);
  const [search, setSearch] = useState("");


  console.log(accounts)

  const itemsPerPage = () => {
    if (search.length === 0 && currentPage === 0) return accounts?.result.slice(currentPage, currentPage + 10)
    let filteredResults = accounts?.result.filter(r => r.productDescription.toLowerCase().includes(search.toLowerCase()))

    if (currentPage === 0) {
      return filteredResults.slice(currentPage, currentPage + 10)
    }
    return filteredResults.slice(currentPage, currentPage + 10)

  }

  const handleSearch = (e) => {
    setSearch(e.target.value)
    setCurrentPage(0)
  }

  const nextPage = () => {
    if (accounts?.result.filter(c => c.productDescription.includes(search)).length > currentPage + 10) {
      setCurrentPage(currentPage + 10)
    }
  }

  const prevPage = () => {
    if (currentPage > 0) {
      setCurrentPage(currentPage - 10)
    }
  }

  return (
    <Layout>

      <DenseTableAccounts rows={accounts} />
      <Grid container display={"flex"} justifyContent={"center"} alignItems={"center"} pt={2}>
        <Button onClick={prevPage} disabled={currentPage - 6 < 0}>
          <ArrowBackIosIcon fontSize='large' color='primary.main' />
        </Button>

        <Button onClick={nextPage} disabled={currentPage + 6 >= accounts?.result.length}>
          <ArrowForwardIosIcon fontSize='large' color='primary.main' />
        </Button>
      </Grid>



    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  try {

    const session = await getSession(context);

    const res = await axios.get(`https://localhost:7149/api/Account?page=${page}&pageSize=100`, {
      headers: {
        'Authorization': `Bearer ${session.user?.token}`,
        "Content-Type": "application/json",
      }
    });

    console.log(res.data);
    return {
      props: {
        accounts: res.data
      }
    };
  } catch (error) {
    console.error(error);
    return {
      props: {
        accounts: null // Retorna null si hay algún error
      }
    };
  }
};

export default AccountsAdmin
