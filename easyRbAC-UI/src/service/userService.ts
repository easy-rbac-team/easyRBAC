import axios from "axios"
import { Config } from "./baseConfig"
import { PagingList } from "./commons"

interface UserInfo {
    userName: string,
    realName: string,
    mobilePhone: string,
    enable: boolean
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
    }, async changeResource(userId: string, appId: string, resourceIdLst: string[]) {
        let url = `${Config.BaseUrl}/user/resource/${userId}/${appId}`;
        let httpResult = await axios.put(url,resourceIdLst);
    }, async getUserResourceIds(userId: string, appId: string):Promise<{userResource:string[],roleResource:string[]}> {
        let url = `${Config.BaseUrl}/user/resource/${userId}/${appId}`;
        let httpResult = await axios.get(url);
        return httpResult.data;
    }
}