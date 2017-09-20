<template lang="pug">
el-row
    el-col(:span="6")
            el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="角色名",icon="search",v-model="roleInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(r,index) in roleInfo.roles", :key="r.id",@click="roleSelect(r.id)")                
                        | {{r.roleName}}
                el-pagination(small,layout="prev, pager, next",:total="roleInfo.page.totalCount",:page-size="roleInfo.page.pageSize")
    el-col(:span="6")
        el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名/code",icon="search",v-model="roleInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(app,index) in appInfo.apps", :key="app.id",@click="appSelect(app.id)")                
                        | {{app.appName}}
                el-pagination(small,layout="prev, pager, next",:total="appInfo.page.totalCount",:page-size="appInfo.page.pageSize")
    el-col(:span="12")
        el-tree(:data="resourceInfo.resouceTree",:props="resourceInfo.defaultProps",@node-click="")
</template>
<script>
import {roleService} from '../../../service/roleService'
import {appService} from '../../../service/appService'
import {resourceService} from '../../../service/resourceService'

export default {
    data() {
        return {
            roleInfo: {
                roles: [],
                page: {pageIndex:1,pageSize:20},
                selectedRoleId: "",
                searchCondition: ""
            },
            appInfo: {
                apps: [],
                page: {pageIndex:1,pageSize:20},
                selectedAppId: "",
                searchCondition: ""
            },
            resourceInfo: { 
                resouceTree:[],              
                defaultProps: {
                    children: 'children',
                    label: 'resourceName'                    
                },
                values: []
            }
        }
    },
    methods:{
        appSelect(appId){
            this.appInfo.selectedAppId = appId;
            this.getResourceTree()
        },
        roleSelect(roleId){
            this.roleInfo.selectedRoleId = roleId;
            this.getResourceTree()
        },
        async getResourceTree(){
            if(this.roleInfo.selectedRoleId!=="" && this.appInfo.selectedAppId!==""){
                let appResourceTree = await resourceService.getAppResource(this.appInfo.selectedAppId);
                this.resourceInfo.resouceTree =await resourceService.getAppResource(this.appInfo.selectedAppId);
                this.resourceInfo.values = await resourceService.getRoleResourceIds(this.roleInfo.selectedRoleId,this.appInfo.selectedAppId);
            }
        },
        async getRoles() {
            let pageResult = await roleService.searchRoles(this.roleInfo.searchCondition, this.roleInfo.page.pageIndex, this.roleInfo.page.pageSize);
            this.roleInfo.roles = pageResult.items;
            this.roleInfo.page = pageResult.page;
        },
        async getApps(){
            let pageResult = await appService.searchApp(
                this.appInfo.searchCondition,
                this.appInfo.page.pageIndex,
                this.appInfo.page.pageSize);
            this.appInfo.apps = pageResult.items;
            this.appInfo.page = pageResult.page;
        }
    },
    mounted:function(){
        this.getRoles();
        this.getApps();
    }
}
</script>
