import {Config} from './baseConfig'
import axios from '../myAxios'
import {AppResource} from './resourceService'

interface AppAndResource{
    appId:string,
    appName:string,
    appCode:string,
    appRsources:AppResource[]
}

export let managerScopeService={
    async getManagedScopeIds(userId:string,appId:string):Promise<string[]>{
        let url = `${Config.BaseUrl}/ManagerScope/${userId}/${appId}`
        let httpResult = await axios.get(url);
        return httpResult.data as string[]
    },
    async setManageScopeIds(userId:string,appId:string,resourceIds:string[]){
        let url = `${Config.BaseUrl}/ManagerScope/${userId}/${appId}`
        let httpResult = await axios.put(url,resourceIds);
    },
    async getManagedResources():Promise<AppAndResource[]>{
        let url = `${Config.BaseUrl}/ManagerScope/manage`
        let httpResult = await axios.get(url);
        return httpResult.data as AppAndResource[]
    },
    async changeUserResources(userId:string,appId:string,userResourceIds:string[]){
        let url = `${Config.BaseUrl}/ManagerScope/userResource/${userId}/${appId}`;
        await axios.put(url,userResourceIds);
    }
}