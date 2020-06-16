<template>
  <div class="semi-transparent center-tab">
    <div class="name">Witaj {{ user.firstName }} {{ user.lastName }}!</div>
    <div class="mail">{{ user.email }}</div>
    <div class="last-actions container">
      <span class="l-col">Przeczytane książki</span>
      <span class="r-col">{{ booksCount }}</span>
      <span class="l-col">Średnia ocen</span>
      <span v-if="avgRatings" class="r-col">{{ avgRatings }}</span>
      <span v-else class="r-col">brak ocen</span>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import AuthService, { AuthModel } from "@/services/AuthService";
// import { Prop } from "vue-property-decorator";
@Component({
  components: {}
})
export default class Profile extends Vue {
  private firstName = "";
  private lastName = "";
  private email = "";
  private booksCount = 0;
  private avgRatings? = 0;
  private user: AuthModel = {firstName: "", lastName: "", userId: "", userName: "", email: "", isAdmin: false};

  created() {
    this.loadData();
  }

  async loadData() {
    try {
      const response = await AuthService.getIdentity();
      const profileData = await AuthService.getProfileData();
      this.user = response;
      this.booksCount = profileData.booksCount;
      this.avgRatings = profileData.avgRatings;
    } catch (ex) {
      console.log(ex);
    }
  }
}
</script>

<style scoped>
.name {
  font-size: 40px;
}

.mail {
  font-size: 24px;
}

.last-actions {
  margin-top: 24px;
  margin-bottom: 24px;
}

.container {
  display: grid;
  grid-template-columns: auto auto;
  column-gap: 20px;
}

.l-col {
  grid-column: 1 / 1;
  text-align: right;
}

.r-col {
  grid-column: 2 / 2;
  text-align: left;
}
</style>
