<template>
  <div>
    <!-- <div id="bg-overlay"></div> -->
    <Menu noOverlay>
      <router-link v-if="!isAuth()" to="/login">Login</router-link>
      <router-link v-if="!isAuth()" to="/register">Rejestracja</router-link>
      <router-link to="/">Stron główna</router-link>
      <router-link to="/books">Książki</router-link>
      <router-link to="/libraries">Biblioteki</router-link>
      <router-link v-if="isAdmin()" to="/admin">Panel administratora</router-link>
      <router-link v-if="isLibrarian()" to="/librarian">Panel bibliotekarza</router-link>
    </Menu>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import { Menu } from 'vue-burger-menu';

@Component({
  components: { Menu }
})
export default class MenuBar extends Vue{
  isAdmin() :boolean {
    if(this.$auth.check() && this.$auth.user().isAdmin) return true;
    return false;
  }

  isAuth() :boolean {
    return this.$auth.check();
  }

  isLibrarian() :boolean {
    return this.$auth.check() && (this.$auth.user().isLibrarian || this.$auth.user().isAdmin);
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
