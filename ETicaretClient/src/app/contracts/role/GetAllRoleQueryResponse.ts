export interface GetAllRoleQueryResponse {
    datas: { [key: string]: string };  // Dictionary<string, string>
    totalRoleCount: number;
}