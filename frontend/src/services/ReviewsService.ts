import Vue from 'vue';

export default class ReviewsService {
    public static async getReviewsForBook(bookTitle: string, authorFullName: string): Promise<ReviewViewModel[]>
    {
        return (await Vue.axios.get<ReviewViewModel[]>(`/reviews/${authorFullName}/${bookTitle}`)).data;
    }
}

export interface ReviewViewModel{
    rate: number;
    description: string;
    userName: string;
}