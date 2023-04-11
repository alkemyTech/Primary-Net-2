import '@/styles/globals.css'
import { ThemeProvider } from '@emotion/react';
import mainTheme from '@/themes/mainTheme';
import { CssBaseline } from '@mui/material';
import { Provider } from 'react-redux';
import { store } from '@/store';

function App({ Component, pageProps }) {
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