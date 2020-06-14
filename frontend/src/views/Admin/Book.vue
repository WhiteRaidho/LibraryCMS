<template>
  <div>
    <div v-if="!editMode">{{ model.title }}</div>
    <form v-else>
      <input v-model="model.title" placeholder="Tytuł" type="text" />
      <input v-model="model.authorName" placeholder="Imię autora" type="text" />
      <input v-model="model.authorSurname" placeholder="Nazwisko autora" type="text" />
      <textarea v-model="model.description" placeholder="Opis" type="textarea" rows="3" />
      <input type="submit" value="Zapisz zmiany" />
    </form>
    <content-table :items="copies" :headers="headers" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import BooksService, { BookFormModel } from "@/services/BooksService";
import ContentTable from "@/components/ContentTable.vue";
import { library } from '@fortawesome/fontawesome-svg-core';

@Component({
  components: {
    ContentTable
  }
})
export default class AdminBook extends Vue {
  private editMode = true;
  private model: BookFormModel = {
    title: "",
    authorName: "",
    authorSurname: "",
    description: "",
    bookCopies: []
  };

  private copies: {id: number, library: string}[] = [];

  private headers = [
    {
      name: "Id", fieldName: "id",
      contentClass: "none-decoration"
    },
    {
      name: "Biblioteka", fieldName: "library",
      contentClass: "none-decoration"
    },
    {name: "", link: "/home", ico: "fas fa-trash", columnClass: "action-column"},
    // {name: "", ico: "fas fa-pencil-alt", columnClass: "action-column"}
  ];

  private get bookId(): number {
    return Number(this.$route.params.bookId || 0);
  }

  private get author(): string {
    return String(this.$route.params.author);
  }

  private get title(): string {
    return String(this.$route.params.title);
  }

  created() {
    this.loadData();
    // this.loadReviews();
  }

  async loadData() {
    try {
      const response = await BooksService.getBookForm(this.title, this.author);
      this.model = response;
      console.log(response);
      this.copies = this.model.bookCopies.map(x => { return {id: x.bookId, library: x.library.name}});
      console.log(this.copies);
    } catch (ex) {
      this.model = {
        title: "",
        authorName: "",
        authorSurname: "",
        description: "",
        bookCopies: []
      };
      this.copies = [];
    }
  }
}
</script>

<style scoped>
form > input {
  margin-bottom: 8px;
}


</style>