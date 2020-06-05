<template>
    <div class="review-container">
        <div class="review-header">
            <stars :changable="false" :rate="model.rate" />
            przez <span class="review-username">{{ model.userName }}</span>
        </div>
        <div class="review-description">
            {{ model.description }}
        </div>
    </div>
</template>

<script lang="ts">
import { Vue, Component, Prop } from 'vue-property-decorator';
import Stars from '@/components/Reviews/Stars.vue'
import { ReviewViewModel } from '@/services/ReviewsService';

@Component({
    name: "review",
    inheritAttrs: false,
    components: {
        Stars
    }
})

export default class Review extends Vue {
    @Prop() model!: ReviewViewModel;
}
</script>

<style scoped>
.review-container {
    text-align: left;
    margin: 8px;
    padding: 8px;
    background-color: var(--main-white);
    backdrop-filter: blur(8px);
    border-radius: 6px;
}

.review-header {
    padding-bottom: 8px;
}

.review-description {
    text-align: justify;
}

.review-username {
    color: var(--main-light-gray-color);
    font-weight: bold;
}
</style>