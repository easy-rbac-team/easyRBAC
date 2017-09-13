<template lang="pug">
  el-row    
    el-col(:span="8")
        el-card.box-card.container
            .clearfix(slot="header")               
                div(style="line-height: 36px;width:147px") 
                    el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addRoleHandler") 添加      
                    el-input(placeholder="组名",icon="search",:on-icon-click="iconClickHandler",v-model="roleName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(r,index) in roles", :key="r.id")                
                    span().span_location
                       | {{r.roleName}}
                    el-button-group.group_location
                        el-button(icon="delete",size="mini",type="danger",@click="deleteRole(index,r.id)")
                        el-button(icon="edit",size="mini",type="warning",@click="doShowEditRole(r.id)")
                        el-button(icon="information",size="mini",type="info")
    el-col(:span="8")
        add-role(v-if="showAddRole",v-on:showFinish="showFinish")
        edit-role(v-if="showEditRole",v-on:editFinish="showFinish",:roleId = "editUserId")        
</template>

<script>
import {roleService} from '../../service/roleService.ts'
import addRole from './addRole.vue'
import eidtRole from './editRole.vue'

export default {
  data(){
      return{
          roleName:"",
          roles:[],
          page:{},
          showAddRole:false,
          pageIndex:1,
          showEditRole:false,
          editUserId:""
      }
  },
  methods:{
      async getRoles(roleName,pageIndex){
         let pageResult =  await roleService.searchRoles(roleName,pageIndex,20);
         this.roles = pageResult.items;
         this.page = pageResult.page;
      },
      async deleteRole(index,id){         
          this.$confirm('确定删除此角色?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(async () => {
                await roleService.deleteRole(id)
                this.roles.splice(index, 1)
                this.$message({
                    type: 'success',
                    message: '删除成功!'
                });
            }).catch();
      },
      editGroup(index,id){

      },
      addRoleHandler(){
          this.showAddRole = !this.showAddRole;
      },
      iconClickHandler(){
          this.getRoles(this.roleName,this.pageIndex)
      },
      showFinish(refresh){
          if(refresh){
              this.getRoles(this.roleName,this.pageIndex)
          }
          this.showAddRole = false;
          this.showEditRole = false;
      },
      doShowEditRole(roleId){
          this.showEditRole = true;
          this.editUserId = roleId;
      }     
  },
  mounted:function(){
      this.getRoles(this.roleName,1);
  },
  components:{
      addRole,
      "edit-role":eidtRole
  }
}
</script>
<style>
.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}

.clearfix:after {
    clear: both
}

.box-card {
    width: 300px;
    margin: 10px 10px;
}
.container{
    margin: 10px 10px;
}
.group_location{
    float: right;;
}
.span_location{
    float: left;
}

.text {
    font-size: 14px;
}

.item {
    padding: 8px 0;
    height: 1em;
}
</style>
