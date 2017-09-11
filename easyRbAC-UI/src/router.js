import Router from 'vue-router';
import userManage from './component/user/userManage.vue'
import editUser from './component/user/editUser.vue'

export let routerCfg = new Router({
    routes: [{
        path: "/user",
        component: userManage,
        children: [{
            path: "edit/:userId",
            component: editUser
        }]
    }]
});