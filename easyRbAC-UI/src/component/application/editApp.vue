<template lang="pug">
div.form-border
        el-form(ref='form', :model='form',:rules="rules", label-width='80px')
            el-form-item(label='应用名',prop="appName")
                el-input(v-model='form.appName')         
            el-form-item(label='应用代码',prop="appCode")
                el-input(v-model='form.appCode')     
            el-form-item(label="启用SSO",prop="enable")
                el-checkbox(v-model="form.enableSSO",style="float:left")  
            el-form-item(v-show="form.enableSSO",label='回调配置',v-for="(item,ix) in form.callbackConfigs",:key="item.id")
                el-autocomplete(:span="20",:fetch-suggestions="querySearchAsync",v-model="item.enviroment", placeholder='环境',:key="item.id")
                el-button(type="danger",@click="removeConfig(ix)") 删除
                el-col(:span="20")
                    el-select(v-model='item.callbackType', placeholder='SSO回调方式')
                        el-option(label='jsonp', value="1")
                        el-option(label='CORS', value="2")
                        el-option(label='Redirect', value="4")
                el-col(:span="20")
                    el-input(v-model='item.callbackUrl', placeholder='回调URL')                     
                el-col(:span="20")
                    el-input(v-model="item.remark",placeholder="描述")
                el-col(:span="20" v-if="ix===(form.callbackConfigs.length-1)")
                    el-button(type="success",@click="addCallbackConfig") 添加
            el-form-item
                el-button(type='primary', @click='onSubmit') 立即更新
                el-button(@click="cancel") 取消
</template>

<script>
import { appService } from "../../service/appService.ts";

export default {
  props: ["appId"],
  data() {
    return {
      defaultEnviroments: [
        { value: "prod" },
        { value: "dev" },
        { value: "local" }
      ],
      form: {
        appName: "",
        appCode: "",
        descript: "",
        enable: false,
        callbackConfigs: []
      },
      rules: {
        appName: [
          { required: true, message: "请输入应用名", trigger: "blur" },
          { min: 5, max: 40, message: "长度在 4 到 20 个字符", trigger: "blur" }
        ],
        appCode: [
          { required: true, message: "请输入应用代码", trigger: "blur" },
          { min: 5, max: 40, message: "长度在 4 到 20 个字符", trigger: "blur" }
        ],
        descript: [
          { max: 150, message: "长度在 4 到 20 个字符", trigger: "blur" }
        ],
        callbackUrl: [
          { max: 150, message: "长度在 4 到 20 个字符", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    querySearchAsync(queryString, cb) {
      var restaurants = this.defaultEnviroments;
      var results = queryString
        ? restaurants.filter(x=>x.value.startsWith(queryString))
        : [];

      cb(results);
    },
    onSubmit() {
      this.$refs["form"].validate(async valid => {
        if (valid) {
          await appService.editApp(this.appId, this.$data.form);
          this.$emit("showFinish", true);
        } else {
          this.$message({
            message: "表单验证不通过",
            type: "warning"
          });
        }
      });
    },
    addCallbackConfig(){
        this.form.callbackConfigs.push({          
            id:-1,  
            enviroment:"",
            callbackType: "",
            callbackUrl: "",
            remark:""
        })
    },
    cancel() {
      this.$emit("showFinish", false);
    },
    async getAppInfo() {
      let appInfo = await appService.getApp(this.appId);
      appInfo.enableSSO =
        appInfo.callbackConfigs != null &&
        appInfo.callbackConfigs != undefined &&
        appInfo.callbackConfigs.length > 0;
      this.form = appInfo;
    },
    removeConfig(ix){
        this.form.callbackConfigs.splice(ix,1)
        if(this.form.callbackConfigs.length===0){
            this.addCallbackConfig()
        }
    }
  },
  mounted: function() {
    this.getAppInfo();
  },
  watch: {
    appId(to, from) {
      this.getAppInfo();
    }
  }
};
</script>
<style>
.form-border {
  margin: 10px;
}
</style>

