import axios, { AxiosInstance } from 'axios';

let backend = axios.create({
    baseURL: 'https://localhost:7211'
});

function replaceBackend(instance: AxiosInstance) {
    backend = instance;
}

function createDefaultInstance(): AxiosInstance {
    let instance = axios.create({
        baseURL: 'https://localhost:7211'
    });
    return instance;
}

export {
    backend as default,
    replaceBackend,
    createDefaultInstance
}