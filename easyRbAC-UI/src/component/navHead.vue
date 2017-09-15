<template lang="pug">
div
    header.header
        div.container
            h1 Easy-RBAC
            el-button(type="warning") LogOut
</template>
<script>
export default {}
</script>
<style>
.header {
    background-color: rgb(32, 160, 255);
    height: 80px;
    background-color: #20a0ff;
    color: #fff;
    top: 0;
    left: 0;
    width: 100%;
    line-height: 80px;
    z-index: 100;
    position: relative;
}

.container {
    width: 100%;
    padding: 0 20px;
    height: 100%;
    box-sizing: border-box;
}

h1 {
    font-size: 2em;
    font-weight: bold;
    margin: 0px 0px;
    text-align: left;
    float: left;
}

header .el-button {
    float: right;
    margin-top: 20px;
}
</style>

