<template lang="pug">
el-row    
    el-col(:span="8")        
        search-lst(:searchFun="getAppLst",placeholder="应用名|CODE",@itemClick="itemSelcetHandler")
            template(slot-scope="props")
                | {{props.item.appName}}
    el-col(:span="8")        
        resource-tree(:appId="selectAppId",:appName="selectAppName")
</template>

<script>
import {appService} from '../../../service/appService'
import {resourceService} from '../../../service/resourceService'
import resourceTree from './resourceTree'
import searchLst from '../../commons/searchLst'

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
            isTreeShow:false,
            selectAppId:"",
            selectAppName:"",
            pagingList:{
                items:[],
                page:{
                    totalCount:0,
                    pageSize:20,
                }
            }     
        }
    },
    methods: {        
        itemSelcetHandler(obj){
            let app = obj.item;
            this.showTree(app.id,app.appName);
        },
        showTree(appId,appName){
            this.isTreeShow = true;
            this.selectAppId = appId;
            this.selectAppName = appName;
        }, iconClickHandler() {
            this.getAppLst()
        },       
        async getAppLst(appName,pageIndex,pageSize) {
            let pageResult = await appService.searchApp(
                appName,
                pageIndex,
                pageSize);
            return pageResult;
        },
        getContent(item){
            return item.appName
        }
    },
    mounted: async function() {
        //this.getAppLst();
    },
    components: {        
        resourceTree,
        searchLst       
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

