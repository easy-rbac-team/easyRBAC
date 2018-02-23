<template lang="pug">
el-row
    el-col(:span="6")
        search-lst(:searchFun="getUsers",placeholder="用户名",@itemClick="userSelect")
            template(slot-scope="props")
                | {{props.item.userName}}
    app-resource(:checkedKeys="checkedKeys",:disabledKeys="disabledKeys",@appSelect="appSelect",@setScope="saveChange")
</template>
<script>
import { userService } from '../../../service/userService'
import { appService } from '../../../service/appService'
import { resourceService } from '../../../service/resourceService'
import searchLst from '../../commons/searchLst'

export default {
    data() {
        return {            
            selectedAppId:"",
            selectedUserId:"",
            checkedKeys:[],
            disabledKeys:[]
        }
    },
    methods: {
        appSelect(arg) {           
             
            let appId = arg.item.id;
            this.selectedAppId = appId;
            this.getResourceTree()
        },
        userSelect(arg) {
            let userId = arg.item.id
            this.selectedUserId = userId;
            this.getResourceTree()
        },
        async getResourceTree() {
            if(this.selectedUserId === "" || this.selectedAppId === "")
            {
                return;
            }
            let userAllResources = await userService.getUserResourceIds(this.selectedUserId, this.selectedAppId)
            let allChecked = userAllResources.userResource.concat(userAllResources.roleResource);
            this.checkedKeys = allChecked;
            
            this.disabledKeys = userAllResources.roleResource;
        },
        async getUsers(searchCondition,pageIndex,pageSize) {
            let pageResult = await userService.getUsers(searchCondition,pageIndex,pageSize);
            return pageResult;
        },
        async getApps(searchCondition,pageIndex,pageSize) {
            let pageResult = await appService.searchApp(searchCondition,pageIndex,pageSize)
            return pageResult;  
        },
        async saveChange(nodes){
            let userId = this.selectedUserId;
            let appId = this.selectedAppId;            
            await userService.changeUserResources(userId, appId, nodes);
            this.$message({ type: "success", message: "设置成功！" })
        }
    },
    mounted: function() {        
    },
    components:{
        searchLst
    }
}
</script>


<style scoped>

</style>
