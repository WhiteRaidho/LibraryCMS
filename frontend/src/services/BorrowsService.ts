import Vue from 'vue';
import { Statement } from './AuthService';

export default class BorrowsService{
    public static async getList(): Promise<BorrowFormModel[]>
    {
        return (await Vue.axios.get<BorrowFormModel[]>('borrows')).data;
    }

    public static async getBorrow(id: number) : Promise<BorrowFormModel>
    {
        return (await Vue.axios.get<BorrowFormModel>(`borrows/${id}`)).data;
    }

    public static async postBorrow(model: BorrowFormModel) : Promise<BorrowFormModel>
    {
        return (await Vue.axios.post<BorrowFormModel>(`borrows`, model)).data;
    }

    public static async putBorrow(model: BorrowFormModel, id: number) : Promise<Statement>
    {
        return (await Vue.axios.put<Statement>(`borrows/${id}`, model)).data;
    }

    public static async deleteBorrow(id: number) : Promise<Statement>
    {
        return (await Vue.axios.delete<Statement>(`borrows/${id}`)).data;
    }
}

export interface BorrowFormModel{
    borrowId: number,
    librarianUserId: string,
    returnLibrarianUserId: string,
    userId: string,
    bookId: number,
    borrowTime: Date,
    returnTime: Date | null,
    status: number
}