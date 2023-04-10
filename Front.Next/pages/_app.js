import '@/styles/globals.css'
import { ThemeProvider } from '@emotion/react';
import mainTheme from '@/themes/mainTheme';
import { CssBaseline } from '@mui/material';

export default function App({ Component, pageProps }) {
  return (
    <ThemeProvider theme={mainTheme}>
      <CssBaseline/>
      <Component {...pageProps} />
    </ThemeProvider>
  )
}
