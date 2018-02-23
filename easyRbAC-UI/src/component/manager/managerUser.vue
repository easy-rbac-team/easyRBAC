<template lang="pug">
    el-row
        el-col(:span="6")   
            search-lst(:searchFun="getUsers",placeholder="用户名",@itemClick="userClickHandler")
                template(slot-scope="props")
                    | {{props.item.userName}}
        app-resource(@appSelect="appSelectHandler",:checkedKeys="userSelectedId",@setScope="saveHandler")
</template>

<script>
import {userService} from '../../service/userService'
import { resourceService } from '../../service/resourceService'
import {managerScopeService} from '../../service/managerScopeService'
import appResource from '../commons/appResource'

export default {
    data() {
        return {
            selectedUserId: "",
            selectedAppId: "",
            userSelectedId: []
        }
    },
    methods: {
        async getUsers(searchCondition, pageIndex, pageSize) {
            let pageResult = await userService.getUsers(searchCondition, pageIndex, pageSize);
            return pageResult;
        },
        userClickHandler(arg) {
            let user = arg.item;
            //await userService.getUserResourceIds(user.id,)
            this.selectedUserId = user.id;
            this.getSelectdId()
        },
        appSelectHandler(arg) {
            let app = arg.item;
            this.selectedAppId = app.id;
            this.getSelectdId()
        },
        async getSelectdId() {
            if (this.selectedUserId !== "" && this.selectedAppId !== "") {
                let userResoueceIds = await managerScopeService.getManagedScopeIds(this.selectedUserId, this.selectedAppId);
                debugger;
                this.userSelectedId = userResoueceIds;
            }
        },
        async saveHandler(checkedNodes){
            let resourceIds = checkedNodes.map(x=>x.id);
            debugger;
            await managerScopeService.setManageScopeIds(this.selectedUserId,this.selectedAppId,resourceIds);
            this.$message("设置成功！")
        }
    },
    components: {
        appResource
    }
}
</script>

<style>

</style>
