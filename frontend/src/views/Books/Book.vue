<template>
  <div>
    <div class="center-tab semi-transparent flex-container flex-wrap">
      <div class="left">
        <img src="@/assets/book_cover.jpg" />
      </div>
      <div class="right">
        <div class="font-8x">{{ book.title }}</div>
        <router-link
          class="author"
          :to="{path: '/books', query: {author: book.authorFullName, search: '', lib: ''}}"
        >{{ book.authorFullName }}</router-link>
        <div class="description">{{ book.description }}</div>
      </div>
    </div>
    <div class="semi-transparent center-tab books">
      <div class="font-4x">Recenzje
        <!-- <stars :changeable='true' :rate='rate' @changeRating="changeRating" class="font-8x" :key="rate"/> -->
      </div>
      <content-table :items="reviews" :headers="headers" />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BooksService, { BookViewModel } from "@/services/BooksService";
import ReviewsService, { ReviewViewModel } from "@/services/ReviewsService";
import ContentTable from "@/components/ContentTable.vue";
import Stars from "@/components/Reviews/Stars.vue"

@Component({
  components: {
    ContentTable,
    Stars
  }
})
export default class Book extends Vue {

  private rate = 2;

  private book: BookViewModel = {
    title: "",
    authorFullName: "",
    description: ""
  };

  private headers: any[] = [
    {
      name: "Autor",
      fieldName: "userName"
    },
    {
      name: "Ocena",
      fieldName: "rate"
    },
    {
      name: "Opis",
      fieldName: "description"
    }
  ];

  private reviews: ReviewViewModel[] = [];


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
    // Load book data
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
    // Load reviews for Book
    try {
      const response = await ReviewsService.getReviewsForBook(this.title, this.author);
      this.reviews = response;
    } catch (ex) {
      this.reviews = [];
    }
  }

  changeRating(rate: number) {
    this.rate = rate;
    console.log(this.rate);
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


.author {
  font-size: 20px;
  color: var(--info-color);
  text-decoration: none;
}

.description {
  padding-top: 32px;
}
</style>