import '@/styles/globals.css'
import { ThemeProvider } from '@emotion/react';
import mainTheme from '@/themes/mainTheme';
import { CssBaseline } from '@mui/material';
import { SessionProvider } from 'next-auth/react'
// process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0;

function App({ Component, pageProps: { session, ...pageProps } }) {
  return (
    <>
      <ThemeProvider theme={mainTheme}>
        <SessionProvider session={session}>
          <CssBaseline />
          <Component {...pageProps} />
        </SessionProvider>
      </ThemeProvider>
    </>
  )
}

export default App;