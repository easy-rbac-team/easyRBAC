<template lang="pug">
el-col(:span="4")
    el-menu.left-menu(default-active="2",:router="true")
        tree-menu(:menu="menus")
            //- el-submenu(index="1")
            //-     template(slot="title")
            //-         i.el-icon-message
            //-         | 用户管理
            //-     el-menu-item-group                    
            //-         el-menu-item(index="1-1") 选项1
            //-         el-menu-item(index="1-2") 选项2                
            //-         el-menu-item(index="1-3") 选项3
            //-     el-submenu(index="1-4")
            //-         template(slot="title") 选项4
            //-         el-menu-item(index="1-4-1") 选项1
            //- el-menu-item(index="2")
            //-     i.el-icon-menu
            //-     | 导航二
            //- el-menu-item(index="3")
            //-     i.el-icon-setting
            //-     | 导航三
            
</template>

<script>
import { menuService } from '../service/menuService.ts'

export default {
    data() {
        return {
            menus: []
        }
    },
    methods: {
        async getMenu() {
            let menus = await menuService.getMenus();
            this.menus = menus
        }
    },
    mounted: function() {
        this.getMenu();
    }
}
</script>

<style>
.left-menu {
    min-height: 500px;
}
</style>

