<template>
  <div>
    <content-table :items="items" :headers="headers" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import LibrariesService, { LibraryListItem } from '@/services/LibrariesService';
import ContentTable from '@/components/ContentTable';

@Component({
  components: {
    'content-table': ContentTable
  }
})
export default class Libraries extends Vue {
  private items: LibraryListItem[] = [];

  private headers: any[]= [
    { name: "Nazwa Biblioteki", fieldName: "name" },
    { name: "Miejscowość", fieldName: "locationName" },
    { name: "Ulica", fieldName: "locationStreet" }
  ];

  created() {
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

<style lang="scss">
.content-table {
  border-collapse: collapse;
  margin: auto;
  margin-top: 20px;
  font-size: 0.9em;
  min-width: 700px;
  border-radius: 8px 8px 0 0;
  overflow: hidden;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
  padding: 20px;
}
.content-table thead tr {
  background-color: var(--info-color);
  color: var(--main-white-color);
  text-align: left;
  font-weight: bold;
}
.content-table th,
.content-table td {
  padding: 12px 15px;
  text-align: left;
}
.content-table tbody tr {
  border-bottom: 1px solid #dddddd;
}
.content-table tbody tr:nth-of-type(even) {
  background-color: #f3f3f340;
}
.content-table tbody tr:last-of-type {
  border-bottom: 2px solid var(--info-color);
}
</style>
