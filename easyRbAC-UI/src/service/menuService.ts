export let menuService = {
    getMenus() {
        let result = [
            {
                id:"1",
                name: '用户管理',
                path: '/user'                
            },
            {
                id:"3",
                name: '用户角色管理',
                children: [
                    {
                        id:"9",
                        name:"角色管理",
                        path:"/role"
                    },
                    {
                        id:"4",
                        name: '角色资源管理',
                        path: '/role/resouce'
                    },
                ]
            },
            {
                id:"5",
                name: '应用管理',
                path: '/app',
                children: [
                    {
                        id:"6",
                        name: '应用资源管理',
                        path: '/app/resouce'
                    }
                ]
            }
        ]

        return result;
    }
}