import Vue from 'vue';

export default class BooksService{
    public static async getLibraryBooksList(libraryId: number): Promise<BookListItemViewModel[]>
    {
        return (await Vue.axios.get<BookListItemViewModel[]>(`${libraryId}/books`)).data;
    }
}

export interface BookListItemViewModel{
    title: string;
    authorFullName: string;
}