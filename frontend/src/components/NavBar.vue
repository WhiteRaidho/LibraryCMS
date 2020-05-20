<template>
  <div class="navbar">
    <div class="navbar-left">
      <!-- <router-link to="/">Menu Biblioteka</router-link> -->
      <Menu />
    </div>
    <div class="navbar-right">
      <a href="/">       <!-- TODO: URL to User Account -->
        <img src="@/assets/svg_icons/Person.svg" class="image" />
      </a>
      <a href="/">  <!-- TODO: URL to Borrowed Books -->
        <img src="@/assets/svg_icons/Bookmark.svg" class="image" />
      </a>
      <a href="/About">  <!-- TODO: URL to Liked Books -->
        <img src="@/assets/svg_icons/Heart.svg" class="image" />
      </a>
    </div>
  </div>
</template>

<script lang="ts">
import Menu from "./Menu";

export default {
  name: 'Navbar',
  components: {
    Menu
  }
};
</script>

<style lang="scss">
:root {
  --main-white-color: #f6f6f6;
}
.navbar {
  padding: 20px;
  padding-bottom: 5px;
  padding-top: 5px;
  // background-color: #f6f6f6;
  background-color: #464646;
  text-align: unset;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-around;

  a {
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
