import Vue from 'vue';
import { Statement } from './AuthService';

export default class LocationsService{
    public static async getList(): Promise<LocationFormModel[]>
    {
        return (await Vue.axios.get<LocationFormModel[]>('locations')).data;
    }

    public static async getLocation(id: number) : Promise<LocationFormModel>
    {
        return (await Vue.axios.get<LocationFormModel>(`locations/${id}`)).data;
    }

    public static async postLocation(model: LocationFormModel ) : Promise<LocationFormModel>
    {
        return (await Vue.axios.post<LocationFormModel>('locations', model)).data;
    }

    public static async putLocation(model: LocationFormModel, id: number ) : Promise<Statement>
    {
        return (await Vue.axios.put<Statement>(`locations/${id}`, model)).data;
    }

    public static async deleteLocation(id: number) : Promise<Statement>
    {
        return (await Vue.axios.delete<Statement>(`locations/${id}`)).data;
    }
}

export interface LocationFormModel{
    locationId: number;
    name: string;
    zipCode: string;
    street: string;
}