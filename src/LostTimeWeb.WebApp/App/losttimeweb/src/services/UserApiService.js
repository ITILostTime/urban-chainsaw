import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/userprofile";

class UserApiService {
    constructor() {

    }
    async getUserListAsync() {
        return await getAsync(endpoint);
    }

    async getUserAsync(UserId) {
        console.log("send get");
        return await getAsync(`${endpoint}/${UserId}`);
    }
    async getUserByEmailAsync(UserEmail) {
        return await getAsync(`${endpoint}/${UserEmail}`);
    }

    async createUserAsync(model) {
        return await postAsync(endpoint, model);
    }

    async updateUserAsync(model) {
        return await putAsync(`${endpoint}/${model.userID}`, model);
    }
    async updateUserPasswordAsync(model) {
        return await putAsync(`${endpoint}/editpassword/${model.userID}`, model);
    }

    async deleteUserAsync(UserId) {
        return await deleteAsync(`${endpoint}/${UserId}`);
    }
}

export default new UserApiService()