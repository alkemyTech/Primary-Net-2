import { Button, Grid, Typography } from '@mui/material'
import Link from 'next/link'

export const AdminItem = ({ name, path, icon }) => {
    return (
        <Grid item xs={2} sm={6} md={4}>
            <Button variant='contained' sx={{p:2 , width:"100%"}}>
                <Link style={{textDecoration:"none"}} href={`${path}`}>
                    <Typography variant='h5' color={"white"} textTransform={"none"}>{name}</Typography>
                </Link>
            </Button>
        </Grid>
    )
}
