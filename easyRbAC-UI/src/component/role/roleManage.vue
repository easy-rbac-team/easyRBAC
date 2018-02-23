<template lang="pug">
  el-row    
    el-col(:span="8")
        el-card.box-card.container
            .clearfix(slot="header")
                el-button(type="success",icon="plus",size="small",style="float:left;margin:4px 5px",@click="addRoleHandler") 添加      
                div(style="line-height: 36px;") 
                    el-input(placeholder="组名|CODE",suffix-icon="el-icon-search",v-model="roleName", @keyup.enter.native="iconClickHandler")
                |
            |   
            .text.item(v-for="(r,index) in roles", :key="r.id")                
                    | {{r.roleName}}
                    el-button-group.group_location
                        el-button(icon="el-icon-delete",size="mini",type="danger",@click="deleteRole(index,r.id)")
                        el-button(icon="el-icon-edit",size="mini",type="warning",@click="doShowEditRole(r.id)")
                        el-button(icon="el-icon-info",size="mini",type="info",@click="showRoleInfo(r.id)")
            el-pagination(small,layout="prev, pager, next",:total="page.totalCount",:page-size="page.pageSize")
    el-col(:span="8")
        add-role(v-if="showStatus.addRole",v-on:showFinish="showFinish")
        edit-role(v-if="showStatus.editRole",v-on:editFinish="showFinish",:roleId = "editUserId")
        edit-role(v-if="showStatus.roleInfo",v-on:editFinish="showFinish",:roleId = "editUserId",:readOnly="true")        
</template>

<script>
import { roleService } from '../../service/roleService.ts'
import addRole from './addRole.vue'
import eidtRole from './editRole.vue'

export default {
    data() {
        return {
            roleName: "",
            roles: [],
            page: {},            
            pageIndex: 1,            
            editUserId: "",
            showStatus:{
                addRole:false,
                editRole:false,
                roleInfo:false
            }
        }
    },
    methods: {
        async getRoles(roleName, pageIndex) {
            let pageResult = await roleService.searchRoles(roleName, pageIndex, 20);
            this.roles = pageResult.items;
            this.page = pageResult.page;
        },
        async deleteRole(index, id) {
            this.showEditRole = false;
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
        editGroup(index, id) {

        },
        closeAll(){
            for (let key of Object.keys(this.showStatus)) {
                this.showStatus[key] = false
            }
        },
        addRoleHandler() {
            this.closeAll()
            this.showStatus.addRole=true;
        },
        iconClickHandler() {
            this.getRoles(this.roleName, this.pageIndex)
        },
        showFinish(refresh) {
            if (refresh) {
                this.getRoles(this.roleName, this.pageIndex)
            }
            this.closeAll()
        },
        doShowEditRole(roleId) {
            this.closeAll()            
            this.editUserId = roleId;
            this.showStatus.editRole = true;            
        },
        showRoleInfo(roleId){
            this.closeAll();
            this.editUserId = roleId;
            this.showStatus.roleInfo = true;
        }
    },
    mounted: function() {
        this.getRoles(this.roleName, 1);
    },
    components: {
        addRole,
        "edit-role": eidtRole
    }
}
</script>
<style>
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

.group_location{
    float: right;;
}
.group_location button{
    line-height: 10px;
    font-size: 10px;
    padding-left: 10px;
    padding-right: 10px;
}
</style>
