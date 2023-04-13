import { Grid, List, ListItem, Typography } from '@mui/material'
import Image from 'next/image'
import React from 'react'
import { ProductCard } from './ProductCard'

export const ListProducts = ({ products = [] }) => {
    return (

            <Grid container display={"flex"}  direction={"row"} width={"70vw"} justifyContent={"center"} gap={2} alignItems={"center"}  >
                {products.map(product => (
                    <ProductCard key={product.id} product={product}/>
                ))}

            </Grid>
    )
}
