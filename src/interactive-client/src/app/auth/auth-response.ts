export interface AuthResponse {
    access_token: string;
    expires_in: Number;
    token_type: string;
    refresh_token: string;
    scope: string;
}