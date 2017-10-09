import {Config} from "./baseConfig"
import {PagingList} from './commons'
import axios from '../myAxios'

interface Role{
    id:string,
    roleName:string,
    descript:string,
    createTime:Date
}

export let roleService = {
    async searchRoles(roleName:string,pageIndex:number,pageSize:number):Promise<PagingList<Role>>{
        let path = `${Config.BaseUrl}/Roles?roleName=${roleName}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        let httpResult = await axios.get(path);
        return httpResult.data as PagingList<Role>
    },
    async changeRoleMember(roleId:string,memberIds:string[]){
        let path = `${Config.BaseUrl}/Role/${roleId}/user`
        await axios.put(path,memberIds);
    },
    async getRoleMember(roleId:string):Promise<Role>{
        let path = `${Config.BaseUrl}/Role/${roleId}/user`
        let httpResult = await axios.get(path);
        return httpResult.data;
    },
    async createRole(role:Role){
        let path =`${Config.BaseUrl}/Role`
        let httpResult = await axios.post(path,role)
    },
    async deleteRole(roleId:string){
        let path = `${Config.BaseUrl}/Role/${roleId}`
        await axios.delete(path);
    },
    async editRole(roleId:string,role:Role){
        let path = `${Config.BaseUrl}/Role/${roleId}`
        await axios.put(path,role);
    },
    async getRole(roleId:string):Promise<Role>{
        let path = `${Config.BaseUrl}/Role/${roleId}`
        let httpResult = await axios.get(path);
        return httpResult.data
    }
}