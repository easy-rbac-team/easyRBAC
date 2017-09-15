<template lang="pug">
      div.form-border
        el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='角色名',prop="roleName")
                el-input(v-model='form.roleName')         
            el-form-item(label="描述",prop="descript")
                el-input(v-model="form.descript",type="textarea")   
            el-form-item
                el-button(type='primary', @click='onSubmit') 立即创建
                el-button(@click="cancel") 取消
</template>

<script>
import {roleService} from '../../service/roleService.ts'

export default {
  data(){
      return{
          form:{
              roleName:"",
              descript:""
          },
          rules:{
              roleName:[
                  {required:true,message:"请输入角色名",trigger:"blur"},
                  { min: 2, max: 20, message: '长度在 4 到 20 个字符', trigger: 'blur' }
              ]
          }
      }
  },
  methods:{
      onSubmit(){
           this.$refs["form"].validate(async (valid) => {
                if (valid) {
                    await roleService.createRole(this.$data.form)
                    this.$emit("showFinish",true)
                } else {                    
                    this.$message({
                        message: '表单验证不通过',
                        type: 'warning'
                    })
                }
            });
      },
      cancel(){
          this.$emit("showFinish",false)
      }
  }
}
</script>
<style>
.form-border{
    margin: 10px;
}
</style>

