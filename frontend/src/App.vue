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
:root {
  --main-white-color: #f6f6f6;
  --info-color: #1F7A8C;
}

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

@import './assets/styles/main.css';
</style>
