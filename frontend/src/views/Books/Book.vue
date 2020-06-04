<template>
  <div class="center-tab semi-transparent flex-container flex-wrap">
    <div class="left">
      <img src="@/assets/book_cover.jpg" />
    </div>
    <div class="right">
      <div class="title">{{ book.title }}</div>
          <router-link class="author" :to="{path: '/books', query: {author: book.authorFullName, search: '', lib: ''}}">{{ book.authorFullName }}</router-link>
      <div class="description">{{ book.description }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BooksService, { BookViewModel } from "@/services/BooksService";

@Component({
  components: {}
})
export default class Book extends Vue {
  private book: BookViewModel = {
    title: "",
    authorFullName: "",
    description: ""
  };

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
  }

  async loadData() {
    try {
      const response = await BooksService.getBook(this.title, this.author);
      this.book = response;
    } catch (ex) {
      this.book = {
        title: "Not found",
        authorFullName: "Not Found",
        description: ""
      };
    }
  }
}
</script>

<style scoped>

.right {
    float: right;
    text-align: left;
}

.left {
    float: left;
    padding-right: 16px;
    margin: auto;
}

.left > img {
    width: 100%;
    max-width: 300px;
}

.title {
    font-size: 70px;
}

.author {
    font-size: 20px;
    color: var(--info-color);
    text-decoration: none;
}

.description {
    padding-top: 32px;
}
</style>