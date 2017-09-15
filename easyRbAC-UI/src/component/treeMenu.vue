<template lang="pug">
  div
    template(v-for="item in menu")
        template(v-if="item.children")
            el-submenu(:index="item.id")
                template(slot="title")
                    | {{item.name}}
                el-menu-item-group
                    tree-menu(:menu="item.children")
        template(v-else)
            el-menu-item(:index="item.path")
                | {{item.name}}
</template>

<script>
export default {
    props: ["menu"],
    mounted: function() {
        console.log(this.menu)
    }
}
</script>
