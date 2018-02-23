<template lang="pug">
  div(style="margin-top: 10px;")
    template(v-if="appId!==undefined && appId!==null &&appId.length>1")  
        div(v-show="showCreatebutton") 
            el-button(type="success",@click="createNewTree",) 创建资源树
        div(v-show="!showCreatebutton")
            el-tree(:data="resources",:props="defaultProps",:render-content="renderContent",:expand-on-click-node="false",default-expand-all,class="my-tree")
    el-dialog(title="添加资源",:visible.sync="showDialog",size="small")
        add-resource(:parentId="add_parentId",:appId="appId",v-on:addResourceComplete="completeResource",v-if="showStatus.addResource")
        resource-info(:resource="selectedResource",v-if="showStatus.info",@addResourceComplete="completeResource")
        edit-resource(:resource="selectedResource",v-if="showStatus.edit",@addResourceComplete="completeResource")
</template>

<script>
import { resourceService } from '../../../service/resourceService'
import addResource from './addResource'
import resourceInfo from './resourceInfo'
import editResource from './editResource'

export default {
    props: ["appId", "appName"],
    data() {
        return {
            resources: [],
            defaultProps: {
                children: 'children',
                label: 'resourceName'
            },
            showStatus: {
                addResource: false,
                info: false,
                edit: false
            },
            add_parentId: "",
            selectedResource: null
        }
    },
    computed: {
        showCreatebutton: function() {
            return this.resources == null || this.resources.length <= 0
        },
        showDialog: {
            get: function() {
                let result = false;
                for (let key of Object.keys(this.showStatus)) {
                    result |= this.showStatus[key];
                }
                return Boolean(result);
            },
            set: function(value) {
                if (!value) {
                    for (let key of Object.keys(this.showStatus)) {
                        this.showStatus[key] = false;
                    }
                }
            }
        }
    },
    methods: {
        completeResource(refresh) {
            if (refresh) {
                this.getAppResource();
            }
            this.closeAll()
        },
        async getAppResource() {
            let result = await resourceService.getAppResource(this.appId);
            if (result === "") {
                return
            }
            this.resources = result
        },
        async createNewTree() {
            let app = {
                applicationId: this.appId,
                resourceName: this.appName,
                resourceCode: `${this.appName}_root`
            }
            await resourceService.addResource(0, app)
            await this.getAppResource();
        },
        addChildNode(data) {
            this.closeAll();
            this.add_parentId = data.id;
            this.showStatus.addResource = true;
        },
        showResourceInfo(data) {
            this.closeAll();
            this.showStatus.info = true;
            this.selectedResource = data;
        },
        editResource(data) {
            this.closeAll();
            this.showStatus.edit = true;
            this.selectedResource = data;
        },
        deleteResource(data) {
            this.$confirm(`是否确定要删除资源:${data.resourceName}?`, '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                resourceService.disableResource(data.id).then((result) => {
                    this.completeResource(true)
                    this.$message({
                        type: 'success',
                        message: '删除成功!'
                    });
                })
            });
        },
        closeAll() {
            for (let key of Object.keys(this.showStatus)) {
                this.showStatus[key] = false;
            }
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
                <div>                                    
                    {menuIco}
                    {resourceIco}
                    {publicIco}
                    <span>
                        <span >{node.label}</span>
                    </span>
                    <span style="float: right; margin-right: 20px">
                        <el-button-group>
                            <el-button size="mini" icon="el-icon-plus" type="success" on-click={() => this.addChildNode(data)}></el-button>
                            <el-button size="mini" icon="el-icon-close" type="danger" on-click={() => this.deleteResource(data)}></el-button>
                            <el-button size="mini" icon="el-icon-edit" type="warning" on-click={() => this.editResource(data)}></el-button>
                            <el-button size="mini" icon="el-icon-info" type="primary" on-click={() => this.showResourceInfo(data)}></el-button>
                        </el-button-group>
                    </span>
                </div>);
        }
    },
    watch: {
        'appId'(to, from) {
            this.getAppResource()
            this.resources = []
        }
    },
    components: {
        addResource,
        resourceInfo,
        editResource
    }
}
</script>

<style>
.my-tree .el-tree-node__content{
    height: 35px;
}
</style>


