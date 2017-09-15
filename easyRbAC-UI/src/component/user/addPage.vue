<template lang="pug">  
    div.form-border
        el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='用户名',prop="userName")
                el-input(v-model='form.userName')
            el-form-item(label='密码',prop="password")
                el-input(v-model='form.password',type="password")
            el-form-item(label='确认密码',prop="confirmPassword")
                el-input(v-model='form.confirmPassword',type="password")
            el-form-item(label='真实姓名',prop="realName")
                el-input(v-model='form.realName')
            el-form-item(label='手机号',prop="mobilePhone")
                el-input(v-model='form.mobilePhone')            
            el-form-item
                el-button(type='primary', @click='onSubmit') 立即创建
                el-button(@click="cancel") 取消
</template>
<script>
import {userService} from '../../service/userService.ts'

export default {
    data() {
        let passwordCompare = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请再次输入密码'));
            } else if (value !== this.form.password) {
                callback(new Error('两次输入密码不一致!'));
            } else {
                callback();
            }
        };
        return {
            form: {
                userName: '',
                password: '',
                confirmPassword: '',
                realName: '',
                mobilePhone: ''
            },
            rules: {
                userName: [
                    { required: true, message: '请输入用户名', trigger: 'blur' },
                    { min: 4, max: 20, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '请输入密码', trigger: 'blur' },
                    { min: 8, message: '最短需要8个字符', trigger: 'blur' }
                ],
                confirmPassword: [
                    { required: true, validator: passwordCompare, trigger: 'blur' }
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
        async onSubmit() {
            this.$refs["form"].validate(async (valid) => {
                if (valid) {
                    await userService.createUser(this.$data.form)
                    this.$emit("addedUserFinish",true)
                } else {                    
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
        },
        cancel(){
            this.$emit("addedUserFinish",false)
        }
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

.form-border input[type="text"][type="password"] {
    width: 250px;
}
</style>
