import axios from 'axios'
import {PagingList} from './commons';
import {Config} from './baseConfig'

interface Application{
    id:string,
    appName:string,
    appCode:string,
    enable:boolean,
    createTime:Date,
    descript:string,
    callbackUrl:string
}

export let appService={
    async searchApp(appName:string,pageIndex:number,pageSize:number):Promise<PagingList<Application>>{
        let path = `${Config.BaseUrl}/applications?appName=${appName}&pageIndex=${pageIndex}&pageSize=${pageSize}`
        let httpResult = await axios.get(path);
        return httpResult.data;
    },
    async getApp(appId:string):Promise<Application>{
        let path = `${Config.BaseUrl}/application/${appId}`
        let httpResult = await axios.get(path);
        return httpResult.data
    },
    async createApp(app:Application):Promise<Application>{
        let path = `${Config.BaseUrl}/application`
        let httpResult = await axios.post(path,app);
        return httpResult.data;
    },
    async editApp(appId:string,app:Application){
        let path = `${Config.BaseUrl}/application/${appId}`
        let httpResult = await axios.put(path,app);
    },
    async deleteApp(appId:string){
        let url = `${Config.BaseUrl}/application/${appId}`
        let httpResult = await axios.delete(url);
    },
    async getAppsecrete(appId:string):Promise<string>{
        let url = `${Config.BaseUrl}/application/appSecret/${appId}`
        let httpResult = await axios.get(url);
        return httpResult.data;
    },
    async changeAppSecret(appId:string){
        let url = `${Config.BaseUrl}/application/appSecret/${appId}`
        let httpResult = await axios.put(url);        
    }
}