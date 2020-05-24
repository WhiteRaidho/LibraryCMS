<template>
  <div>
    <content-table :items="items" :headers="headers" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import LibrariesService, { LibraryListItem } from '@/services/LibrariesService';
import ContentTable from '@/components/ContentTable.vue';

@Component({
  components: {
    'content-table': ContentTable
  }
})
export default class Libraries extends Vue {
  private items: LibraryListItem[] = [];

  private headers: any[] = [
    { name: "Nazwa Biblioteki", fieldName: "name", link:"/library/{libraryID}/books" },
    { name: "Miejscowość", fieldName: "locationName" },
    { name: "Ulica", fieldName: "locationStreet" }
  ];

  async created() {
    this.loadData();
  }

  async loadData() {
    this.items = [];

    try {
      const response = await LibrariesService.getList();
      this.items = response;
    } 
    catch (ex) {
      this.items = [];
    }
  }
}
</script>
