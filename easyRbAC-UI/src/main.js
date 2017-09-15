import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import App from './App.vue'
import VueRouter from 'vue-router'
import { routerCfg } from './router.js'
import userManage from './component/user/userManage.vue'
import axios from 'axios'
import treeMenu from './component/treeMenu.vue'
import VueClipboard from 'vue-clipboard'

Vue.use(ElementUI)
Vue.use(VueRouter);
Vue.use(VueClipboard)

Vue.component("tree-menu", treeMenu);
let v = new Vue({
    router: routerCfg,
    el: '#app',
    render: h => h(App)
});



axios.interceptors.response.use(function(response) {

    return response;
}, function(error) {
    console.log('error response!!!!!!!!!!')

    v.$message.error('服务器发生错误:' + error.message);
    // Do something with response error
    return Promise.reject(error);
});