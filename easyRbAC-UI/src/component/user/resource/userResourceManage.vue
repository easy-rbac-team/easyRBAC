<template lang="pug">
el-row
    el-col(:span="6")
            el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="用户名",icon="search",v-model="userInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(r,index) in userInfo.users", :key="r.id",@click="userSelect(r.id)",v-bind:class="{selected:r.id===userInfo.selectedUserId}")                
                        | {{r.userName}}
                el-pagination(small,layout="prev, pager, next",:total="userInfo.page.totalCount",:page-size="userInfo.page.pageSize")
    el-col(:span="6")
        el-card.box-card.container  
                div(style="line-height: 36px;") 
                    el-input(placeholder="应用名/code",icon="search",v-model="userInfo.searchCondition", @keyup.enter.native="iconClickHandler")
                .text.item(v-for="(app,index) in appInfo.apps", :key="app.id",@click="appSelect(app.id)",v-bind:class="{selected:app.id===appInfo.selectedAppId}")                
                        | {{app.appName}}
                el-pagination(small,layout="prev, pager, next",:total="appInfo.page.totalCount",:page-size="appInfo.page.pageSize")
    el-col(:span="12")
        el-tree(:data="resourceInfo.resourceTree",
                :props="resourceInfo.defaultProps",
                node-key="id",show-checkbox,default-expand-all,
                :check-strictly="true",ref="tree",@node-click="")
        el-button(type="success",@click="setUserResource") 保存
        el-button(@click="cancelSetResouce") 还原
        el-button(@click="getkeys") 获取keys
</template>
<script>
import { userService } from '../../../service/userService'
import { appService } from '../../../service/appService'
import { resourceService } from '../../../service/resourceService'

export default {
    data() {
        return {
            userInfo: {
                users: [],
                page: { pageIndex: 1, pageSize: 20 },
                selectedUserId: "",
                searchCondition: ""
            },
            appInfo: {
                apps: [],
                page: { pageIndex: 1, pageSize: 20 },
                selectedAppId: "",
                searchCondition: ""
            },
            resourceInfo: {
                resourceTree: [],
                defaultProps: {
                    children: 'children',
                    label: 'resourceName',
                    disabled: 'disabled'
                }
            }
        }
    },
    methods: {
        appSelect(appId) {
            this.appInfo.selectedAppId = appId;
            this.getResourceTree()
        },
        userSelect(userId) {
            this.userInfo.selectedUserId = userId;
            this.getResourceTree()
        },
        async getResourceTree() {
            if (this.userInfo.selectedUserId !== "" && this.appInfo.selectedAppId !== "") {
                let resourceTree = await resourceService.getAppResource(this.appInfo.selectedAppId);
                let userAllResources = await userService.getUserResourceIds(this.userInfo.selectedUserId, this.appInfo.selectedAppId)

                let allChecked = userAllResources.userResource.concat(userAllResources.roleResource);
                resourceService.setResourceDisable(resourceTree, userAllResources.roleResource);
                this.resourceInfo.resourceTree = resourceTree;
                this.keys_temp = allChecked;                
                this.$refs.tree.setCheckedKeys(allChecked);
                console.log("set的checked：")
                console.log(allChecked)
                
                this.$nextTick(()=>{
                    this.$refs.tree.setCheckedKeys(allChecked);
                    let tt = this.$refs.tree.getCheckedKeys()
                    console.log("读到的checked:")
                    console.log(tt);
                })
            }
        },
        getkeys() {
            let resourceLst = this.$refs.tree.getCheckedKeys();
            console.log("再来读一发：")
            console.log(resourceLst)
        },
        cancelSetResouce() {
            this.$refs.tree.setCheckedKeys(this.keys_temp);
            console.log("重新set一发：")            
            console.log(this.keys_temp)
        },
        async getUsers() {
            let pageResult = await userService.getUsers(this.userInfo.searchCondition, this.userInfo.page.pageIndex, this.userInfo.page.pageSize);
            this.userInfo.users = pageResult.items;
            this.userInfo.page = pageResult.page;
        },
        async getApps() {
            let pageResult = await appService.searchApp(
                this.appInfo.searchCondition,
                this.appInfo.page.pageIndex,
                this.appInfo.page.pageSize);
            this.appInfo.apps = pageResult.items;
            this.appInfo.page = pageResult.page;
        },
        async setUserResource() {
            let userId = this.userInfo.selectedUserId;
            let appId = this.appInfo.selectedAppId;
            let resourceLst = this.$refs.tree.getCheckedNodes();
            await userService.changeUserResources(userId, appId, resourceLst);
            this.$message({ type: "success", message: "设置成功！" })
            this.keys_temp = resourceLst.map(x => x.id)
        }
    },
    mounted: function() {
        this.getUsers();
        this.getApps();
    }
}
</script>


<style scoped>
.text {
    font-size: 14px;
}

.item {
    padding: 8px 0;
    text-align: center;
}

.item:hover {
    cursor: pointer;
    background: #D3DCE6;
}

.selected {
    background: #E5E9F2;
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
</style>
