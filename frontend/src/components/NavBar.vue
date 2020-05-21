<template>
  <div class="navbar">
    <div class="navbar-left">
      <!-- <router-link to="/">Menu Biblioteka</router-link> -->
      <Menu />
    </div>
    <div class="navbar-right">
      <router-link to="/profile" class="router-link">       <!-- TODO: URL to User Account -->
        <img src="@/assets/svg_icons/Person.svg" class="image" />
      </router-link>
      <router-link to="/" class="router-link">  <!-- TODO: URL to Borrowed Books -->
        <img src="@/assets/svg_icons/Bookmark.svg" class="image" />
      </router-link>
      <router-link to="/About" class="router-link">  <!-- TODO: URL to Liked Books -->
        <img src="@/assets/svg_icons/Heart.svg" class="image" />
      </router-link>
      <button class="button" v-on:click="this.$auth.logout()">Wyloguj</button>
    </div>
    
  </div>
</template>

<script lang="ts">
import Menu from "./Menu.vue";

export default {
  name: 'Navbar',
  components: {
    Menu
  }
};
</script>

<style lang="scss">
.navbar {
  padding: 20px;
  padding-bottom: 5px;
  padding-top: 5px;
  // background-color: #f6f6f6;
  background-color: var(--main-white-color);
  text-align: unset;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-around;

  .router-link {
    font-weight: bold;
    color: #2c3e50;
  }

  .navbar-left {
    margin-right: auto;
  }

  .navbar-right {
    margin-left: auto;
  }

  .image {
    padding-right: 15px;
    margin-right: 15px;
    width: 30px;
    height: 30px;
    
  }

  .circle-border {
    border: 2px solid #707070;
    box-sizing: border-box;
    border-radius: 100%;
  }
}
</style>
