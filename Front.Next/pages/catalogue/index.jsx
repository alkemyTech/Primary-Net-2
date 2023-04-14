import React, { useState } from 'react'
import { Button, Grid, Icon, IconButton, TextField, Typography } from "@mui/material"
import { Layout } from '@/layouts/Layout'
import { getSession, useSession } from 'next-auth/react'
import axios from 'axios'
import { ListProducts } from '@/components/cartalogue/ListProducts'
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';
import Link from 'next/link'
import AddCircleIcon from '@mui/icons-material/AddCircle';


const CatalogueIndex = ({products}) => {

    const [currentPage, setCurrentPage] = useState(0);
    const [search, setSearch] = useState("");
  
    const {data: session} = useSession();
    const userRole = session?.user?.rol;
    
    const itemsPerPage = () => {
        if (search.length === 0 && currentPage === 0) return products?.result.slice(currentPage, currentPage + 6)
        let filteredResults = products?.result.filter(r => r.productDescription.toLowerCase().includes(search.toLowerCase()))
        
        if (currentPage === 0) {
            return filteredResults.slice(currentPage, currentPage + 6)
        }
        return filteredResults.slice(currentPage, currentPage + 6)
        
    }
  
    const handleSearch = (e) => {
      setSearch(e.target.value)
      setCurrentPage(0)
    }
  
    const nextPage = () => {
      if (products?.result.filter(c => c.productDescription.includes(search)).length > currentPage + 6) {
        setCurrentPage(currentPage + 6)
      }
    }
  
    const prevPage = () => {
      if (currentPage > 0) {
        setCurrentPage(currentPage - 6)
      }
    }

    return (
        <Layout>
        <Grid container display={"flex"} direction="column" sx={{ mb: 1, mt: 1 }} >
            <Typography variant='h4'>
                Primates Store
            </Typography>
 
            <TextField size="normal" sx={{width:"100%", pb:4}} label="Search Products by Name" variant="standard" onChange={handleSearch}  />
        <Grid container sx={{backgroundColor:"primary.default", pt:2,pb:4,  justifyContent:"center", alignItems:"center"}}>
           <ListProducts products={itemsPerPage()}/>

        </Grid>

        <Grid container display={"flex"} justifyContent={"center"} alignItems={"center"} pt={2}>
            <Button onClick={prevPage} disabled={currentPage - 6 < 0}>
                    <ArrowBackIosIcon fontSize='large' color='primary.main'/>
            </Button>

            <Button onClick={nextPage} disabled={currentPage + 6 >= products?.result.length}>
                    <ArrowForwardIosIcon fontSize='large' color='primary.main'/>
            </Button>
        </Grid>

        </Grid>
        {
            userRole === "Admin"
            ?
        <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
                <Link href={"catalogue/newproduct"} style={{ textDecoration: "none", color: "none" }}>
                    <Button color={"tertiary"}>
                        <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
                    </Button>
                </Link>
                </Grid>
            :null
        }
        </Layout>
    )
}

export const getServerSideProps = async (context) => {
    try {

        const session = await getSession(context);

        const res = await axios.get(`https://localhost:7149/api/Catalogue?page=1&pageSize=100`, {
            headers: {
                'Authorization': `Bearer ${session.user?.token}`,
                "Content-Type": "application/json",
            }
        });
        return {
            props: {
                products: res.data
            }
        };
    } catch (error) {
        console.error(error);
        return {
            props: {
                products: null // Retorna null si hay algún error
            }
        };
    }
};



export default CatalogueIndex;