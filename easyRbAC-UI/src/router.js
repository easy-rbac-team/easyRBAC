import Router from 'vue-router';
import userManage from './component/user/userManage.vue'
import editUser from './component/user/editUser.vue'
import roleManage from './component/role/roleManage.vue'
import appManage from './component/application/applicationManage.vue'

export let routerCfg = new Router({
    routes: [{
        path: "/user",
        component: userManage,
        children: [{
            path: "edit/:userId",
            component: editUser
        }]
    },{
        path:"/role",
        component:roleManage
    },{
        path:"/application",
        component:appManage
    }]
});