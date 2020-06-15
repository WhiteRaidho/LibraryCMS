<template>
  <div class>
    <div class="font-2x m8 f-left">Książki</div>
    <router-link class="none-decoration button f-right p8" to="/admin/books/new">Dodaj</router-link>
    <search-bar text="Podaj autora lub tytuł" @submit="searchSubmit" class="m8" />
    
    <div class="m8"><content-table :items="items" :headers="headers" /></div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ContentTable from "@/components/ContentTable.vue";
import BooksService, { BookListItemViewModel } from "@/services/BooksService.ts";
import SearchBar from "@/components/SearchBar.vue";

@Component({
  components: {
    "content-table": ContentTable,
    "search-bar": SearchBar
  }
})
export default class AdminBooks extends Vue {
  private headers = [
    {
      name: "Tytuł książki", fieldName: "title",
      link: "/admin/books/{authorFullName}/{title}",
      contentClass: "none-decoration"
    },
    {
      name: "Autor", fieldName: "authorFullName",
      //link: "/admin/books?search=[search]&author={authorFullName}",
      contentClass: "none-decoration"
    },
    {name: "Egzemplarze", fieldName: "copies"},
    // {name: "", link: "/home", ico: "fas fa-trash", columnClass: "action-column"},
    // {name: "", ico: "fas fa-pencil-alt", columnClass: "action-column"}
  ];

  private items: BookListItemViewModel[] = [];

  private get search() {
    if (typeof this.$route.query.search !== "undefined") {
      return String(this.$route.query.search || "");
    }
    return "";
  }

  private get author(): string {
    if (typeof this.$route.query.search !== "undefined") {
      return String(this.$route.query.author || "");
    }
    return "";
  }

  created() {
    this.loadData();
    this.$watch("search", () => this.loadData());
    this.$watch("author", () => this.loadData());
  }

  async loadData() {
    this.items = [];
    try {
      const response = await BooksService.getBooks(this.search, "", 0);
      this.items = response;
    } catch (ex) {
      this.items = [];
    }
  }

  searchSubmit(search: string) {
    this.$router.push({
      path: this.$route.path,
      query: {
        search: search
      }
    });
  }
}
</script>

<style scoped>
.flex-row > a {
  margin: 4px;
}
</style>