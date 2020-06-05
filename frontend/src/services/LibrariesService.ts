import Vue from 'vue';

export default class LibrariesService{
    public static async getList(): Promise<LibraryListItem[]>
    {
        return (await Vue.axios.get<LibraryListItem[]>('libraries')).data;
    }

    // public static async getLibrary(id: number) : Promise<LibraryListItem>
    // {
    //     return (await Vue.axios.get<LibraryListItem>(`libraries/${id}`)).data;
    // }
}

export interface LibraryListItem{
    id: number;
    name: string;
    locationName: string;
    locationStreet: string;
}