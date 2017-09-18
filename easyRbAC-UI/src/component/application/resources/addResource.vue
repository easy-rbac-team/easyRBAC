<template lang="pug">
  div.add_form
    el-form(:model='form',:inline="true",label-width="100px")
        el-form-item(label='资源名')
            el-input(v-model='form.resourceName')
        el-form-item(label='资源Code')
            el-input(v-model='form.resourceCode')
        el-form-item(label='资源参数')
            el-input(v-model='form.parameters')
        el-form-item(label='URL')
            el-input(v-model='form.url')
        el-form-item(label='图标')
            el-input(v-model='form.iconUrl')
        el-form-item(label='活动区域')
            el-select(v-model='form.resourceType', placeholder='请资源类型')
                el-option(label='资源', value="1")
                el-option(label='菜单', value="2")
                el-option(label='公开', value="4")
        el-form-item(label='描述')
            el-input(v-model='form.describe',type="textarea")
        el-form-item()
            el-button(type="success",@click="createOne") 添加
            el-button(type="warning",@click="cancel") 取消

</template>
<script>
import {resourceService} from '../../../service/resourceService'

export default {
    props: ["parentId","appId"],
    data() {
        return {
            form: {
                isPublic: false,
                resourceName: "",
                resourceCode: "",
                parameters: "",
                url:"",
                iconUrl: "",
                describe:"",
                resourceType:"1"
            }
        }
    },
    methods:{
        async createOne(){
            let appResource = {
                applicationId : this.appId,
                resourceName : this.form.resourceName,
                resourceCode : this.form.resourceCode,
                iconUrl : this.form.iconUrl,
                parameters:this.form.parameters,
                describe : this.form.describe,
                url:this.form.url,
                resourceType : this.form.resourceType
            };
            await resourceService.addResource(this.parentId,appResource);
            this.$emit("addResourceComplete",true);
        },
        cancel(){
            this.$emit("addResourceComplete",false);
        }
    }
}
</script>
<style>
.add_form{
   
}
</style>
