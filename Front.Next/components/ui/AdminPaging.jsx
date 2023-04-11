import useLocalStorage from "@/hooks/useLocalStorage";
import { Box, Button, Grid, TextField, Typography } from "@mui/material";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
import Link from "next/link";

export const AdminPaging = ({ allResults }) => {

  const [currentPage, setCurrentPage] = useLocalStorage("currentPage", "");
  const [search, setSearch] = useLocalStorage("search", "");



  const itemsPerPage = () => {
    if (search.length === 0 && currentPage === 0) return allResults.slice(currentPage, currentPage + 10)
    let filteredResults = allResults.filter(r => r.name.toLowerCase().includes(search.toLowerCase()))

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
    if (allResults.filter(c => c.name.includes(search)).length > currentPage + 10) {
      setCurrentPage(currentPage + 10)
    }
  }

  const prevPage = () => {
    if (currentPage > 0) {
      setCurrentPage(currentPage - 10)
    }
  }
  return (
    <Grid container>

      {allResults.length > 10
        ?
        <Grid container fullWidth>
        <TextField size="normal" sx={{width:"75%"}} label="Search" variant="standard" onChange={handleSearch}  />
        <Grid container sx={{width:"25%"}}>
          <ArrowBackIcon onClick={prevPage} />
          <ArrowForwardIcon onClick={nextPage} />
        </Grid>
        </Grid>
        : 
        <Grid container display={"flex"} direction={"column"} alignItems={"center"} justifyContent={"center"} bgcolor={"#eee"} height={"99vh"}>
        <Grid container display={"flex"} justifyContent={"center"} direction={"row"}>

        <Typography variant="h4" color={"#aaa"}>
          No Reults Found
        </Typography>
          <ErrorOutlineIcon fontSize={"large"} color="error"/>
        </Grid>
          <Button type="contained" > 
          <Link href={"/"} style={{textDecoration:"none", color:"#000" , padding: 8 ,border:"1px solid black"}}>Return to Primates Wallet</Link>  
           </Button>

        </Grid>

      }
    </Grid>

  )
}
