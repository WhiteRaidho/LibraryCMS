import Vue from 'vue';

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
}

export interface BookListItemViewModel{
    title: string;
    authorFullName: string;
}