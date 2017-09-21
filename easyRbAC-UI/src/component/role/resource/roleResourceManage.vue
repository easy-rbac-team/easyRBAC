<template lang="pug">
el-row
    el-col(:span="6")
            el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="角色名",icon="search",v-model="roleInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(r,index) in roleInfo.roles", :key="r.id",@click="roleSelect(r.id)",v-bind:class="{selected:r.id===roleInfo.selectedRoleId}")                
                        | {{r.roleName}}
                el-pagination(small,layout="prev, pager, next",:total="roleInfo.page.totalCount",:page-size="roleInfo.page.pageSize")
    el-col(:span="6")
        el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名/code",icon="search",v-model="roleInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(app,index) in appInfo.apps", :key="app.id",@click="appSelect(app.id)",v-bind:class="{selected:app.id===appInfo.selectedAppId}")                
                        | {{app.appName}}
                el-pagination(small,layout="prev, pager, next",:total="appInfo.page.totalCount",:page-size="appInfo.page.pageSize")
    el-col(:span="12")
        el-tree(:data="resourceInfo.resouceTree",
                :props="resourceInfo.defaultProps",
                node-key="id",show-checkbox,default-expand-all,
                :check-strictly="true",ref="tree",@node-click="")
        el-button(type="success",@click="setRoleResource") 保存
        el-button(@click="cancelSetResouce") 还原
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
                }               
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
                this.resourceInfo.resouceTree =await resourceService.getAppResource(this.appInfo.selectedAppId);
                let roleResources = await resourceService.getRoleResourceIds(this.roleInfo.selectedRoleId,this.appInfo.selectedAppId)
                this.$refs.tree.setCheckedKeys(roleResources);
                this.keys_temp = roleResources;
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
        },
        async setRoleResource(){
            let roleId = this.roleInfo.selectedRoleId;
            let resourceIdLst = this.$refs.tree.getCheckedKeys();
            await resourceService.changeRoleResources(roleId,resourceIdLst);
            this.$message({type:"success",message:"设置成功！"})
            this.keys_temp = resourceIdLst;
        },
        cancelSetResouce(){
            this.$refs.tree.setCheckedKeys(this.keys_temp);
        }
    },
    mounted:function(){
        this.getRoles();
        this.getApps();
    }
}
</script>


<style scoped>
.text {
    font-size: 14px;
}

.item {
    padding: 8px 0;
    text-align: center;
}

.item:hover{
    cursor: pointer;
    background:#D3DCE6;
}

.selected{
    background:#E5E9F2;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}

.clearfix:after {
    clear: both
}

.box-card {
    width: 300px;
    margin: 10px 10px;
}
</style>
