import {Config} from './baseConfig'
import axios from 'axios'

export let managerScopeService={
    async getManagedScopeIds(userId:string,appId:string):Promise<string[]>{
        let url = `${Config.BaseUrl}/ManagerScope/${userId}/${appId}`
        let httpResult = await axios.get(url);
        return httpResult.data as string[]
    },
    async setManageScopeIds(userId:string,appId:string,resourceIds:string[]){
        let url = `${Config.BaseUrl}/ManagerScope/${userId}/${appId}`
        let httpResult = await axios.put(url,resourceIds);
    }
}