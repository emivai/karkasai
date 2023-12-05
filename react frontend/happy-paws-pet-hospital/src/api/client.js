import axios from 'axios';

const urlBase = 'https://localhost:7294/';

const client = {
  get: async (url) => await axios.get(urlBase + url),
  post: async (url, data) => await axios.post(urlBase + url, data),
  put: async (url, data) => await axios.put(urlBase + url, data),
  delete: async (url) => await axios.delete(urlBase + url),
};

axios.interceptors.request.use((config) => {
  const token =  localStorage.getItem('token');
  config.headers.Authorization = token ? `Bearer ${token}` : '';
  return config;
});

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    throw new Error(error?.response?.data?.message || error.message);
  }
);

export { client };