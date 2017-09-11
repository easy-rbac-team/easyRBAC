<template lang="pug">
  el-row    
    el-col(:span="5")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addUser") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="组名",icon="search",:on-icon-click="iconClickHandler",v-model="userName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(g,index) in groups", :key="u.id")                
                    | {{u.userName}}
                    el-button-group
                        el-button(icon="delete",size="mini",type="danger",@click="deleteGroup(index,u.id)")
                        el-button(icon="edit",size="mini",type="warning",@click="editGroup(u.id)")
                        el-button(icon="information",size="mini",type="info")
    el-col(:span="8")
</template>

<script>
export default {
  data(){
      return{
          roles:[]
      }
  }
}
</script>
