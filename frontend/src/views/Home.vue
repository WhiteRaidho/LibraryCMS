<template>
  <div class="semi-transparent center-absolute-tab home">
    <div class="home-text">
      Szukasz książki? Sprawdź czy jest dostępna u nas!
    </div>
    <!-- <form @submit.prevent="onSubmit" class="search-bar m8">
      <input type="text" placeholder="Podaj tytuł książki lub autora..." v-model="search" />
      <button type="submit"><i class="fas fa-search"></i></button>
    </form> -->
    <search-bar text="Podaj tytuł książki lub autora..." @submit="onSubmit"/>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
import SearchBar from '@/components/SearchBar.vue';

@Component({
  components: {
    SearchBar
  }
})
export default class Home extends Vue{
  private search = "";

  onSubmit(search: string)
  {
    this.$router.push({path: "/books", query: {search: search, author: '', lib: ''}});
  }
};
</script>

<style scoped>
.home {
  min-height: 25%;
  width: 70%;
}

.home-text {
  font-size: 30px;
  padding: 50px;
}


</style>
