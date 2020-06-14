<template>
  <div class="semi-transparent center-absolute-tab font-2x">
    <div class="m8">
      Wygląda na to że nie powinno cię tu być
    </div>
    <router-link class="button none-decoration" to="/">Zabierz mnie stąd</router-link>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'

@Component({
  components: {
  }
})
export default class Forbidden extends Vue{
};
</script>

<style scoped>
</style>
