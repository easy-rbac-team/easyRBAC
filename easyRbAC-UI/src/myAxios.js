import axios from 'axios';
import Vue from 'vue';
import Message from 'element-ui'

const customAxios = axios.create({
    // baseURL: '/api',
    // // target: 'http://10.66.4.67:8099/'
    // headers: {
    //     // Cookie: 'MOZARTSESSIONID=4cfd8b02-52a0-4520-8777-f11c5fc05a44',
    // },
    timeout: 15000,
    withCredentials: true
});

function showErro(msg) {
    Message.Message.error(msg);
}

const ssoUrl = "http://localhost:8010/sso.html?appCode=easyRBAC&from=";

customAxios.interceptors.response.use(function(response) {
    return response;
}, function(error) {
    debugger;
    if (error.response == undefined) {
        showErro('服务器连接超时')
        return;
    }
    let code = error.response.status;

    if (code >= 500) {
        let data = error.response.data;
        showErro(`服务器发生错误：${data.message}`)
    }

    if (code === 401) {
        //goTo(ssoUrl);
        window.location.href = ssoUrl + window.location.href;
    }

    if (code === 403) {
        showErro(`没有权限进行此操作`)
    }

    if (code === 400) {
        let data = error.response.data;
        showErro(`输入错误`)
    }

    // Do something with response error
    return Promise.reject(error);
});
export default customAxios;