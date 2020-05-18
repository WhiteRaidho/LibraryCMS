<template>
  <Menu>
    <router-link to="/">Menu Biblioteka</router-link>
  </Menu>
</template>

<script lang="ts">
import { Menu } from 'vue-burger-menu';
export default {
  name: 'Menu',
  components: {
    Menu
  }
};
</script>
