import '@/styles/globals.css'
import { ThemeProvider } from '@emotion/react';
import mainTheme from '@/themes/mainTheme';
import { CssBaseline } from '@mui/material';
import axios from 'axios';

function App({ Component, pageProps }) {


  if(typeof window !== "undefined"){
    const auth = localStorage.getItem("Token") || "";
    if(auth){
      axios.defaults.headers.common= {"Authorization": `Bearer ${auth}`}
    }else{
      axios.defaults.headers.common = {"Authorization":null}
    }
  }

  return (
    <>
          <ThemeProvider theme={mainTheme}>
            <CssBaseline/>
            <Component {...pageProps} />
          </ThemeProvider>
    </>
  )
}

export default App;