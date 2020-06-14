import Vue from 'vue';
import { LibraryListItem } from '@/services/LibrariesService';

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