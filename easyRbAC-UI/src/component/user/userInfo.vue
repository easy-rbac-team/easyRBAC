<template lang="pug">
  el-tabs(v-model="activeName")
    el-tab-pane(label="用户基本信息",name="baseInfo")
        .container
            .item
                label 用户名：
                span {{userInfo.userName}}
            .item
                label 真实姓名：
                span {{userInfo.realName}}
            .item
                label 手机：
                span {{userInfo.mobilePhone}}
    el-tab-pane(label="用户角色资源",name="roleInfo")
        .container
            app-resource(:checkedKeys="checkedKeys",:readOnly="true",@appSelect="appSelect")
</template>
<script>
import appResource from '../commons/appResource'
import {userService} from '../../service/userService'

export default {
    props: {
        userInfo: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            activeName: "baseInfo",
            checkedKeys: [],
            selectedAppId: ""
        }
    },
    methods: {
        async getResourceTree() {
            if (this.selectedAppId === "") {
                return;
            }
            let userAllResources = await userService.getUserResourceIds(this.userId, this.selectedAppId)
            let allChecked = userAllResources.userResource.concat(userAllResources.roleResource);
            this.checkedKeys = allChecked;
        },
        appSelect(arg) {
            let appId = arg.item.id;
            this.selectedAppId = appId;
            this.getResourceTree()
        }
    },
    computed: {
        userId: function() {
            return this.userInfo.id;
        }
    },
    comments: {
        appResource
    },
    watch:{
        userId:function(to,from){
            this.getResourceTree();
        }
    }
}
</script>
