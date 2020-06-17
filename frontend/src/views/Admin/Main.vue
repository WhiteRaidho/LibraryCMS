<template>
  <div>
    <div class="center-tab semi-transparent button-tab">
      <!-- <router-link to="/admin/borrows" class="button none-decoration">Wypożyczenia</router-link> -->
      <router-link to="/admin/libraries" class="button none-decoration">Biblioteki</router-link>
      <router-link to="/admin/locations" class="button none-decoration">Lokacje</router-link>
      <router-link to="/admin/librarians" class="button none-decoration">Pracownicy</router-link>
      <router-link to="/admin/books" class="button none-decoration">Książki</router-link>
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
export default class AdminMain extends Vue {}
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