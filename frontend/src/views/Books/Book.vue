<template>
  <div>
    <div class="center-tab semi-transparent">
      <div class="flex-container flex-wrap container">
        <div class="left">
          <img src="@/assets/book_cover.jpg" />
        </div>
        <div class="right">
          <div class="font-8x">{{ book.title }}</div>
          <router-link
            class="author"
            :to="{path: '/books', query: {author: book.authorFullName, search: '', lib: ''}}"
          >{{ book.authorFullName }}</router-link>
          <div>
            <stars :changeable="false" :rate="book.avgRating" :key="book.avgRating" />
            <span class="rating">{{ book.avgRating.toFixed(1) }}/5</span>
            </div>
          <div class="description">{{ book.description }}</div>
        </div>
      </div>
    </div>
    <div class="semi-transparent center-tab books">
      <div class="font-4x">
        Recenzje
      </div>
      <div class="p8" v-if="canWriteReview">
        <form name="review">
          <input type="text" placeholder="Opinia"/>
        </form>
      </div>
      <review v-for="(item, index) in reviews" :key="index" :model="item" />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BooksService, { BookViewModel } from "@/services/BooksService";
import ReviewsService, { ReviewViewModel } from "@/services/ReviewsService";
import ContentTable from "@/components/ContentTable.vue";
import Review from "@/components/Reviews/Review.vue";
import Stars from "@/components/Reviews/Stars.vue";

@Component({
  components: {
    ContentTable,
    Review,
    Stars
  }
})
export default class Book extends Vue {

  private book: BookViewModel = {
    title: "",
    authorFullName: "",
    description: "",
    avgRating: 0  
  };

  private reviews: ReviewViewModel[] = [];
  private canWriteReview = false;

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
        description: "",
        avgRating: 0
      };
    }
    // Load reviews for Book
    try {
      const response = await ReviewsService.getReviewsForBook(
        this.title,
        this.author
      );
      this.reviews = response;
    } catch (ex) {
      this.reviews = [];
    }
    // Check if can write review
    if(this.$auth.check()) {
      try {
        const response = await ReviewsService.canWriteReview(this.title, this.author);
        this.canWriteReview = response;
      } catch (ex) {
        this.canWriteReview = false;
      }
    }
  }
}
</script>

<style scoped>
.container {
  justify-content: space-evenly;
  margin: auto;
}

.right {
  float: right;
  text-align: left;
}

.left {
  float: left;
  padding-right: 16px;
  /* margin: auto; */
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
  text-align: justify;
}

.rating {
  padding-left: 8px;
  font-weight: bold;
}
</style>