import axios from "axios";

const API_BASE_URL = 'https://dtaquito-backend-web-app.azurewebsites.net/api/v1';
const http = axios.create({
    baseURL: API_BASE_URL, headers: {
        'Content-type': 'application/json',
    }
});

export default http;