<template lang="pug">
  el-row    
    el-col(:span="5")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="用户名",icon="search",:on-icon-click="iconClickHandler",v-model="userName", @keyup.enter.native="iconClickHandler")
                |     
                // el-button(style="float: right;", type="primary") 搜索
            |   
            .text.item(v-for="o in 20", :key="o")
                | {{'列表内容 ' + o }}
    el-col(:span="8")
        add-page
</template>

<script>
import {userService} from '../../service/userService.ts'
import addPage from './addPage.vue'

export default {
    data() {
        return {
            userName: "",
            users:[],
            page:{}
        }
    },
    methods: {
        iconClickHandler() {
            console.log("click")
            userService.getUsers()
        }
    },
    mounted:async function(){
        let users = await userService.getUsers("",1,20);
        this.users= users.items;
        this.page={
            pageIndex: users.pageIndex,
            totalPage: users.totalPage
        }
    },
    components:{
        addPage
    }
}
</script>

<style scoped>
.text {
    font-size: 14px;
}

.item {
    padding: 8px 0;
}

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

.el-input {
    width: 180px;
}
</style>

