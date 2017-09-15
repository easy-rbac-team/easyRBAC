<template lang="pug">
  .details
    .el-form-item
        label.el-form-item__label
            | 应用名
        div.el-form-item__content
            | {{appData.appName}}
    .el-form-item
        label.el-form-item__label
            | AppCode
        div.el-form-item__content
            | {{appData.appCode}}
    .el-form-item
        label.el-form-item__label
            | AppSecuret
        div.el-form-item__content
            | {{appData.appSecuret?appData.appSecuret:'***'}}
    .el-form-item(v-if="appData.callbackUrl!==null&&appData.callbackUrl!==''")
        label.el-form-item__label
            | 回调地址
        div.el-form-item__content
            | {{appData.callbackUrl}}
    .el-form-item
        label.el-form-item__label
            | APP描述
        div.el-form-item__content
            | {{appData.descript}}
    div
        el-button(type="success",size="small",v-show="showAppSecuret") 复制
</template>
<script>
import { appService } from '../../service/appService.ts'

export default {
    props:["appId"],
    data() {
        return {
            appData: {
                id: "",
                appName: "",
                appCode: "",
                createTime:null,
                descript:"",
                callbackUrl:"",
                appSecuret:""
            },
            showAppSecuret:false
        }
    },
    methods:{
        async getAppSecuret(){
            this.appData.appSecuret = await appService.getAppsecrete(this.appId);
        },
        async getAppInfo(){
            let result = await appService.getApp(this.appId);
            this.appData = result;
        }
    },
    mounted:function(){
        if(this.appId!=undefined&&this.appId!==""){
            this.getAppInfo();
        }
        
    },
    watch:{
        'appId' (to, from) {
            this.getAppInfo()
        }
    }
}
</script>

<style>
.details{
    margin: 10px 10px;
}
</style>
