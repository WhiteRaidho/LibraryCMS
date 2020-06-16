import Vue from 'vue';
import { Statement } from './AuthService';

export default class LibrariesService{
    public static async getList(): Promise<LibraryListItem[]>
    {
        return (await Vue.axios.get<LibraryListItem[]>('libraries')).data;
    }

    public static async getLibrary(id: number) : Promise<LibraryFormModel>
    {
        return (await Vue.axios.get<LibraryFormModel>(`libraries/${id}`)).data;
    }

    public static async postLibrary(model: LibraryFormModel) : Promise<LibraryFormModel>
    {
        return (await Vue.axios.post<LibraryFormModel>(`libraries`, model)).data;
    }

    public static async putLibrary(model: LibraryFormModel, id: number) : Promise<Statement>
    {
        return (await Vue.axios.put<Statement>(`libraries/${id}`, model)).data;
    }

    public static async deleteLibrary(id: number) : Promise<Statement>
    {
        return (await Vue.axios.delete<Statement>(`libraries/${id}`)).data;
    }
}

export interface LibraryListItem{
    libraryID: number;
    name: string;
    locationName: string;
    locationStreet: string;
}

export interface LibraryFormModel {
    libraryId: number;
    name: string;
    locationId: number;
}