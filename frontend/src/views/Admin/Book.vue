<template>
  <div>
    <form @submit.prevent="onSubmit" >
      <input v-model="model.title" placeholder="Tytuł" type="text" />
      <input-error v-bind:errors="err.Title" />
      <input v-model="model.authorName" placeholder="Imię autora" type="text" />
      <input-error v-bind:errors="err.AuthorName" />
      <input v-model="model.authorSurname" placeholder="Nazwisko autora" type="text" />
      <input-error v-bind:errors="err.AuthorSurname" />
      <textarea v-model="model.description" placeholder="Opis" type="textarea" rows="3" />
      <input-error v-bind:errors="err.Description" />
      <input type="submit" value="Zapisz zmiany" />
    </form>
    <div class="flex-container flex-row">
      <select v-model="selectedLibrary" :options="libraries" class="f-left">
        <option v-for="lib in libraries" v-bind:value="lib" :key="lib.libraryID">{{ lib.name }}</option>
      </select>
      <button class="f-right" v-on:click="addCopie">Dodaj</button>
    </div>
    <content-table :items="copies" :headers="headers" @deleteBook="deleteCopie" />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import BooksService, { BookFormModel, BookCopies } from "@/services/BooksService";
import LibrariesService, { LibraryListItem } from "@/services/LibrariesService";
import ContentTable from "@/components/ContentTable.vue";
import Book from "../Books/Book.vue";
import InputError from "@/components/InputError.vue";
import CustomSelect from "@/components/CustomSelect.vue";

@Component({
  components: {
    ContentTable,
    InputError,
    CustomSelect
  }
})
export default class AdminBook extends Vue {
  private err: any;
  private model: BookFormModel = {
    title: "",
    authorName: "",
    authorSurname: "",
    description: "",
    bookCopies: []
  };

  private libraries: LibraryListItem[] = [];
  private selectedLibrary: LibraryListItem = {
    libraryID: 0,
    name: "Brak bibliotek w bazie danych",
    locationName: "",
    locationStreet: ""
  };

  private copies: { id: number; library: string }[] = [];

  private headers = [
    {
      name: "Id",
      fieldName: "id",
      contentClass: "none-decoration"
    },
    {
      name: "Biblioteka",
      fieldName: "library",
      contentClass: "none-decoration"
    },
    {
      name: "",
      ico: "fas fa-trash",
      emitOnClick: "deleteBook",
      contentClass: "pointer",
      columnClass: "action-column"
    }
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
    this.err = {
      Title: [],
      AuthorName: [],
      AuthorSurname: [],
      Description: [],
    };

    this.loadLibraries();
    if (this.author !== "undefined" && this.title !== "undefined")
      this.loadData();
    // this.loadReviews();
  }

  async loadLibraries() {
    try {
      const response = await LibrariesService.getList();
      this.libraries = response;
      if (this.libraries.length > 0) this.selectedLibrary = this.libraries[0];
      else
        this.selectedLibrary = {
          libraryID: 0,
          name: "Brak bibliotek w bazie danych",
          locationName: "",
          locationStreet: ""
        };
    } catch (ex) {
      this.libraries = [];
      this.selectedLibrary = {
        libraryID: 0,
        name: "Brak bibliotek w bazie danych",
        locationName: "",
        locationStreet: ""
      };
    }
  }

  async loadData() {
    try {
      const response = await BooksService.getBookForm(this.title, this.author);
      this.model = response;
      this.mapLibraries();
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

  mapLibraries() {
    this.copies = this.model.bookCopies.map(x => {
      return { id: x.bookId, library: x.library.name };
    });
  }

  async onSubmit() {
    try {
      if (this.author === "undefined" || this.title === "undefined") {
        const response = await BooksService.postBookForm(this.model);
        this.model = response;
        this.mapLibraries();
        this.$router.push(
          `${this.model.authorName} ${this.model.authorSurname}/${this.model.title}`
        );
      } else {
        const response = await BooksService.putBookForm(this.model);
        this.model = response;
        this.mapLibraries();
        this.$router.push(
          `/admin/books/${this.model.authorName} ${this.model.authorSurname}/${this.model.title}`
        );
      }
    } catch (ex) {
      this.err = ex.errors;
      this.$forceUpdate();
    }
  }

  addCopie() {
    this.model.bookCopies.push({ bookId: 0, library: this.selectedLibrary });
    this.mapLibraries();
  }

  async deleteCopie(item: any) {
    const toRemove = this.model.bookCopies.find(obj => obj.bookId == item.id && obj.library.name == item.library);
    if(toRemove === undefined) return;
    const index = this.model.bookCopies.indexOf(toRemove);
    this.model.bookCopies.splice(index, 1);
    if(item.id != 0) {
      await BooksService.deleteBook(item.id);
    }
    this.mapLibraries();
  }
}
</script>

<style scoped>
form > div, form > input {
  margin-bottom: 8px;
}
</style>