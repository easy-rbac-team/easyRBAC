import axios from '../myAxios'
import {Config} from './baseConfig'


enum ResourceType{
    Resource=1,
    Menu=2,
    Public = 4
}

export interface AppResource{
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
    disabled:boolean;
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
    async changeRoleResources(roleId:string,appId:string,resourceLst:string[]){
        let path =`${Config.BaseUrl}/AppResource/role/${roleId}/${appId}`;
        let httpResult = await axios.put(path,resourceLst);        
    },   
    setResourceDisable(tree:TreeStruct[],resourceIds:string[]){
        this.ergodicTree(tree,x=>x.disabled = resourceIds.some(y=>y===x.id))
    },
    ergodicTree(tree:TreeStruct[],action:((x:TreeStruct)=>void)){
        for(let item of tree){            
            action(item)
            if(item.children!=null&&item.children!.length>0){
                this.ergodicTree(item.children,action);
            }
        }
    }
}

