<template>
  <div class="app">
    <NavBar />
    <router-view />
  </div>
</template>

<script lang="ts">
import NavBar from "@/components/NavBar.vue";

export default {
  name: "App",
  components: {
    NavBar
  }
};
</script>

<style lang="scss">
body {
  margin: 0px;
  background-image: url("../src/assets/library_bg.jpg");
  background-attachment: fixed;
}

.app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #0c0f13;
  display: flex;
  flex-direction: column;
}
</style>
