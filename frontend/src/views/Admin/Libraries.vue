<template>
  <div>
    <div class="m8">
      <div class="font-2x f-left title-header">Biblioteki</div>
      <router-link class="none-decoration button f-right p8" to="/admin/libraries/new">Dodaj</router-link>
    </div>
    <!-- <search-bar text="Podaj nazwę biblioteki" class="m8" /> -->

    <div class="m8">
      <content-table :items="items" :headers="headers" @deleteItem="deleteItem" />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import LibrariesService, { LibraryListItem } from "@/services/LibrariesService";

import SearchBar from "@/components/SearchBar.vue";
import ContentTable from "@/components/ContentTable.vue";

@Component({
  components: {
    SearchBar,
    ContentTable
  }
})
export default class AdminLibraries extends Vue {
  private headers = [
    { name: "Id", fieldName: "libraryID" },
    {
      name: "Nazwa",
      fieldName: "name",
      contentClass: "none-decoration",
      link: "/admin/libraries/{libraryID}"
    },
    { name: "Miasto", fieldName: "locationName" },
    { name: "Adres", fieldName: "locationStreet" },
    { name: "", ico: "fas fa-trash", emitOnClick: "deleteItem", columnClass: "action-column", contentClass: "pointer" }
  ];

  private items: LibraryListItem[] = [];

  created() {
    this.loadData();
  }

  async loadData() {
    this.items = [];

    try {
      const response = await LibrariesService.getList();
      this.items = response;
    } catch (ex) {
      this.items = [];
    }
  }

  async deleteItem(item: any) {
    try {
      await LibrariesService.deleteLibrary(item.libraryID);
      this.loadData();
    } catch (ex) {
      console.log(ex);
    }
  }
}
</script>

<style scoped>
.flex-row > a {
  margin: 4px;
}

.title-header {
  margin-bottom: 16px;
}
</style>