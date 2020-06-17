<template>
  <div class="books">
    <search-bar text="Podaj tytuł książki lub autora..." @submit="searchSubmit" />
    <div v-if="author || search" class="filters">
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
    </div>
    <content-table :items="items" :headers="headers" @selectBook="selectBook" v-if="!bookId"/>
    <content-table :items="userItems" :headers="userHeaders" @borrowBook="borrowBook" v-else/>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ContentTable from "@/components/ContentTable.vue";
import SearchBar from "@/components/SearchBar.vue";
import BooksService, { AvalibleBookListItemViewModel } from "@/services/BooksService";
import BorrowService, { BorrowFormModel } from "@/services/BorrowsService";
import AuthService, { AuthModel } from "@/services/AuthService";

@Component({
  components: {
    "content-table": ContentTable,
    "search-bar": SearchBar
  }
})
export default class Borrow extends Vue {
  private items: AvalibleBookListItemViewModel[] = [];
  private userItems: AuthModel[] = [];
  private bookId = 0;
  private model: BorrowFormModel = {
    borrowId: 0,
    librarianUserId: "",
    returnLibrarianUserId: "",
    userId: "",
    bookId: 0,
    borrowTime: new Date(),
    returnTime: new Date(),
    status: 0
  };
  private headers: any[] = [
    {
      name: "Id",
      fieldName: "bookId",
      contentClass: "none-decoration"
    },
    {
      name: "Tytuł książki",
      fieldName: "title",
      contentClass: "none-decoration font-lg"
    },
    {
      name: "Autor",
      fieldName: "authorFullName",
      contentClass: "none-decoration"
    },
    { name: "", ico: "fas fa-address-book", emitOnClick: "selectBook", columnClass: "action-column", contentClass: "pointer" }
  ];

  private userHeaders: any[] = [
    {
      name: "Imie",
      fieldName: "firstName",
      contentClass: "none-decoration"
    },
    {
      name: "Nazwisko",
      fieldName: "lastName",
      contentClass: "none-decoration"
    },
    {
      name: "Email",
      fieldName: "email",
      contentClass: "none-decoration"
    },
    { name: "", ico: "fas fa-arrow-alt-circle-right", emitOnClick: "borrowBook", columnClass: "action-column", contentClass: "pointer" }
  ];


  async created() {
    this.loadData();
    this.$watch("author", () => this.loadData());
    this.$watch("search", () => this.loadData());
  }

  async selectBook(item: AvalibleBookListItemViewModel) {
    this.bookId = item.bookId;
  }

  async borrowBook(item: AuthModel) {
    try {
      if (item != null) {
        this.model.userId = item.userId;
        this.model.bookId = this.bookId;
        await BorrowService.postBorrow(this.model);
        this.loadData();
      }
    } catch (er) {
      console.log(er);
    }
  }

  private isBookSelected() {
    if (this.bookId != 0) {
      return true;
    }
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
    this.userItems = [];
    this.bookId = 0;
    try {
      const response = await BooksService.getAvalibleBooks(
        this.search,
        this.author
      );
      const userResponse = await AuthService.getUserList();
      this.items = response;
      this.userItems = userResponse;
    } catch (ex) {
      this.items = [];
      this.userItems = [];
    }
  }

  searchSubmit(search: string) {
    this.$router.push({
      path: this.$route.path,
      query: {
        search: search,
        author: String(this.author)
      }
    });
  }

  resetFilter(filter: string) {
    let search = this.search;
    let author = this.author;
    switch (filter) {
      case "search":
        search = "";
        break;
      case "author":
        author = "";
    }

    this.$router.push({
      path: this.$route.path,
      query: {
        search: search,
        author: author
      }
    });
  }
}
</script>
<style scoped>
.filter {
  margin-top: 8px;
  margin-right: 8px;
  padding: 4px 8px;
  border-radius: 24px;
  text-align: left;
  background-color: var(--info-color);
  color: var(--main-white-color);
  display: inline-block;
}

.pointer {
  cursor: pointer;
}
</style>
