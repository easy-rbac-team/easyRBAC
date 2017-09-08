export interface PagingList<T>{
    items:T[],
    totalCount:number,
    pageIndex:number,
    pageSize:number,
    totalPage:number
}