import Vue from 'vue';
import { Statement } from './AuthService';

export default class ReviewsService {
    public static async getReviewsForBook(bookTitle: string, authorFullName: string): Promise<ReviewViewModel[]>
    {
        return (await Vue.axios.get<ReviewViewModel[]>(`/reviews/${authorFullName}/${bookTitle}`)).data;
    }

    public static async postReviewForBook(bookTitle: string, authorFullName: string, model: ReviewFormModel) : Promise<ReviewViewModel>
    {
        return (await Vue.axios.post<ReviewViewModel>(`/reviews/${authorFullName}/${bookTitle}`, model)).data;
    }

    public static async canWriteReview(bookTitle: string, authorFullName: string) : Promise<boolean>
    {
        return (await Vue.axios.get<boolean>(`/reviews/${authorFullName}/${bookTitle}/check`)).data;
    }
}

export interface ReviewFormModel {
    id: number;
    rate: number;
    description: string;
}

export interface ReviewViewModel{
    rate: number;
    description: string;
    userName: string;
}