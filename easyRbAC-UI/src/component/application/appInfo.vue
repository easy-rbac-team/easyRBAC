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
        div.el-form-item__content(v-show="appSecuret")      
            el-tooltip(class="item",effect="dark",content="复制",placement="top-start")
                el-button(size="mini",icon="document",type="success",v-clipboard:copy="appSecuret",v-clipboard:success="onCopy",v-clipboard:error="onError")
            el-tooltip(class="item",effect="dark",content="重置",placement="top-start")        
                el-button(size="mini",icon="edit",type="warning",@click="changeAppSecret")      
        div.el-form-item__content(v-show="!appSecuret")
            el-button(size="mini",@click="getAppSecuret")  获取                  
    .el-form-item(v-if="appData.callbackUrl!==null&&appData.callbackUrl!==''")
        label.el-form-item__label
            | 回调地址
        div.el-form-item__content
            span 
                | {{appData.callbackUrl}}
    .el-form-item(v-if="appData.callbackUrl!==null&&appData.callbackUrl!==''")
        label.el-form-item__label
            | 回调方式
        div.el-form-item__content
            span 
                | {{callbackTypeStr}}
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
    props: ["appId"],
    data() {
        return {
            appData: {
                id: "",
                appName: "",
                appCode: "",
                createTime: null,
                descript: "",
                callbackUrl: "",
                callbackType:""
            },
            showAppSecuret: false,
            appSecuret: ""
        }
    },
    computed:{
        callbackTypeStr:function(){
            switch(this.appData.callbackType){
                case 0:
                    return 'Null';
                case 1:
                    return 'jsonp';
                case 2:
                    return 'CORS';
                case 4: 
                    return 'Redirect'
            }
        }
    },
    methods: {
        async getAppSecuret() {
            let result = await appService.getAppsecrete(this.appId);
            this.appSecuret = result;
        },
        async getAppInfo() {
            let result = await appService.getApp(this.appId);
            this.appData = result;
            this.appData.appSecuret = '';
        },
        onCopy() {
            this.$message("复制成功")
        },
        onError() {
            this.$message("复制失败")
        },
        async changeAppSecret() {

            this.$confirm('确定删除此应用?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(async () => {
                await appService.changeAppSecret(this.appId);
                this.appSecuret = undefined;
                this.apps.splice(index, 1)
                this.$message({
                    type: 'success',
                    message: '删除成功!'
                });
            }).catch();
        }
    },
    mounted: function() {
        if (this.appId != undefined && this.appId !== "") {
            this.getAppInfo();
        }

    },
    watch: {
        'appId'(to, from) {
            this.getAppInfo()
            this.appSecuret = undefined
        }
    }
}
</script>

<style>
.details {
    margin: 10px 10px;
    border: 1px solid #eaeefb;
    border-radius: 4px;
    transition: .2s;
    padding: 10px 10px;
}

.details .el-form-item {
    border-bottom: 1px solid #eaeefb;
}
</style>
