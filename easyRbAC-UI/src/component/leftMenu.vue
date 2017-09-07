<template lang="pug">
    el-col(:span="4")
       el-menu.left-menu(default-active="2", @open="handleOpen", @close="handleClose")
            el-submenu(index="1")
                template(slot="title")
                    i.el-icon-message
                    | 导航一
                el-menu-item-group
                    template(slot="title") 分组一
                    el-menu-item(index="1-1") 选项1
                    el-menu-item(index="1-2") 选项2
                el-menu-item-group(title="分组2")
                    el-menu-item(index="1-3") 选项3
                el-submenu(index="1-4")
                    template(slot="title") 选项4
                    el-menu-item(index="1-4-1") 选项1
            el-menu-item(index="2")
                i.el-icon-menu
                | 导航二
            el-menu-item(index="3")
                i.el-icon-setting
                | 导航三
</template>

<script>
export default {
  
}
</script>

<style>
.left-menu{
    min-height: 500px;
}
</style>

