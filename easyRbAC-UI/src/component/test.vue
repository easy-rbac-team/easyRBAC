<template lang="pug">
  div
    div
        div
           | {{a}} 
           button(@click="testMethod") |test
</template>
<script>
import {configService} from '../service/app.ts'

export default {
  data(){
      return {
          a:"configService.fucn()"
      }
  },
  methods:{
      testMethod(){          
          alert(configService.fucn())
      }
  }
}
</script>
