<template lang="pug">
      div.form-border
        el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='角色名',prop="roleName")
                el-input(v-model='form.roleName',:disabled="readOnly")         
            el-form-item(label="描述",prop="descript")
                el-input(v-model="form.descript",type="textarea",:disabled="readOnly")   
            el-form-item(v-if="!readOnly")
                el-button(type='primary', @click='onSubmit') 立即修改
                el-button(@click="cancel") 取消
</template>

<script>
import { roleService } from '../../service/roleService.ts'

export default {
    props: {
        roleId:{
            type:String,
            required:true
        },
        readOnly:{
            type:Boolean,
            default:false
        }
    },
    data() {
        return {
            form: {
                roleName: "",
                descript: ""
            },
            rules: {
                roleName: [
                    { required: true, message: "请输入角色名", trigger: "blur" },
                    { min: 2, max: 20, message: '长度在 4 到 20 个字符', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        onSubmit() {
            this.$refs["form"].validate(async (valid) => {
                if (valid) {
                    await roleService.editRole(this.roleId,this.$data.form)
                    this.$emit("editFinish", true)
                } else {
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
        },
        cancel() {
            this.$emit("editFinish", false)
        }
    },
    mounted:async function(){
        let result = await roleService.getRole(this.roleId)
        this.form = result;
    },
    watch:{
        'roleId' (to, from) {
            roleService.getRole(this.roleId).then(result=>{
                this.form = result;
            })            
        }
    }
}
</script>
<style>
.form-border {
    margin: 10px;
}
</style>

