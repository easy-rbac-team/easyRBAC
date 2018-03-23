<template lang="pug">
  el-row    
    el-col(:span="5")
        el-card.box-card                  
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addUser") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="用户名",suffix-icon="el-icon-search",v-model="userName", @change="iconClickHandler")
                |
            |   
            .text.item(v-for="(u,index) in users", :key="u.id")                
                    span(v-bind:class="{isEnable:!u.enable,isApplication:u.accountType===1}") 
                        | {{u.userName}}
                    el-button-group.right-buttons
                        el-button(icon="el-icon-delete",size="mini",type="danger",@click="deleteUser(index,u.id)",v-if="u.enable")
                        el-button(icon="el-icon-refresh",size="mini",type="danger",@click="recorveUser(index,u.id)",v-else="!u.enable")
                        el-button(icon="el-icon-edit",size="mini",type="warning",@click="editUser(u.id)")
                        el-button(icon="el-icon-setting",size="mini",type="primary",@click="doChangePwd(u.id)")
                        el-button(icon="el-icon-info",size="mini",type="info",@click="showUserInfo(u)")
            el-pagination(small,layout="prev, pager, next",:total="page.totalCount",:page-size="page.pageSize")
    el-col(:span="18")
        add-page(v-if="showStatus.showAddUser",v-on:addedUserFinish="addedUserHandle")
        change-pwd(v-if="showStatus.showChangePwd",:userId="selectUserId",@closeChangePwd="closeChangePwdHandler")
        edit-user(v-if="showStatus.editUser",:userId="selectUserId",@close="closeChangePwdHandler")
        user-info(v-if="showStatus.userInfo",:userInfo="selectUser",@close="closeChangePwdHandler")
</template>

<script>
import { userService } from '../../service/userService.ts'
import addPage from './addPage.vue'
import changePwd from './changePwd.vue'
import editUser from './editUser'
import userInfo from './userInfo'

export default {
    data() {
        return {
            userName: "",
            selectUserId: "",
            users: [],
            page: {},
            selectUser:null,
            showStatus: {
                showAddUser: false,
                showChangePwd: false,
                editUser :false,
                userInfo :false
            }
        }
    },
    methods: {
        showUserInfo(user){
            this.closeAll();
            this.selectUser = user;
            this.showStatus.userInfo = true;
        },
        async iconClickHandler() {
            let pageResult = await userService.getUsers(this.userName, 1, 20)
            
            this.page = pageResult.page;
            this.users = pageResult.items
        },
        addUser() {
            this.closeAll();            
            this.showStatus.showAddUser = !this.showStatus.showAddUser;
        },
        editUser(userId) {
            this.selectUserId = userId;
            this.closeAll();
            this.showStatus.editUser = true;
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
            this.closeAll();
        },
        recorveUser(index,userId){
            this.$message('还未实现阿喂！');
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
        },
        doChangePwd(userId) {
            this.closeAll()
            this.showStatus.showChangePwd = true;
            this.selectUserId = userId;
        },
        closeChangePwdHandler() {
            this.closeAll();
        },
        closeAll() {
            for (let key of Object.keys(this.showStatus)) {
                this.showStatus[key] = false
            }
        }
    },
    mounted: async function() {
        await this.getUserLst();
    },
    components: {
        addPage,
        changePwd,
        editUser,
        userInfo
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

.right-buttons {
    float: right;
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

.isEnable{
    color: red;
}

.isApplication{
    color: #E6A23C;
}
</style>

