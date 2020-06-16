<template>
  <div>
    <div class="m8">
      <div class="font-2x f-left title-header">Lokacje</div>
      <router-link class="none-decoration button f-right p8" to="/admin/locations/new">Dodaj</router-link>
    </div>
    <!-- <search-bar text="Podaj nazwę biblioteki" class="m8" /> -->

    <div class="m8">
      <content-table :items="items" :headers="headers" @deleteItem="deleteItem" />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import LocationsService, {
  LocationFormModel
} from "@/services/LocationsService";

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
    { name: "Id", fieldName: "locationId" },
    {
      name: "Miasto",
      fieldName: "name",
      contentClass: "none-decoration",
      link: "/admin/locations/{locationId}"
    },
    { name: "Kod pocztowy", fieldName: "zipCode" },
    { name: "Ulica", fieldName: "street" },
    { name: "", ico: "fas fa-trash", emitOnClick: "deleteItem", columnClass: "action-column", contentClass: "pointer" }
  ];

  private items: LocationFormModel[] = [];

  created() {
    this.loadData();
  }

  async loadData() {
    this.items = [];

    try {
      const response = await LocationsService.getList();
      this.items = response;
    } catch (ex) {
      this.items = [];
    }
  }

  async deleteItem(item: LocationFormModel) {
    try {
      await LocationsService.deleteLocation(item.locationId);
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