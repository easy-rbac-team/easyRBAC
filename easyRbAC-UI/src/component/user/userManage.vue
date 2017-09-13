<template lang="pug">
  el-row    
    el-col(:span="5")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addUser") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="用户名",icon="search",:on-icon-click="iconClickHandler",v-model="userName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(u,index) in users", :key="u.id")                
                    | {{u.userName}}
                    el-button-group
                        el-button(icon="delete",size="mini",type="danger",@click="deleteUser(index,u.id)")
                        el-button(icon="edit",size="mini",type="warning",@click="editUser(u.id)")
                        el-button(icon="information",size="mini",type="info")
    el-col(:span="8")
        add-page(v-if="showAddUser",v-on:addedUserFinish="addedUserHandle")
        router-view
</template>

<script>
import { userService } from '../../service/userService.ts'
import addPage from './addPage.vue'

export default {
    data() {
        return {
            userName: "",
            users: [],
            page: {},
            showAddUser: false
        }
    },
    methods: {
        iconClickHandler() {
            userService.getUsers(this.userName, 1, 20)
        },
        addUser() {
            this.$router.push({ path: "/user" })
            this.showAddUser = !this.showAddUser;
        },
        editUser(userId) {
            this.showAddUser = false;
            this.$router.push({ path: `/user/edit/${userId}` })
        },
        async getUserLst() {
            let users = await userService.getUsers("", 1, 20);
            this.users = users.items;
            this.page = users.page;
        },
        addedUserHandle(refresh) {
            if (refresh) {
                this.getUserLst();
            }
            this.showAddUser = false;
        },
        async deleteUser(index, userId) {
            this.$confirm('确定删除此用户?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(async () => {
                await userService.deleteUser(userId);
                this.users.splice(index, 1)
                this.$message({
                    type: 'success',
                    message: '删除成功!'
                });
            }).catch();
        }
    },
    mounted: async function() {
        await this.getUserLst();
    },
    components: {
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

.el-button-group {
    margin-left: 10px;
}
</style>

