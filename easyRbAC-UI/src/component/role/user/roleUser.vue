<template lang="pug">
    el-row
        el-col(:span=8)
            el-card.box-card                  
                    .clearfix(slot="header")   
                        div(style="line-height: 36px;") 
                            el-input(placeholder="角色名|CODE",icon="search",:on-icon-click="getRoles",v-model="search.condition", @keyup.enter.native="getRoles")
                        |
                    |   
                    .text.item(v-for="(r,index) in roles", :key="r.id",@click="selectRole(r)",v-bind:class="{selected:r.active}")                
                            | {{r.roleName}}                        
                    el-pagination(small,layout="prev, pager, next",:total="search.page.totalCount",:page-size="search.page.pageSize")
        el-col(:span=12)
            .container(v-show="selectedRoleId!==''")
                el-transfer(v-model="userIds",filterable,
                :titles="['组外', '组内']",
                :button-texts="['出组','进组']",
                :data="allUsers", @change="handleChange"
                )

</template>

<script>
import { roleService } from '../../../service/roleService'
import { userService } from '../../../service/userService'

export default {
    data() {
        return {
            search: {
                condition: "",
                page: {
                    pageIndex: 1,
                    totalCount: 0
                },
            },
            roles: [],
            allUsers: [],
            userIds: [],
            selectedRoleId: ""
        }
    },
    methods: {
        async getRoles() {
            let pageResult = await roleService.searchRoles(this.search.condition, this.search.page.pageIndex, 20);
            this.roles = pageResult.items;
            this.search.page = pageResult.page;
        },
        async handleChange(value, direction, movedKeys) {
            debugger;
            if (this.selectedRoleId !== "") {
                await roleService.changeRoleMember(this.selectedRoleId,value);
                this.$message('修改成功！')
            }
        },
        async selectRole(role){
            debugger;
            this.userIds =await roleService.getRoleMember(role.id);
            this.roles.forEach(x=>x.active = false);
            role.active = true;

            this.selectedRoleId =role.id;
        }
    },
    mounted: async function() {
        this.getRoles();
        let users = await userService.getUsers("", 1, 1000000)
        let u = users.items.map(x => {
            let result = {
                label: x.userName,
                key: x.id
            }
            return result;
        })
        this.allUsers = u;
    }
}
</script>


<style scoped>
.text {
    font-size: 14px;
    text-align: center;
}

.item {
    padding: 8px 0;
    margin: 10px 10px;
    border: 1px solid #eaeefb;
    border-radius: 4px;
    transition: .2s;
}

.container {
    margin: 10px 10px;
}

.item:hover {
    cursor: pointer;
    background: #99A9BF;
    color: #eaeefb
}

.selected{
    background:#E5E9F2;
    color: #5e6d82;
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
