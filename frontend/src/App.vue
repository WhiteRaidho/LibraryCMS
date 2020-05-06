<template>
  <div id="app">
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
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #0c0f13;
  background-image: url("../src/assets/library_bg.jpg");
  background-size: cover;
  background-attachment: fixed;
  display: flex;
  flex-direction: column;
}
</style>
