import {Config} from "./baseConfig.ts"
import {PagingList} from './commons.ts'
import axios from 'axios'

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
    async createRole(user:Role){
        let path =`${Config.BaseUrl}/Role`
        let httpResult = await axios.post(path,user)
    },
    async deleteRole(userId:string){
        let path = `${Config.BaseUrl}/Role/${userId}`
        await axios.delete(path);
    },
    async editRole(userId:string,user:Role){
        let path = `${Config.BaseUrl}/Role/${userId}`
        await axios.put(path,user);
    }
}