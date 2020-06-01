<template>
  <div class="semi-transparent center-tab books">
    <search-bar text="Podaj tytuł książki lub autora..." @submit="searchSubmit" />
    <content-table :items="items" :headers="headers" />
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import ContentTable from "@/components/ContentTable.vue";
import SearchBar from "@/components/SearchBar.vue";
import BooksService, {
  BookListItemViewModel
} from "@/services/BooksService.ts";

@Component({
  components: {
    "content-table": ContentTable,
    "search-bar": SearchBar
  },
  watch: {}
})
export default class BooksLibraryList extends Vue {
  private items: BookListItemViewModel[] = [];
  private headers: any[] = [
    { name: "Tytuł książki", fieldName: "title", link: "/books/{title}" },
    {
      name: "Autor",
      fieldName: "authorFullName",
      link: "/books?search=[search]&author={authorFullName}&lib=[lib]"
    }
  ];
  async created() {
    this.loadData();
    this.$watch("author", () => this.loadData());
    this.$watch("search", () => this.loadData());
    this.$watch("lib", () => this.loadData());
  }

  private get libraryId(): number {
    return Number(this.$route.query.lib || 0);
  }

  private get author(): string {
    return String(this.$route.query.author || "");
  }

  private get search() {
    return String(this.$route.query.search || "");
  }

  async loadData() {
    this.items = [];
    try {
      const response = await BooksService.getBooks(
        this.search,
        this.author,
        this.libraryId
      );
      this.items = response;
    } catch (ex) {
      this.items = [];
    }
  }

  searchSubmit(search: string) {
    console.log(this.$route.path);
      this.$router.push({
        path: this.$route.path, 
        query: {search: search, author: String(this.author), lib: String(this.libraryId)}
      });
  }
}
</script>

<style scoped>
.books {
  width: 70%;
}
</style>
