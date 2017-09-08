import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import App from './App.vue'
import VueRouter from 'vue-router'
import routerCfg from './router.js'
import userManage from './component/user/userManage.vue'
import axios from 'axios'

Vue.use(ElementUI)
Vue.use(VueRouter);

const router = new VueRouter({
    routes: [{
        path: '/user',
        component: userManage
    }]
})


let v = new Vue({
    router,
    el: '#app',
    render: h => h(App)
});

axios.interceptors.response.use(function(response) {
    // if (!response.data.success) {
    //     v.$message.error(`发生业务错误：Code:${response.data.code};Msg:${response.data.msg}`);
    // }
    // Do something with response data
    return response;
}, function(error) {
    console.log('error response!!!!!!!!!!')
    v.$message.error('服务器发生错误:' + error.message);
    // Do something with response error
    return Promise.reject(error);
});