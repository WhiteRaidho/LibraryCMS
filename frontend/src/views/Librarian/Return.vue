<template>
  <div class="books">
    <search-bar text="Podaj tytuł książki, autora lub nazwisko wypożyczającego..." @submit="searchSubmit" />
    <div v-if="author || search || lastName" class="filters">
      <div v-if="search" class="filter">
        Wyszukiwanie: {{ search }}
        <span v-on:click="resetFilter('search')">
          <i class="fas fa-times-circle pointer"></i>
        </span>
      </div>
    </div>
    <content-table :items="items" :headers="headers" @returnBook="returnBook" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ContentTable from "@/components/ContentTable.vue";
import SearchBar from "@/components/SearchBar.vue";
import BorrowService, { BorrowListItemModel } from "@/services/BorrowsService";

@Component({
  components: {
    "content-table": ContentTable,
    "search-bar": SearchBar
  }
})
export default class Borrow extends Vue {
  private items: BorrowListItemModel[] = [];
  private headers: any[] = [
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
    {
      name: "Imię",
      fieldName: "firstName",
      contentClass: "none-decoration"
    },
    {
      name: "Nazwisko",
      fieldName: "lastName",
      contentClass: "none-decoration"
    },
    { name: "", ico: "fas fa-arrow-alt-circle-right", emitOnClick: "returnBook", columnClass: "action-column", contentClass: "pointer" }
  ];

  async created() {
    this.loadData();
    this.$watch("search", () => this.loadData());
  }

  async returnBook(item: BorrowListItemModel) {
    await BorrowService.ReturnBook(item.borrowId);
    this.loadData();
  }

  private get search() {
    if (typeof this.$route.query.search !== "undefined") {
      return String(this.$route.query.search || "");
    }
    return "";
  }

  async loadData() {
    this.items = [];
    try {
      const response = await BorrowService.getList(this.search);
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

  resetFilter(filter: string) {
    let search = this.search;
    switch (filter) {
      case "search":
        search = "";
    }

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
