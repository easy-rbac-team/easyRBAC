<template lang="pug">
el-row
    el-col(:span="6")
        search-lst(:searchFun="getRoles",placeholder="角色名",@itemClick="roleSelect")
            template(slot-scope="props")
                | {{props.item.roleName}}
    app-resource(:checkedKeys="checkedKeys",@appSelect="appSelect",@setScope="setRoleResource")
</template>
<script>
import {roleService} from '../../../service/roleService'
import {appService} from '../../../service/appService'
import {resourceService} from '../../../service/resourceService'

export default {
    data() {
        return {            
            selectedRoleId:"",
            selectedAppId:"",
            checkedKeys:[]
        }
    },    
    methods:{
        appSelect(arg){
            let appId = arg.item.id;
            this.selectedAppId = appId;
            this.getResourceTree()
        },
        roleSelect(arg){
            let roleId = arg.item.id
            this.selectedRoleId = roleId;
            this.getResourceTree()
        },
        async getResourceTree(){
            if(this.selectedRoleId!=="" && this.selectedAppId!==""){
                let roleResources = await resourceService.getRoleResourceIds(this.selectedRoleId,this.selectedAppId)
                this.checkedKeys = roleResources
            }
        },
        async getRoles(searchCondition,pageIndex,pageSize) {
            let pageResult = await roleService.searchRoles(searchCondition,pageIndex,pageSize);
            return pageResult;
        },
        async setRoleResource(nodes){
            let roleId = this.selectedRoleId;
            let keys = nodes.map(x=>x.id);
            await resourceService.changeRoleResources(roleId,this.selectedAppId,keys);
            this.$message({type:"success",message:"设置成功！"})           
        }
    },
    mounted:function(){
        // this.getRoles();
        // this.getApps();
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
