<template lang="pug">
  div
    el-col(:span="6")
        search-lst(:searchFun="getApps",placeholder="应用名/code",@itemClick="itemClickHandler")
            template(scope="props")
                | {{props.item.appName}}
    el-col(:span="12").tree-container
        el-tree(:data="resourceTree",
                :props="defaultProps",
                node-key="id",show-checkbox,default-expand-all,
                :check-strictly="true",ref="tree",@node-click="")
        .buttons
        el-button(type="success",@click="saveHandler") 保存
        el-button(@click="reset") 还原
</template>

<script>
import {resourceService} from '../../service/resourceService'
import {appService} from '../../service/appService'

import searchLst from '../commons/searchLst'

export default {
    props: {
        checkedKeys:{
            type:Array
        },
        disableKeys:{
            type:Array
        }
    },
    data() {
        return {
            selectedAppId: "",
            resourceTree: [],
            defaultProps: {
                    children: 'children',
                    label: 'resourceName',
                    disabled: 'disabled'
                }
        }
    },
    methods: {
        async getApps(searchCondition, pageIndex, pageSize) {
            let pageResult = await appService.searchApp(searchCondition, pageIndex, pageSize)
            return pageResult;
        },
        async getResourceTree() {
            if(this.selectedAppId===""){
                return;
            }
            let resourceTree = await resourceService.getAppResource(this.selectedAppId);
            this.resourceTree = resourceTree;
        },
        itemClickHandler(arg){
            let app = arg.item;
            this.selectedAppId = app.id;
            this.getResourceTree();
            this.$emit("appSelect",arg)
        },
        setDisableKeys(){
            this.$refs.tree.setCheckedKeys(this.disableKeys);
        },
        saveHandler(){
            let nodes = this.$refs.tree.getCheckedNodes();
            this.$emit("setScope",nodes);
        },
        reset(){
            this.$refs.tree.setCheckedKeys(this.selected_temp);
        }
    },
    watch:{
        disableKeys:function(to,from){
            this.setDisableKeys()
            this.selected_temp = to;
        }
    },
    components:{
        searchLst
    }
}
</script>

<style>
.buttons{
    margin-top: 10px;
}
.tree-container{
    margin-top: 10px;
}
</style>
