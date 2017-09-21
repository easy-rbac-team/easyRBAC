import axios from 'axios'
import {Config} from './baseConfig'

enum ResourceType{
    Resource=1,
    Menu=2,
    Public = 4
}

interface AppResource{
    id:string,
    applicationId:string,    
    resourceName:string,
    resourceCode:string,
    iconUrl:string,
    parameters:string,
    describe:string,
    children:AppResource[],
    url:string
    resourceType:ResourceType
}



interface TreeStruct{
    id:string;
    label:string;
    children:TreeStruct[]
}

export let resourceService={
    async addResource(parentId:string,appResource:AppResource){
        let path = `${Config.BaseUrl}/AppResource/${parentId}`;
        await axios.post(path,appResource);
    },
    async disableResource(id:string){
        let path = `${Config.BaseUrl}/AppResource/${id}`;
        await axios.delete(path);
    },
    async editResource(id:string,appResource:AppResource){
        let path = `${Config.BaseUrl}/AppResource/${id}`
        await axios.put(path,appResource);
    },
    async getResouceInfo(id:string):Promise<AppResource>{
        let path = `${Config.BaseUrl}/AppResource/${id}`
        let httpResult = await axios.get(path);
        return httpResult.data;
    },
    async getAppResource(appId:string):Promise<AppResource[]>{
        let path = `${Config.BaseUrl}/AppResource/app/${appId}`
        let httpResult = await axios.get(path);
        let result = httpResult.data as AppResource[]
        return result;
        
    },
    async getRoleResourceIds(roleId:string,appId:string):Promise<string[]>{
        let path = `${Config.BaseUrl}/AppResource/role/${roleId}/${appId}`
        let httpResult = await axios.get(path);
        return httpResult.data as string[]
    },
    async changeRoleResources(roleId:string,resourceLst:string[]){
        let path =`${Config.BaseUrl}/AppResource/role/${roleId}`;
        let httpResult = await axios.put(path,resourceLst);        
    }
}

