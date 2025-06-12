export interface User {
    id: number;
    firstname: string;
    lastname: string;
    phonenumber: string;
    emailaddress: string;
    password: string;
    // status?: string;
}

export interface ApiResponse<T> {
    message: string;
    data: T;
}
