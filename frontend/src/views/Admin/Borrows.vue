<template>
  <div class="">
      Borrows
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';

@Component({
  components: {
  }
})
export default class AdminBooks extends Vue {
 created() {
     this.loadData();
 }

 async loadData() {
     console.log("lel");
 }
}
</script>

<style scoped>
.flex-row > a {
    margin: 4px;
}
</style>