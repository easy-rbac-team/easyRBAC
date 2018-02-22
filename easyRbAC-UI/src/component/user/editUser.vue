<template lang="pug">
  div.form-border
    el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='用户名',prop="userName")
                el-input(v-model='form.userName')                           
            el-form-item(label='真实姓名',prop="realName")
                el-input(v-model='form.realName')
            el-form-item(label='手机号',prop="mobilePhone")
                el-input(v-model='form.mobilePhone')            
            el-form-item
                el-button(type='primary', @click='onSubmit') 修改
                el-button(@click="cancel") 取消
</template>

<script>
import { userService } from "../../service/userService.ts"

export default {
    props:["userId"],
    data() {
        return {
            form: {
                userName: '',
                realName: '',
                mobilePhone: ''
            },
            rules: {
                userName: [
                    { required: true, message: '请输入用户名', trigger: 'blur' },
                    { min: 4, max: 20, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                realName: [
                    { required: true, message: '请输入真实姓名', trigger: 'blur' },
                    { min: 2, max: 20, message: '长度在 2 到 20 个字符', trigger: 'blur' }
                ],
                mobilePhone: [
                    { type: "string", message: '请输入正确手机号', trigger: 'blur', pattern: /1\d{10}/ },

                ]
            }
        }
    },
    methods: {
        onSubmit() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    userService.createUser(this.$data.form)
                } else {
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
        },
        async getUserInfo(userId) {
            let userInfo = await userService.getUserInfo(userId);
            this.form = {
                userName: userInfo.userName,
                realName: userInfo.realName,
                mobilePhone: userInfo.mobilePhone
            }
        },
        cancel() {
            this.$emit("close")
        }
    },
    watch: {
        'userId'(to, from) {           
            this.getUserInfo(to)
        }
    },
    mounted: async function() {       
        await this.getUserInfo(this.userId);
    }
}
</script>
<style>
.form-border {
    margin: 10px 10px;
    border: 1px solid #eaeefb;
    border-radius: 4px;
    transition: .2s;
    padding: 10px 10px;
}
</style>
