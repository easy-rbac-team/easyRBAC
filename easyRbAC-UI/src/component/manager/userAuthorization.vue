<template lang="pug">
  el-row
    el-col(:span="6")
        search-lst(:searchFun="getUsers",placeholder="用户名",@itemClick="userSelect")
            template(slot-scope="props")
                | {{props.item.userName}}
    el-col(:span="6")
        el-card.box-card.container  
            .text.item(v-for="(r,index) in appAndResources", :key="r.appId",@click="appSelect(index,r)",v-bind:class="{selected:index===selectedIndex}")                
                | {{r.appName}}
    el-col(:span="8")
        el-tree(:data="resourceTree",
                :props="defaultProps",
                node-key="id",show-checkbox,default-expand-all,
                :check-strictly="true",ref="tree")
        el-button(type="success",@click="saveChange") 保存
</template>

<script>
import {managerScopeService} from '../../service/managerScopeService'
import { userService } from '../../service/userService'
import searchLst from '../commons/searchLst'
import {resourceService} from '../../service/resourceService'

export default {
    data(){
        return {
            selectedIndex:-1,
            selectUserId:-1,
            selectAppId:-1,
            appAndResources:[],
            resourceTree:[],
            defaultProps: {
                children: 'children',
                label: 'resourceName',
                disabled: 'disabled'
            }
        }
    },
    methods:{
        async getResources(){
            let result = await managerScopeService.getManagedResources();
            this.appAndResources = result;
        },
        async getUsers(searchCondition, pageIndex, pageSize) {
            let pageResult = await userService.getUsers(searchCondition, pageIndex, pageSize);
            return pageResult;
        },
        userSelect(arg){
            let user = arg.item 
            this.selectUserId = user.id;
            if(this.selectedIndex!==-1){
                this.getUserResourceIds(user.id,this.appAndResources[this.selectedIndex].appId);
            }
        },
        appSelect(index,app){
            this.selectedIndex = index;            
            this.selectAppId = app.appId;
            this.resourceTree = app.appResouces;             
            if(this.selectUserId !==-1){
                this.getUserResourceIds(this.selectUserId,this.selectAppId)
            }          
        },
        async getUserResourceIds(userId,appId){
            let userResources = await userService.getUserResourceIds(userId,appId);
            let tree = this.$refs.tree;
            let allCheckedIds = userResources.userResource.concat(userResources.roleResource);
            
            resourceService.setResourceDisable(this.resourceTree,userResources.roleResource);
            this.$nextTick((v)=>{
                tree.setCheckedKeys(allCheckedIds)
            })
        },
        async saveChange(){
            let tree = this.$refs.tree;
            let checkedNodes = tree.getCheckedNodes();
            let selectedIds = checkedNodes.filter(x=>!x.disabled).map(x=>x.id);
            await managerScopeService.changeUserResources(this.selectUserId,this.selectAppId,selectedIds);
            this.$message({type:"success",message:"修改成功"})
        }
    },
    mounted:function(){
        this.getResources()
    },
    components:{
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
    text-align: center;
}

.item:hover {
    cursor: pointer;
    background: #D3DCE6;
}

.selected {
    background: #E5E9F2;
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
    width: 280px;
    margin: 10px 10px;
}
.page-container{
    text-align: center;
}
</style>
