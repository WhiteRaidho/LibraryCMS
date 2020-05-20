<template>
  <div>
    <!-- <div id="bg-overlay"></div> -->
    <Menu noOverlay>
      <router-link to="/">Menu Biblioteka</router-link>
    </Menu>
  </div>
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
  top: 16px;
  height: 20px;
}
.bm-menu {
  background-color: var(--main-white-color);
}

.cross-style {
  top: 16px;
  right: 8px;
}

.bm-cross {
  width: 5px !important;
  height: 25px !important;
}

#bg-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  backdrop-filter: blur(10px);
}
</style>
