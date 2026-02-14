import { Injectable } from "@angular/core";
import { HttpClientService } from "../http-client.service";
import { firstValueFrom, Observable } from "rxjs";
import { GetAllRoleQueryResponse } from "../../../contracts/role/GetAllRoleQueryResponse";


@Injectable({
    providedIn: 'root'
})

export class RoleService {

    constructor(private httpclientServie: HttpClientService) {

    }
    async createRole(name: string) {
        const observable: Observable<any> = this.httpclientServie.post({
            controller: 'Role'
        }, { name: name })
        return await firstValueFrom(observable) as { succeeded };
    }
    async read(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void) {
        const observable: Observable<GetAllRoleQueryResponse> = this.httpclientServie.get<GetAllRoleQueryResponse>({
            controller: "Role",
            queryString: `page=${page}&size=${size}`
        });
        try {
            const result = await firstValueFrom(observable);
            if (successCallBack) successCallBack();
            return result;
        } catch (error) {
            if (errorCallBack) errorCallBack(error.message);
            throw error;
        }
    }
}