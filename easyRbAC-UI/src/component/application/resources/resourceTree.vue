<template lang="pug">
  div
    template(v-if="appId!==undefined && appId!==null &&appId.length>1")  
        div(v-show="showCreatebutton") 
            el-button(type="success",
            @click="createNewTree",
            ) 创建资源树
        div(v-show="!showCreatebutton")
            el-tree(:data="resources",:props="defaultProps",@node-click="",:render-content="renderContent")
    el-dialog(title="添加资源",:visible.sync="addResource",size="small")
        add-resource(:parentId="add_parentId",:appId="appId",v-on:addResourceComplete="completeResource")
</template>

<script>
import { resourceService } from '../../../service/resourceService'
import addResource from './addResource'

export default {
    props: ["appId", "appName"],
    data() {
        return {
            resources: [],
            defaultProps: {
                children: 'children',
                label: 'resourceName'
            },
            addResource:false,
            add_parentId:""
        }
    },
    computed: {
        showCreatebutton: function() {
            return this.resources == null || this.resources.length <= 0
        }
    },
    methods: {
        completeResource(refresh){
            if(refresh){
                this.getAppResource();
            }
            this.addResource = false;
        },
        async getAppResource() {
            let result = await resourceService.getAppResource(this.appId);
            if (result === "") {
                return
            }
            this.resources = [result]
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
        addChildNode(data){
            this.add_parentId = data.id;
            this.addResource = true;
        },
        renderContent(h, { node, data, store }) {
            return (
            <span>
                <span>
                <span>{node.label}</span>
                </span>
                <span style="float: right; margin-right: 20px">
                <el-button size="mini" icon="plus" type="success" on-click={ () => this.addChildNode(data) }></el-button>
                <el-button size="mini" icon="close" type="danger" on-click={ () => this.addChildNode(data) }></el-button>
                <el-button size="mini" icon="edit" type="warning" on-click={ () => this.addChildNode(data) }></el-button>
                </span>
            </span>);
      }
    },
    watch: {
        'appId'(to, from) {
            this.getAppResource()
            this.resources = []
        }
    },
    components:{
        addResource
    }
}
</script>

