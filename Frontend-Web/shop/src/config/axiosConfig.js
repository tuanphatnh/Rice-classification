import axios from 'axios';
export const instance = axios.create({
    baseURL: 'https://localhost:44352/api',
    timeout: 10000,
    headers: {'X-Custom-Header': 'foobar'}
  }); 