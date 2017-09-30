import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-default/index.css'
import App from './App.vue'
import VueRouter from 'vue-router'
import { routerCfg } from './router.js'
import userManage from './component/user/userManage.vue'
import axios from 'axios'
import treeMenu from './component/treeMenu.vue'
import VueClipboard from 'vue-clipboard2'
import searchLst from './component/commons/searchLst'
import appResource from './component/commons/appResource'
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                      佛祖保佑       永无BUG
//                      心外无法       法外无心

Vue.use(ElementUI)
Vue.use(VueRouter);
Vue.use(VueClipboard)


Vue.component("tree-menu", treeMenu);
Vue.component("search-lst", searchLst);
Vue.component("app-resource", appResource);

let v = new Vue({
    router: routerCfg,
    el: '#app',
    render: h => h(App)
});


axios.defaults.withCredentials = true;

axios.interceptors.response.use(function(response) {

    return response;
}, function(error) {
    debugger;
    console.log('error response!!!!!!!!!!')

    v.$message.error('服务器发生错误:' + error.message);
    // Do something with response error
    return Promise.reject(error);
});