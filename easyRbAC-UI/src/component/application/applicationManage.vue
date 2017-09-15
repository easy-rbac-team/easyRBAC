<template lang="pug">
  el-row    
    el-col(:span="8")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addApp") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名|CODE",icon="search",:on-icon-click="iconClickHandler",v-model="search.appName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(u,index) in apps", :key="u.id")                
                    | {{u.appName}}
                    el-button-group
                        el-button(icon="delete",size="mini",type="danger",@click="deleteApp(index,u.id)")
                        el-button(icon="edit",size="mini",type="warning",@click="editApp(u.id)")
                        el-button(icon="information",size="mini",type="info",@click="showAppInfo(u.id)")
            el-pagination(small,layout="prev, pager, next",:total="page.totalCount",:page-size="page.pageSize")
    el-col(:span="8")
         add-app(v-if="showAddApp",v-on:showFinish="addedAppHandle")
         app-info(v-if="appInfoOp.showAppInfo",:appId="appInfoOp.appId")
</template>

<script>
import { appService } from '../../service/appService.ts'
import appInfo from './appInfo.vue'
import addApp from './addApp.vue'

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
            }
        }
    },
    methods: {
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
        },
        iconClickHandler() {
            
        },
        addApp() {
           this.showAddApp = true;
           this.appInfoOp.showAddApp = false;
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
        },
        async deleteApp(index, AppId) {
            this.$confirm('确定删除此应用?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(async () => {
                //await AppService.deleteApp(AppId);
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
        appInfo
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

