import axios from '../myAxios'
import {Config} from './baseConfig'
import {resourceService} from './resourceService'

interface MenuItem{
    id:String,
    name:string,
    path:string
    children:MenuItem[]
}

export let menuService = {
    async getMenus() {
        let url = `${Config.BaseUrl}/easyRBAC/login/userMenu`;
        let httpResult = await axios.get(url)
        let resources = httpResult.data;
        resourceService.ergodicTree(resources,(x:any)=>{
            x.id = x.id;
            x.name = x.resourceName;
            x.path = x.url;
            x.children = x.children
        })        
        return resources;
    }
}