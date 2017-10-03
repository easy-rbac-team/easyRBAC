<template lang="pug">
  el-tabs
    el-tab-pane(label="用户基本信息",name="baseInfo")
        .container
            .item
                label 用户名
                span {{userInfo.userName}}
            .item
                label 真实姓名
                span {{userInfo.realName}}
            .item
                label 手机
                span {{userInfo.mobilePhone}}
    el-tab-pane(label="用户角色",name="roleInfo")
        .container
            .item(v-for="(r,i) in roles")
                {{r.roleName}}
    el-tab-pane(label="资源信息",name="resourceInfo")
        .container
            
</template>
<script>
export default {
    props: {
        userInfo:{
            type:Object,
            required: true
        }
    },
    data() {
        return {
            activeName: "baseInfo"
        }
    }
}
</script>
