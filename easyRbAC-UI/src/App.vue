<template>
  <div id="app">
    <nav-header></nav-header>
    <el-row>
      <left-menu></left-menu>
    </el-row>
  </div>
</template>

<script>
import test from "./component/test.vue"
import navHeader from './component/navHead.vue'
import leftMenu from './component/leftMenu.vue'

export default {
  data() {
    return {
      msg: 'Want something new?'
    }
  },

  methods: {
    startHacking() {
      this.$notify({
        title: 'Shhh',
        message: 'Just be patient...',
        duration: 6000
      })
    }
  },

  components: {
    test,
    navHeader,
    leftMenu
  }
}
</script>

<style>
html,
body,
#app {
  height: 100%;
  width: 100%;
  margin: 0;
  padding: 0
}

#app {
  font-family: Helvetica, sans-serif;
  text-align: center;
}


#app .el-row {
  height: calc(100% - 80px);
}

.el-row .el-col,
.el-row .el-menu {
  height: 100%;
}
</style>
