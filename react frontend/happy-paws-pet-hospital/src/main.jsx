import React from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import configureStore from './store/store'
import App from './App.jsx'
import 'bootstrap/dist/css/bootstrap.min.css'
import './styles/custom.css'
import './styles/carousel.css'
import AuthProvider from './AuthProvider'

ReactDOM.createRoot(document.getElementById('root')).render(
  <Provider store={configureStore}>
    <AuthProvider>
      <React.StrictMode>
        <App />
      </React.StrictMode>
    </AuthProvider>
  </Provider>
)
