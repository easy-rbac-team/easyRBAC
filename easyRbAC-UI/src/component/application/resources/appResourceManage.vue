<template lang="pug">
el-row    
    el-col(:span="8")
        el-card.box-card                  
            .clearfix(slot="header")                
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名|CODE",icon="search",:on-icon-click="iconClickHandler",v-model="search.appName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(u,index) in apps", :key="u.id")                
                    | {{u.appName}}                    
            el-pagination(small,layout="prev, pager, next",:total="page.totalCount",:page-size="page.pageSize")
    el-col(:span="8")         
</template>

<script>
import {appService} from '../../../service/appService'

export default {
    data() {
        return {
            search:{
                appName: "",
                pageIndex:1,
                pageSize:20
            },            
            apps: [],
            page: {}            
        }
    },
    methods: {        
        showEdit(appId){
            
        },
        showAppInfo(appId){
            
        },
        iconClickHandler() {
            this.getAppLst()
        },
        addApp() {
           
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
        }
    },
    mounted: async function() {
        this.getAppLst();
    },
    components: {        
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

