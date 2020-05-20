<template>
  <Menu noOverlay>
    <router-link to="/">Menu Biblioteka</router-link>
  </Menu>
</template>

<script lang="ts">
import { Menu } from 'vue-burger-menu';
export default {
  components: {
    Menu
  }
};
</script>
<style lang="scss">
.bm-burger-button {
  top: 22px;
}
.bm-menu {
  background-color: var(--main-white-color);
}
</style>
