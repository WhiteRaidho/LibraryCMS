<template>
  <div>
    <div class="m8">
      <div class="font-2x f-left title-header">Pracownicy</div>
      <router-link class="none-decoration button f-right p8" to="/admin/librarians/new">Dodaj</router-link>
    </div>
    <!-- <search-bar text="Podaj nazwę biblioteki" class="m8" /> -->

    <div class="m8">
      <content-table :items="items" :headers="headers" @deleteItem="deleteItem" />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import RolesService, { RoleListItem } from "@/services/RolesService";

import ContentTable from "@/components/ContentTable.vue";

@Component({
  components: {
    ContentTable
  }
})
export default class AdminBooks extends Vue {
  private headers = [
    { name: "Id", fieldName: "roleId" },
    {
      name: "Użytkownik",
      fieldName: "username",
      contentClass: "none-decoration"
    },
    { name: "Biblioteka", fieldName: "libraryName" },
    { name: "Stanowisko", fieldName: "userRoleName" },
    { name: "", ico: "fas fa-trash", emitOnClick: "deleteItem", columnClass: "action-column", contentClass: "pointer" }
  ];

  private items: RoleListItem[] = [];
  private roles: {value: number, text: string}[] = [
    {value: 0, text: "Użytkownik"},
    {value: 1, text: "Bibliotekarz"},
    {value: 2, text: "Manager"}
  ];

  created() {
    this.loadData();
  }

  async loadData() {
    try {
      const response = await RolesService.getList();
      this.items = response;
      this.items.forEach(element => {
        element.userRoleName = this.roles[element.userRole].text;
      });
    } catch(ex) {
      this.items = [];
    }
  }

  async deleteItem(item: RoleListItem) {
    try {
      await RolesService.deleteRole(item.roleId);
      this.loadData();
    } catch(ex) {
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