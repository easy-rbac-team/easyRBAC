<template lang="pug">
  .details
    .el-form-item
        label.el-form-item__label
            | 应用名
        div.el-form-item__content
            span 
                | {{appData.appName}}
    .el-form-item
        label.el-form-item__label
            | AppCode
        div.el-form-item__content
            span 
                | {{appData.appCode}}
    .el-form-item
        label.el-form-item__label
            | AppSecuret
        div.el-form-item__content           
            el-tooltip(class="item",effect="dark",content="复制",placement="top-start")
                el-button(size="mini",icon="document",type="success")
            el-tooltip(class="item",effect="dark",content="重置",placement="top-start")        
                el-button(size="mini",icon="edit",type="warning")                
    .el-form-item(v-if="appData.callbackUrl!==null&&appData.callbackUrl!==''")
        label.el-form-item__label
            | 回调地址
        div.el-form-item__content
            span 
                | {{appData.callbackUrl}}
    .el-form-item
        label.el-form-item__label
            | APP描述
        div.el-form-item__content
            span 
                | {{appData.descript}}    
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
    border: 1px solid #eaeefb;
    border-radius: 4px;
    transition: .2s;
    padding: 10px 10px;
}

.details .el-form-item{
    border-bottom: 1px solid #eaeefb;
}
</style>
