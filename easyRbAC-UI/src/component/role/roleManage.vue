<template lang="pug">
  el-row    
    el-col(:span="5")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addRoleHandler") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="组名",icon="search",:on-icon-click="iconClickHandler",v-model="roleName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(r,index) in roles", :key="r.id")                
                    | {{r.roleName}}
                    el-button-group
                        el-button(icon="delete",size="mini",type="danger",@click="deleteGroup(index,r.id)")
                        el-button(icon="edit",size="mini",type="warning",@click="editGroup(r.id)")
                        el-button(icon="information",size="mini",type="info")
    el-col(:span="8")
        add-role(v-if="showAddRole")
</template>

<script>
import {roleService} from '../../service/roleService.ts'
import addRole from './addRole.vue'

export default {
  data(){
      return{
          roleName:"",
          roles:[],
          page:{},
          showAddRole:false
      }
  },
  methods:{
      async getUsers(roleName,pageIndex){
         let pageResult =  await roleService.searchRoles(roleName,pageIndex,20);
         this.roles = pageResult.items;
         this.page = pageResult.page;
      },
      deleteGroup(index,id){
      },
      editGroup(index,id){

      },
      addRoleHandler(){
          this.showAddRole = !this.showAddRole;
      },
      iconClickHandler(){
          
      }
  },
  mounted:function(){
      this.getUsers("",1);
  },
  components:{
      addRole
  }
}
</script>
