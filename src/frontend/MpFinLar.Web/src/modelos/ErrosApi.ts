export interface ErroValidacao {
    type: string;
    title: string;
    status: number;
    errors: Record<string, string[]>;
}

export interface ErroAPI {
    title: string;
    status: number;
}
