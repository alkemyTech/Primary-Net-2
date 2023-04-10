import axios from "axios";

const primateWalletApi = axios.create({
    baseURL:"/api"
})

export default primateWalletApi;