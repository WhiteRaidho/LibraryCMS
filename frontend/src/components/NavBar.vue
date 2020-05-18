<template>
  <div id="navbar">
    <div id="navbar-left">
      <!-- <router-link to="/">Menu Biblioteka</router-link> -->
      <Menu />
    </div>
    <div id="navbar-right">
      <a href="/">       <!-- TODO: URL to User Account -->
        <img src="@/assets/svg_icons/Person.svg" id="image" />
      </a>
      <a href="/">  <!-- TODO: URL to Borrowed Books -->
        <img src="@/assets/svg_icons/Bookmark.svg" id="image" />
      </a>
      <a href="/About">  <!-- TODO: URL to Liked Books -->
        <img src="@/assets/svg_icons/Heart.svg" id="image" />
      </a>
    </div>
  </div>
</template>

<script lang="ts">
import { Menu } from "@/components/Menu";

export default {
  name: 'Navbar',
  components: {
    'Menu': Menu
  }
}
</script>

<style lang="scss">
#navbar {
  padding: 20px;
  background-color: #f6f6f6;
  text-align: unset;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-around;

  a {
    font-weight: bold;
    color: #2c3e50;
  }

  #navbar-left {
    margin-right: auto;
  }

  #navbar-right {
    margin-left: auto;
  }

  #image {
    padding-right: 15px;
    margin-right: 15px;
    width: 30px;
    height: 30px;
  }
}
</style>
