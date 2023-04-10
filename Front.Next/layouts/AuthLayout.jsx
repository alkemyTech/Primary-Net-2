import { Grid, Typography } from "@mui/material"
import primate from "../public/primate.jpg"
import Image from "next/image"

export const AuthLayout = ({children, title}) => {
    return (
        <Grid container
            spacing={0}
            direction={"column"}
            alignItems={"center"}
            justifyContent={"center"}
            sx={{
                minHeight: "100vh",
                backgroundColor: "primary.main",
                padding: 4
            }}
        >
            <Grid 
            item
            >
                <Image src={primate} alt="monito"/> 

            </Grid>
            <Grid item
                className=""
                xs={3}
                sx={{
                    backgroundColor: "secondary.main",
                    padding: 3,
                    borderRadius: 2,
                    width: { md: 450 }
                }}
            
            >
                <Typography
                variant="h5"
                sx={{mb:1}}
                fontWeight={"bold"}
                textAlign={"center"}
                >
                    

                    {title}
                </Typography>
                {children}
            </Grid>



        </Grid>
    )
}
