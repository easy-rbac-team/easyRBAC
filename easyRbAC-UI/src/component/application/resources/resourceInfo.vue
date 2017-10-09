<template lang="pug">
  .resource-info
    .item-row.clearfix
        .title
            | ID
        .value
            | {{resource.id}}
    .item-row
        .title
            | 资源名
        .value
            | {{resource.resourceName}}
    .item-row
        .title
            | 资源code
        .value
            | {{resource.resourceCode}}
    .item-row
        .title
            | 资源参数
        .value
            | {{resource.parameters}}
    .item-row
        .title
            | URL
        .value
            | {{resource.url}}
    .item-row
        .title
            | 图标
        .value
            | {{resource.iconUrl}}
    .item-row
        .title
            | 资源类型
        .value 
            el-tag(v-for="x in resourceTypes",:type="x.type",:key="x.name").tags
                | {{x.name}}
    .item-row
        .title
            | 描述
        .value
            | {{resource.describe}}

</template>

<script>
export default {
    props:{
        resource:{
            type:Object,
            required:true
        }
    },
    computed:{
        resourceTypes:function(){
            let result = []
            for(let i=0;i<3;i++){
                let permissionTag = Math.pow(2,i);
                if((this.resource.resourceType & permissionTag) === permissionTag){
                    switch(permissionTag){
                        case 1:
                            result.push({name:"资源",type:"primary"});
                            break;
                        case 2:
                            result.push({name:"菜单",type:"success"});
                            break;
                        case 4:
                            result.push({name:"公开",type:"danger"})
                    }                        
                }
            }
            return result;
        }
    },
    data() {
        return {           
        }
    }
}
</script>


<style>
.resource-info {
    /* border: 1px solid #D3DCE6 */
    width: 100%;
}

.item-row{
    display: block;
    border-bottom: 1px solid #D3DCE6;
}
.title{   
    display: inline;    
}
.value{    
    padding-left: 50px;
    display: inline;
}
.tags{
    margin-left: 10px;
}
.clearfix:after {
    clear: both
}
</style>
