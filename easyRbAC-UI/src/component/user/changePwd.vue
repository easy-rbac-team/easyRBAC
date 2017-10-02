<template lang="pug">
div.form-border
    el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='密码',prop="password")
                el-input(v-model='form.password')                           
            el-form-item(label='确认密码',prop="confirmPassword")
                el-input(v-model='form.confirmPassword')           
            el-form-item
                el-button(type='primary', @click='onSubmit') 修改
                el-button(@click="cancel") 取消
</template>
<script>
import { userService } from "../../service/userService.ts"

export default {
    props: ["userId"],
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
                password: '',
                confirmPassword: '',
            },
            rules: {
                password: [
                    { required: true, message: '请输密码', trigger: 'blur' },
                    { min: 4, max: 20, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ],
                confirmPassword: [
                    { required: true, validator: passwordCompare, trigger: 'blur' },
                    { required: true, message: '请再次输入密码', trigger: 'blur' },
                    { min: 4, max: 20, message: '长度在 2 到 20 个字符', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        onSubmit() {
            this.$refs["form"].validate((valid) => {
                if (valid) {
                    userService.changePassowrd(this.userId, this.$data.form).then(() => {
                        this.$message({
                            message: '修改成功',
                            type: 'success'
                        })
                        this.$emit("closeChangePwd");
                    })
                } else {
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
        },
        cancel() {
            this.$emit("closeChangePwd")
        }
    }
}
</script>
