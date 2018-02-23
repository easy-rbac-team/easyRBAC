<template lang="pug">
el-row    
    el-col(:span="8")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addApp") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名|CODE",suffix-icon="el-icon-search",v-model="search.appName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(u,index) in apps", :key="u.id")                
                    | {{u.appName}}
                    el-button-group
                        el-button(icon="el-icon-delete",size="mini",type="danger",@click="deleteApp(index,u.id)")
                        el-button(icon="el-icon-edit",size="mini",type="warning",@click="showEdit(u.id)")
                        el-button(icon="el-icon-info",size="mini",type="info",@click="showAppInfo(u.id)")
            el-pagination(small,layout="prev, pager, next",:total="page.totalCount",:page-size="page.pageSize")
    el-col(:span="8")
         add-app(v-if="showAddApp",v-on:showFinish="addedAppHandle")
         app-info(v-if="appInfoOp.showAppInfo",:appId="appInfoOp.appId",v-on:showFinish="addedAppHandle")
         edit-app(v-if="editAppOp.showEdit",:appId="editAppOp.appId",v-on:showFinish="addedAppHandle")
</template>

<script>
import { appService } from '../../service/appService.ts'
import appInfo from './appInfo.vue'
import addApp from './addApp.vue'
import editApp from "./editApp"

export default {
    data() {
        return {
            search:{
                appName: "",
                pageIndex:1,
                pageSize:20
            },            
            apps: [],
            page: {},
            showAddApp: false,
            appInfoOp:{
                showAppInfo : false,
                appId:""
            },
            editAppOp:{
                showEdit:false,
                appId:""
            }
        }
    },
    methods: {        
        showEdit(appId){
            let oldApp = this.editAppOp.appId;
            this.editAppOp.appId = appId;
            if(oldApp == appId){
                this.editAppOp.appId = "",
                this.editAppOp.showEdit = false;
            }else{
                this.editAppOp.showEdit = true;
            }
            this.appInfoOp.showAppInfo = false;
            this.showAddApp = false;
        },
        showAppInfo(appId){
            let oldApp = this.appInfoOp.appId;
            this.appInfoOp.appId = appId;

            if(oldApp == appId){
                this.appInfoOp.showAppInfo= false;
                this.appInfoOp.appId = undefined;
            }else{
                this.appInfoOp.showAppInfo = true;                
            }          
            this.showAddApp = false;
            this.editAppOp.showEdit = false;
        },
        iconClickHandler() {
            this.getAppLst()
        },
        addApp() {
           this.showAddApp = true;
           this.appInfoOp.showAppInfo = false;
           this.editAppOp.showEdit = false;
        },
        editApp(AppId) {
            
        },
        async getAppLst() {
            let pageResult = await appService.searchApp(
                this.search.appName,
                this.search.pageIndex,
                this.search.pageSize);
            this.apps = pageResult.items;
            this.page = pageResult.page;
        },
        addedAppHandle(refresh) {            
            if(refresh){
                this.getAppLst()
            }
            this.showAddApp = false;
            this.editAppOp.showEdit = false;
            this.appInfoOp.showAppInfo = false;
        },
        async deleteApp(index, appId) {
            this.$confirm('确定删除此应用?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(async () => {
                await appService.deleteApp(appId);
                this.apps.splice(index, 1)
                this.$message({
                    type: 'success',
                    message: '删除成功!'
                });
            }).catch();
        }
    },
    mounted: async function() {
        this.getAppLst();
    },
    components: {
        addApp,
        appInfo,
        editApp
    }
}
</script>

<style scoped>
.text {
    font-size: 14px;
}

.item {
    padding: 8px 0;
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

.el-input {
    width: 180px;
}

.el-button-group {
    margin-left: 10px;
}
</style>

