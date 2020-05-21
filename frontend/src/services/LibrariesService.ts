import Vue from 'vue';

export default class LibrariesService{
    public static async getList(): Promise<LibraryListItem[]>
    {
        return (await Vue.axios.get<LibraryListItem[]>('libraries')).data;
    }
}

export interface LibraryListItem{
    id: number;
    name: string;
    locationName: string;
    locationStreet: string;
}