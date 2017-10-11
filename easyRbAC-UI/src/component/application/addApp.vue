<template lang="pug">
div.form-border
        el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='应用名',prop="appName")
                el-input(v-model='form.appName')         
            el-form-item(label='应用代码',prop="appCode")
                el-input(v-model='form.appCode')     
            el-form-item(label="启用SSO",prop="enableSSO")
                el-checkbox(v-model="form.enableSSO",style="float:left")  
            el-form-item(v-show="form.enableSSO",label='回调配置',prop="callbackUrl")
                el-col(:span="11")
                    el-select(v-model='form.callbackType', placeholder='SSO回调方式')
                        el-option(label='jsonp', value="1")
                        el-option(label='CORS', value="2")
                        el-option(label='Redirect', value="4")
                el-col(:span="11")
                    el-input(v-model='form.callbackUrl')                
            el-form-item(label="描述",prop="descript")
                el-input(v-model="form.descript",type="textarea")   
            el-form-item
                el-button(type='primary', @click='onSubmit') 立即创建
                el-button(@click="cancel") 取消
</template>

<script>
import { appService } from '../../service/appService.ts'

export default {
    data() {
        return {
            form: {
                appName:"",
                appCode:"",
                descript:"",
                enableSSO:false,
                callbackUrl:"",
                callbackType:""
            },
            rules: {
                appName: [
                    { required: true, message: "请输入应用名", trigger: "blur" },
                    { min: 5, max: 40, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                appCode:[
                    { required: true, message: "请输入应用代码", trigger: "blur" },
                    { min: 5, max: 40, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                descript:[
                    {max: 150, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                callbackUrl:[
                    {max: 150, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        onSubmit() {
            this.$refs["form"].validate(async (valid) => {
                if (valid) {                    
                    await appService.createApp(this.$data.form)
                    this.$emit("showFinish", true)
                } else {
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
        },
        cancel() {
            this.$emit("showFinish", false)
        }
    }
}
</script>
<style>
.form-border {
    margin: 10px;
}
</style>

