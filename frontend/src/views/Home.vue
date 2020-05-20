<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";

export default {
  name: "Home",
  components: {
    HelloWorld
  }
};
</script>

<style scoped>
.books {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 300px;
  padding: 16px;
  text-align: center;
  transform: translate(-50%, -50%);
  color: #262626;
}

.semi-transparent {
  backdrop-filter: blur(10px);
  background: #f6f6f620;
}
</style>
