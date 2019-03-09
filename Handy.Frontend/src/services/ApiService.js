import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:8443/api',
    withCredentials: false,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
    },
    timeout: 8000
});

export default api;