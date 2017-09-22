export let menuService = {
    getMenus() {
        let result = [
            {
                id:"1",
                name: '用户',               
                children: [
                    {
                        id:"10",
                        name:"用户管理",
                        path:"/user"
                    },
                    {
                        id:"11",
                        name: '用户资源管理',
                        path: '/user/resource'
                    }
                ]             
            },
            {
                id:"3",
                name: '角色',
                children: [
                    {
                        id:"9",
                        name:"角色管理",
                        path:"/role"
                    },
                    {
                        id:"4",
                        name: '角色资源管理',
                        path: '/role/roleResource'
                    },{
                        id:"15",
                        name:"用户角色管理",
                        path:'/role/user'
                    }
                ]
            },
            {
                id:"5",
                name: '应用',
                path: '/app',
                children: [
                    {
                        id:"12",
                        name:"应用管理",
                        path:'/application'
                    },
                    {
                        id:"6",
                        name: '应用资源管理',
                        path: '/app/resource'
                    }
                ]
            }
        ]

        return result;
    }
}