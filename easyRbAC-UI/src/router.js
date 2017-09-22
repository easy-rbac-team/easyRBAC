import Router from 'vue-router';
import userManage from './component/user/userManage.vue'
import editUser from './component/user/editUser.vue'
import roleManage from './component/role/roleManage.vue'
import appManage from './component/application/applicationManage.vue'
import appResouceManage from './component/application/resources/appResourceManage'
import roleUser from './component/role/user/roleUser'
import roleResource from './component/role/resource/roleResourceManage'
import userResource from './component/user/resource/userResourceManage'


export let routerCfg = new Router({
    routes: [{
        path: "/user",
        component: userManage,
        children: [{
            path: "edit/:userId",
            component: editUser
        }]
    }, {
        path: "/role",
        component: roleManage
    }, {
        path: "/role/roleResource",
        component: roleResource
    }, {
        path: "/application",
        component: appManage
    }, {
        path: "/app/resource",
        component: appResouceManage
    }, {
        path: "/role/user",
        component: roleUser
    }, {
        path: "/user/resource",
        component: userResource
    }]
});