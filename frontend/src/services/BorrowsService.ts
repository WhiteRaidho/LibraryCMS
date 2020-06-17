import Vue from 'vue';
import { Statement } from './AuthService';

export default class BorrowsService{
    public static async getList(search: string): Promise<BorrowListItemModel[]>
    {
        return (await Vue.axios.get<BorrowListItemModel[]>('borrows',{
            params: {
                search: search
            }}
            )).data;
    }

    public static async getBorrow(id: number) : Promise<BorrowFormModel>
    {
        return (await Vue.axios.get<BorrowFormModel>(`borrows/${id}`)).data;
    }

    public static async postBorrow(model: BorrowFormModel) : Promise<BorrowFormModel>
    {
        return (await Vue.axios.post<BorrowFormModel>(`borrows`, model)).data;
    }

    public static async ReturnBook(id: number) : Promise<Statement>
    {
        return (await Vue.axios.put<Statement>(`borrows/${id}`)).data;
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

export interface BorrowListItemModel {
    borrowId: number,
    title: string,
    authorFullName: string,
    firstName: string,
    lastName: string
}
