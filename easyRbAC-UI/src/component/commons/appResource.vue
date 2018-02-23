<template lang="pug">
  div
    el-col(:span="6")
        search-lst(:searchFun="getApps",placeholder="应用名/code",@itemClick="itemClickHandler")
            template(slot-scope="props")
                | {{props.item.appName}}
    el-col(:span="12").tree-container
        el-tree(:data="resourceTree",
                :props="defaultProps",
                node-key="id",show-checkbox,default-expand-all,
                :render-content="renderContent"
                :check-strictly="true",ref="tree",@node-click="",class="my-tree")
        .buttons(v-if="!readOnly")
            el-button(type="success",@click="saveHandler") 保存
            el-button(@click="reset") 还原
</template>

<script>
import { resourceService } from '../../service/resourceService'
import { appService } from '../../service/appService'

import searchLst from '../commons/searchLst'

export default {
    props: {
        checkedKeys: {
            type: Array
        },
        disabledKeys: {
            type: Array
        },
        readOnly:{
            type:Boolean,
            default:false
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
            if (this.selectedAppId === "") {
                return;
            }
            let resourceTree = await resourceService.getAppResource(this.selectedAppId);
            if(this.readOnly){
                resourceService.ergodicTree(resourceTree,x=>x.disabled = true);
            }
            this.resourceTree = resourceTree;
        },
        itemClickHandler(arg) {
            let app = arg.item;
            this.selectedAppId = app.id;
            this.getResourceTree();
            this.$emit("appSelect", arg)
        },
        setDisableKeys() {            
            let disableKeys = this.disabledKeys;
            resourceService.ergodicTree(
                this.resourceTree,
                x => x.disabled = disableKeys.some(key => key == x.id))
            this.$set(this,"resourceTree",this.resourceTree)
        },
        setCheckedKeys() {            
            this.$refs.tree.setCheckedKeys(this.checkedKeys);
        },
        saveHandler() {
            let nodes = this.$refs.tree.getCheckedNodes();
            this.$emit("setScope", nodes);
        },
        reset() {
            this.$refs.tree.setCheckedKeys(this.checkedKeys);
        },
        renderContent(h, { node, data, store }) {
            let menuIco,resourceIco,publicIco;
            if((data.resourceType&2)==2){
                menuIco = <el-tag type="success"><i class="iconfont icon-menu"></i></el-tag>
            }
            if((data.resourceType&1)==1){
                resourceIco = <el-tag type="primary"><i class="iconfont icon-zhihuiyuanqu-yuncang-wuliuerjiyemiantubiao-"></i></el-tag>
            }
            if((data.resourceType&4)===4){
                publicIco = <el-tag type="danger"><i class="iconfont icon-anonymous"></i></el-tag>
            }
            return (                
                <span>                                    
                    {menuIco}
                    {resourceIco}
                    {publicIco}
                    <span>
                        <span>{node.label}</span>
                    </span>                    
                </span>
                );
        }
    },
    watch: {
        disabledKeys: function(to, from) {           
            this.setDisableKeys()
        },
        checkedKeys: function(to, from) {
            this.setCheckedKeys();            
        }
    },
    components: {
        searchLst
    }
}
</script>

<style>
.buttons {
    margin-top: 10px;
}

.tree-container {
    
    margin-top: 10px;
}

.my-tree .el-tree-node__content{
    height: 35px;
}
</style>
