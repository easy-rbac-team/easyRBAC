import axios from 'axios';

const customAxios = axios.create({
    // baseURL: '/api',
    // // target: 'http://10.66.4.67:8099/'
    // headers: {
    //     // Cookie: 'MOZARTSESSIONID=4cfd8b02-52a0-4520-8777-f11c5fc05a44',
    // },
    timeout: 15000,
    withCredentials: true
});

axios.interceptors.response.use(function(response) {
    return response;
}, function(error) {
    console.log('error response!!!!!!!!!!')
    v.$message.error('服务器发生错误:' + error.message);
    // Do something with response error
    return Promise.reject(error);
});
export default customAxios;