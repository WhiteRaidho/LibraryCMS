<template>
  <div>
    <div class="center-tab semi-transparent button-tab">
      <router-link to="/librarian/borrow" class="button none-decoration">Wypożycz</router-link>
      <router-link to="/librarian/return" class="button none-decoration">Zwróć</router-link>
    </div>
    <div class="center-tab semi-transparent">
      <router-view></router-view>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";

@Component({
  components: {}
})
export default class LibrarianMain extends Vue {}
</script>

<style scoped>
.button-tab {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-around;
}
.button-tab > a {
  margin: 4px;
  flex: 1 1 0px;
}
</style>
