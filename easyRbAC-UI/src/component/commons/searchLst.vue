<template lang="pug">
  el-card.box-card.container  
    div(style="line-height: 36px;") 
        el-input(:placeholder="placeholder",suffix-icon="search",v-model="searchCondition", @change="iconClickHandler")
    .text.item(v-for="(r,index) in pagingList.items", :key="r.id",@click="itemSelect(index,r)",v-bind:class="{selected:index===selectedIndex}")                
        slot(:item="r")
    .page-container
        el-pagination(small,layout="prev, pager, next",:total="pagingList.page.totalCount",:page-size="pagingList.page.pageSize",@current-change="pageChange")
</template>

<script>
export default {
    props:{         
        searchFun:{
            type:Function,
            require:true
        },
        placeholder:{
            type:String,
            require:true
        }
    },
    methods:{
        async iconClickHandler(){                        
            let result = await this.searchFun(this.searchCondition,this.searchPage.pageIndex,this.searchPage.pageSize)
            this.pagingList = result;
        },
        itemSelect(index,item){
            this.selectedIndex = index;
            this.$emit("itemClick",{index,item})
        },
        pageChange(pageIndex){
            this.searchPage.pageIndex = pageIndex;
            this.iconClickHandler()
        }        
    },
    data(){
        return{
            selectedIndex:"",
            searchCondition:"",
            searchPage:{
                pageIndex:1,
                pageSize:20
            },
            pagingList:{
                page:{pageIndex:1,pageSize:20},
                items:[]
            }
        }
    },
    mounted:function(){
        this.iconClickHandler();
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
    width: 90%;
    margin: 10px 10px;
}
.page-container{
    text-align: center;
}
</style>
