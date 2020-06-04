<template>
  <div class="semi-transparent center-tab books">
    <search-bar text="Podaj tytuł książki lub autora..." @submit="searchSubmit" />
    <div v-if="author || search || libraryId" class="filters">
      <div v-if="search" class="filter">
        Wyszukiwanie: {{ search }}
        <span v-on:click="resetFilter('search')">
          <i class="fas fa-times-circle pointer"></i>
        </span>
      </div>
      <div v-if="author" class="filter">
        Autor: {{ author }}
        <span v-on:click="resetFilter('author')">
          <i class="fas fa-times-circle pointer"></i>
        </span>
      </div>
      <div v-if="libraryId" class="filter">
        Biblioteka: {{ libraryName }}
        <span v-on:click="resetFilter('libraryId')">
          <i class="fas fa-times-circle pointer"></i>
        </span>
      </div>
    </div>
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
import LibrariesService from "@/services/LibrariesService";

@Component({
  components: {
    "content-table": ContentTable,
    "search-bar": SearchBar
  }
})
export default class BooksLibraryList extends Vue {
  private items: BookListItemViewModel[] = [];
  private headers: any[] = [
    {
      name: "Tytuł książki",
      fieldName: "title",
      link: "/books/{authorFullName}/{title}",
      styleClass: "none-decoration font-lg"
    },
    {
      name: "Autor",
      fieldName: "authorFullName",
      link: "/books?search=[search]&author={authorFullName}&lib=[lib]",
      styleClass: "none-decoration"
    }
  ];
  private libraryName = "";

  async created() {
    this.loadData();
    this.$watch("author", () => this.loadData());
    this.$watch("search", () => this.loadData());
    this.$watch("lib", () => this.loadData());
  }

  private get libraryId(): number {
    if (typeof this.$route.query.lib !== "undefined") {
      return Number(this.$route.query.lib || 0);
    }
    return 0;
  }

  private get author(): string {
    if (typeof this.$route.query.search !== "undefined") {
      return String(this.$route.query.author || "");
    }
    return "";
  }

  private get search() {
    if (typeof this.$route.query.search !== "undefined") {
      return String(this.$route.query.search || "");
    }
    return "";
  }

  async loadData() {
    this.items = [];
    this.libraryName = "";
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
    if (this.libraryId > 0) {
      try {
        const response = await LibrariesService.getLibrary(this.libraryId);
        this.libraryName = response.name;
      } catch (ex) {
        this.libraryName = "";
      }
    }
  }

  searchSubmit(search: string) {
    this.$router.push({
      path: this.$route.path,
      query: {
        search: search,
        author: String(this.author),
        lib: String(this.libraryId)
      }
    });
  }

  resetFilter(filter: string) {
    let search = this.search;
    let author = this.author;
    let libraryId = this.libraryId;
    switch (filter) {
      case "search":
        search = "";
        break;
      case "libraryId":
        libraryId = 0;
        break;
      case "author":
        author = "";
    }

    this.$router.push({
      path: this.$route.path,
      query: {
        search: search,
        author: author,
        lib: String(libraryId)
      }
    });
  }
}
</script>

<style scoped>
.books {
  width: 70%;
}

.filters {
}

.filter {
  margin-top: 8px;
  margin-right: 8px;
  padding: 4px 8px;
  border-radius: 300px;
  text-align: left;
  background-color: var(--info-color);
  color: var(--main-white-color);
  display: inline-block;
}

.pointer {
  cursor: pointer;
}
</style>
