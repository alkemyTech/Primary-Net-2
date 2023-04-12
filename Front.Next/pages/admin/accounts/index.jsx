import DenseTableAccounts from '@/components/adminPanel/accounts/AccountsAdmin';
import { Layout } from '@/layouts/Layout';
import { Button, Grid } from '@mui/material';
import axios from 'axios';
import { getSession } from 'next-auth/react';
import { useState } from 'react';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';

let page = 1

function AccountsAdmin({ accounts }) {
  
  console.log(accounts);
  const [ currentPage, setCurrentPage ] = useState(1)

    const prevPageRequest = () => {
      if(accounts.previousPage === "None" || accounts.previousPage === "none" || !accounts.previousPage ) return
      setCurrentPage(currentPage - 1)   
      page -=1
    }

    const nextPageRequest = () => {
      if(accounts.nextPage === "None" || accounts.nextPage === "none" || !accounts.nextPage ) return
      setCurrentPage(currentPage +1)   
      page +=1
    }

      

  return (
    <Layout>

        <Grid container display={"flex"} position={"fixed"} justifyContent={"center"} alignItems={"flex-end"} spacing={2} gap={1} bottom={1}>
          <Button variant='contained'  onClick={prevPageRequest} sx={{ height: "75%", width: "8%" }}
          disabled={!accounts.previousPage || accounts.previousPage == "None" || typeof(accounts.previousPage == "undefined")}
          >
            <ArrowBackIcon />
          </Button>
          <Button variant='contained' onClick={nextPageRequest} sx={{ height: "75%", width: "8%" }}
          disabled={!accounts.nextPage || accounts.nextPage == "None" || typeof(accounts.nextPage == "undefined")}
          >
            <ArrowForwardIcon />
          </Button>
        </Grid>



        <DenseTableAccounts rows={accounts} />
    </Layout>
  )
}

export const getServerSideProps = async (context) => {
  try {

    const session = await getSession(context);

    const res = await axios.get(`https://localhost:7149/api/Account?page=${page}&pageSize=10`, {
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
