import Vue from 'vue';
import { Statement } from './AuthService';

export default class RolesService{
    public static async getList(): Promise<RoleListItem[]>
    {
        return (await Vue.axios.get<RoleListItem[]>('roles')).data;
    }

    public static async getRole(id: number) : Promise<RoleFormModel>
    {
        return (await Vue.axios.get<RoleFormModel>(`roles/${id}`)).data;
    }

    public static async postRole(model: RoleFormModel) : Promise<RoleFormModel>
    {
        return (await Vue.axios.post<RoleFormModel>(`roles`, model)).data;
    }

    public static async putRole(model: RoleFormModel, id: number) : Promise<Statement>
    {
        return (await Vue.axios.put<Statement>(`roles/${id}`, model)).data;
    }

    public static async deleteRole(id: number) : Promise<Statement>
    {
        return (await Vue.axios.delete<Statement>(`roles/${id}`)).data;
    }
}

export interface RoleFormModel{
    roleId: number;
    userId: string;
    libraryId: number;
    userRole: number;
}

export interface RoleListItem {
    roleId: number;
    username: string;
    libraryName: string;
    userRole: number;
    userRoleName: string;
}