import axios from '../myAxios'
import { Config } from "./baseConfig"
import { PagingList } from "./commons"
import {AppResource} from "./resourceService"

enum AccountType{
    User,
    Application
}

interface UserInfo {
    userName: string,
    realName: string,
    mobilePhone: string,
    enable: boolean,
    accountType:AccountType
}

interface ChangePassword{
    password:string,
    confirmPassword:string
}

export let userService = {
    async getUsers(userName: string, pageIndex: number, pageSize: number): Promise<PagingList<any>> {
        let path = `${Config.BaseUrl}/user?userName=${userName}&pageIndex=${pageIndex}&pageSize=${pageSize}`
        let result = await axios.get(path)
        return result.data as PagingList<any>;
    },
    async createUser(user: UserInfo) {
        let path = `${Config.BaseUrl}/user`
        let result = await axios.post(path, user);
    },
    async getUserInfo(userId: string): Promise<UserInfo> {
        let path = `${Config.BaseUrl}/user/${userId}`
        let httpResult = await axios.get(path);
        return httpResult.data as UserInfo
    },
    async deleteUser(userId: string) {
        let path = `${Config.BaseUrl}/user/${userId}`
        let httpResult = await axios.delete(path);
    }, 
    async changeResource(userId: string, appId: string, resourceIdLst: string[]) {
        let url = `${Config.BaseUrl}/user/resource/${userId}/${appId}`;        
        let httpResult = await axios.put(url, resourceIdLst);
    }, 
    async changeUserResources(userId:string,appId:string,resourceLst:AppResource[]){
        let resouce = resourceLst.filter((x)=>(x as any).disabled===false);
        let resourceIds = resouce.map(x=>x.id);
        await this.changeResource(userId,appId,resourceIds);
    },
    async getUserResourceIds(userId: string, appId: string): Promise<{ userResource: string[], roleResource: string[] }> {
        let url = `${Config.BaseUrl}/user/resource/${userId}/${appId}`;
        let httpResult = await axios.get(url);
        let result = httpResult.data as { userResource: string[], roleResource: string[] };
        return result;
    },
    async changePassowrd(userId:string,pwd:ChangePassword){
        let url = `${Config.BaseUrl}/user/${userId}/pwd`
        let httpResult = await axios.put(url,pwd);
    }
}