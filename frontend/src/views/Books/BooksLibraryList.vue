<template>
  <content-table :items="items" :headers="headers" />
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator';
import ContentTable from "@/components/ContentTable.vue";
import BooksService, { BookListItemViewModel } from "@/services/BooksService.ts";

@Component({
  components:{
    'content-table': ContentTable
  }
})
export default class BooksLibraryList extends Vue {
  private items: BookListItemViewModel[] = [];
  private headers: any[] = [
    { name: "Tytuł książki", fieldName: "title" },
    { name: "Autor", fieldName: "authorFullName" }
  ];
  async created() {
    this.loadData();
  }

  private get libraryId(): number {
    return Number(this.$route.params.libraryId || 0);
  }

  async loadData() {
    this.items = [];

    try {
      const response = await BooksService.getLibraryBooksList(this.libraryId);
      this.items = response;
    } 
    catch (ex) {
      this.items = [];
    }
  }
}
</script>
