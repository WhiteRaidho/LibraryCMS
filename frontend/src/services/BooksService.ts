import Vue from 'vue';
import { LibraryListItem } from '@/services/LibrariesService';
import { Statement } from './AuthService';

export default class BooksService{
    public static async getLibraryBooksList(libraryId: number): Promise<BookListItemViewModel[]>
    {
        return (await Vue.axios.get<BookListItemViewModel[]>(`/books`)).data;
    }

    public static async getBooks(search: string, author: string, lib: number) : Promise<BookListItemViewModel[]>
    {
        return (await Vue.axios.get<BookListItemViewModel[]>(`/books`, {
            params: {
                search: search,
                author: author,
                lib: lib
            }
        })).data;
    }

    public static async getBook(title: string, authorFullName: string) : Promise<BookViewModel>
    {
        return (await Vue.axios.get<BookViewModel>(`/books/${authorFullName}/${title}`)).data;
    }

    public static async getBookForm(title: string, authorFullName: string) : Promise<BookFormModel>
    {
        return (await Vue.axios.get<BookFormModel>(`/admin/books/${authorFullName}/${title}`)).data;
    }

    public static async getBookCopies(title: string, authorFullName: string) : Promise<BookCopies[]>
    {
        return (await Vue.axios.get<BookCopies[]>(`/admin/books/${authorFullName}/${title}/copies`)).data;
    }

    public static async postBookForm(model: BookFormModel) : Promise<BookFormModel>
    {
        return (await Vue.axios.post<BookFormModel>(`/admin/books`, model)).data;
    }

    public static async putBookForm(model: BookFormModel) : Promise<BookFormModel>
    {
        return (await Vue.axios.put<BookFormModel>(`/admin/books`, model)).data;
    }

    public static async deleteBook(id: number) : Promise<Statement>
    {
        return (await Vue.axios.delete<Statement>(`/admin/books/${id}`)).data;
    }

    public static async getAvalibleBooks(search: string, author: string) : Promise<AvalibleBookListItemViewModel[]>
    {
        return (await Vue.axios.get<AvalibleBookListItemViewModel[]>(`/avalibleBooks`, {
            params: {
                search: search,
                author: author
            }
        })).data;
    }
}

export interface BookListItemViewModel{
    title: string;
    authorFullName: string;
    copies: number;
}

export interface BookViewModel {
    title: string;
    authorFullName: string;
    description: string;
    avgRating: number;
}

export interface BookFormModel {
    title: string;
    authorName: string;
    authorSurname: string;
    description: string;
    bookCopies: BookCopies[];
}

export interface BookCopies {
    bookId: number;
    library: LibraryListItem;
}

export interface AvalibleBookListItemViewModel {
    bookId: number;
    title: string;
    AuthorFullName: string;
}
