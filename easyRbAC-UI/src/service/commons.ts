export interface PagingList<T>{
    items:T[],
    page:Page
}

interface Page{
    totalCount:number,
    pageIndex:number,
    pageSize:number,
    totalPage:number
}