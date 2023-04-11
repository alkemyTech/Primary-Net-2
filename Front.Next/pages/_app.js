import '@/styles/globals.css'
import { ThemeProvider } from '@emotion/react';
import mainTheme from '@/themes/mainTheme';
import { CssBaseline } from '@mui/material';
import { Provider, useSelector } from 'react-redux';
import { store } from '@/store';
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
      <Provider store={store}>
          <ThemeProvider theme={mainTheme}>
            <CssBaseline/>
            <Component {...pageProps} />
          </ThemeProvider>
      </Provider>
    </>
  )
}

export default App;