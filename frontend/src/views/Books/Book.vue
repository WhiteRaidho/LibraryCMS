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
        <span class="font-lg">Czytałeś tę książkę, lecz jeszcze jej nie oceniłeś. Zrób to teraz!</span>
        <form name="review" class="flex-container flex-row" @submit.prevent="onSubmit">
          <stars class="font-8x m4" :noBackground="true" :changeable="true" :rate="review.rate" :key="review.rate" @changeRating="changeRating" />
          <input type="text" class="m4" v-model="review.description" placeholder="Opinia"/>
          <input type="submit" class="button m4" :disabled="formBlocked" value="Wyślij opinię"/>
        </form>
      </div>
      <review v-for="(item, index) in reviews" :key="index" :model="item" />
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BooksService, { BookViewModel } from "@/services/BooksService";
import ReviewsService, { ReviewViewModel, ReviewFormModel } from "@/services/ReviewsService";
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

  private review: ReviewFormModel = {
    description: "",
    rate: 3,
    id: 0
  };

  private reviews: ReviewViewModel[] = [];
  private canWriteReview = false;
  private formBlocked = false;

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
    this.loadReviews();
  }

  changeRating(x: number) {
    this.review.rate = x;
  }

  async onSubmit() {
    this.formBlocked = true;
    try {
     const response = await ReviewsService.postReviewForBook(this.title, this.author, this.review); 
     this.canWriteReview = false;
     this.reviews.push(response);
     this.loadData();
    } catch(ex) {
      console.log("Nie można dodać recenzji //TODO obsługa błędów");
    }
  }

  async loadData() {
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
  }

  async loadReviews() {
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