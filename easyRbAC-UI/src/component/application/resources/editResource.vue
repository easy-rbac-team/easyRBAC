<template lang="pug">
  div.add_form
    el-form(:model='form',:inline="true",:rules="rules",label-width="100px",ref="ruleForm")
        el-form-item(label='资源名',prop="resourceName")
            el-input(v-model='form.resourceName')
        el-form-item(label='资源Code',prop="resourceCode")
            el-input(v-model='form.resourceCode')
        el-form-item(label='资源参数')
            el-input(v-model='form.parameters')
        el-form-item(label='URL')
            el-input(v-model='form.url')
        el-form-item(label='图标')
            el-input(v-model='form.iconUrl')
        el-form-item(label='资源类型',prop="resourceType")
            el-select(v-model='resourcesTypes', placeholder='请资源类型',multiple)
                el-option(label='资源', value="1")
                el-option(label='菜单', value="2")
                el-option(label='公开', value="4")
        el-form-item(label='描述')
            el-input(v-model='form.describe',type="textarea")
        el-form-item()
            el-button(type="success",@click="createOne") 修改
            el-button(type="warning",@click="cancel") 取消

</template>
<script>
import { resourceService } from '../../../service/resourceService'

export default {
    props: {
        resource: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            form: {
                resourceName: "",
                resourceCode: "",
                parameters: "",
                url: "",
                iconUrl: "",
                describe: "",
                resourceType: 0
            },
            rules: {
                resourceName: [
                    { required: true, message: '请输入资源名称', trigger: 'blur' },
                    { min: 2, max: 45, message: '长度在 2 到 45 个字符', trigger: 'blur' }
                ],
                resourceCode: [
                    { required: true, message: '请输入资源Code', trigger: 'blur' },
                    { min: 2, max: 45, message: '长度在 2 到 45 个字符', trigger: 'blur' }
                ],
                parameters: [

                ],
                url: [],
                iconUrl: [],
                describe: [],
                resourceType: [
                    { required: true, trigger: 'blur' },
                    { min: 5, max: 7, message: '请选择资源类型', trigger: 'blur' }
                ]
            }
        }
    },
    computed: {
        resourcesTypes: {
            get: function() {
                let typesNum = this.form.resourceType
                let result = new Array();
                for (let i = 0; i < 3; i++) {
                    let permissionFlag = Math.pow(2, i);
                    if ((typesNum & permissionFlag) === permissionFlag) {
                        result.push(permissionFlag.toString())
                    }
                }
                return result;
            },
            set: function(val) {
                let result = 0;
                for (let item of val) {

                    result += Number(item);
                }
                this.form.resourceType = result;
            }
        }
    },
    methods: {
        async createOne() {
            let appResource = {
                applicationId: this.resource.applicationId,
                resourceName: this.form.resourceName,
                resourceCode: this.form.resourceCode,
                iconUrl: this.form.iconUrl,
                parameters: this.form.parameters,
                describe: this.form.describe,
                url: this.form.url,
                resourceType: this.form.resourceType
            };
            await resourceService.editResource(this.resource.id, appResource);
            //await resourceService.addResource(this.parentId, appResource);
            this.$emit("addResourceComplete", true);
        },
        cancel() {
            this.$emit("addResourceComplete", false);
        },
        mapToForm(){
            this.form.resourceName = this.resource.resourceName;
            this.form.resourceCode = this.resource.resourceCode;
            this.form.parameters = this.resource.parameters;
            this.form.url = this.resource.url;
            this.form.iconUrl = this.resource.iconUrl;
            this.form.describe = this.resource.describe;
            this.form.resourceType = this.resource.resourceType;
        }
    },
    watch: {
        resource: function(to, from) {
            this.mapToForm()
        }
    },
    mounted: function() {
        this.mapToForm()
    }
}
</script>
<style>

</style>
