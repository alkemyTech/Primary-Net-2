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
import { NoProducts, PointsAlert } from '@/components/adminPanel/catalogue/NoProducts'
import Head from 'next/head'


const CatalogueIndex = ({ products }) => {

    const [currentPage, setCurrentPage] = useState(0);
    const [search, setSearch] = useState("");
    const [filterPoints, setFilterPoints] = useState(false);


    const { data: session } = useSession();
    const userRole = session?.user?.rol;

    const itemsPerPage = () => {
        if (!filterPoints) {
            if (search.length === 0 && currentPage === 0) return products?.result.slice(currentPage, currentPage + 6)
            let filteredResults = products?.result.filter(r => r.productDescription.toLowerCase().includes(search.toLowerCase()))

            if (currentPage === 0) {
                return filteredResults.slice(currentPage, currentPage + 6)
            }
            return filteredResults.slice(currentPage, currentPage + 6)
        }

        let filteredResults = products?.result.filter(r => r.productDescription.toLowerCase().includes(search.toLowerCase()))
        let pointFilter = filteredResults.filter(r => r.points <= session?.user?.points)

        if (currentPage === 0) {
            return pointFilter.slice(currentPage, currentPage + 6)
        }
        return pointFilter.slice(currentPage, currentPage + 6)

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
        <>
            <Head>
                <title>
                    Primates - Product catalogue
                </title>
            </Head>
            <Layout>

                <Grid container display={"flex"} direction="column" sx={{ mb: 1, mt: 1 }} >
                    <Typography variant='h4'>
                        Primates Store
                    </Typography>

                    <Grid container component={"div"} display={"flex"} direction={"row"} sx={{ pb: 4 }}>

                        <TextField size="normal" sx={{ width: "80%" }} label="Search Products by Name" variant="standard" onChange={handleSearch} />
                        <Button variant="text" sx={{ width: "20%", backgroundColor: "#ddd", ":hover": { color: "white", backgroundColor: "#FB923C" } }} onClick={() => setFilterPoints(!filterPoints)}>
                            Filter By Points ({session?.user?.points} Points)
                        </Button>
                    </Grid>

                    <Grid container sx={{ backgroundColor: "primary.default", pt: 2, pb: 4, justifyContent: "center", alignItems: "center" }}>
                        {
                            itemsPerPage()?.length === 0 ?
                                <PointsAlert pointsNeeded={session?.user?.points} />
                                :
                                <>
                                    <ListProducts products={itemsPerPage()} />
                                    <Grid container display={"flex"} justifyContent={"center"} alignItems={"center"} pt={2}>
                                        <Button onClick={prevPage} disabled={currentPage - 6 < 0}>
                                            <ArrowBackIosIcon fontSize='large' color='primary.main' />
                                        </Button>

                                        <Button onClick={nextPage} disabled={currentPage + 6 >= products?.result.length}>
                                            <ArrowForwardIosIcon fontSize='large' color='primary.main' />
                                        </Button>
                                    </Grid>

                                </>
                        }


                    </Grid>



                </Grid>
                {
                    userRole === "Admin"
                        ?
                        <Grid container position={"fixed"} bottom={"20px"} left={"90vw"}>
                            <Link href={"admin/catalogues/new"} style={{ textDecoration: "none", color: "none" }}>
                                <Button color={"tertiary"}>
                                    <AddCircleIcon sx={{ height: "100px", width: "100px" }} />
                                </Button>
                            </Link>
                        </Grid>
                        : null
                }
            </Layout>
        </>
    )
}

export const getServerSideProps = async (context) => {
    try {

        const session = await getSession(context);

        const now = Math.floor(Date.now() / 1000);

        if (session == null || session.expires < now) {
            return {
                redirect: {
                    destination: '/login',
                    permanent: false,
                },
            };
        }

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