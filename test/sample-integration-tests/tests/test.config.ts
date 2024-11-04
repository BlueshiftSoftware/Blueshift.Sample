import axios from "axios";
import https from "https";

//process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'; // golden hammer solution to certificate problems

const instance = axios.create({
    httpsAgent: new https.Agent({
        rejectUnauthorized: false, // deal with ASP.NET's self-signed server cert in the Docker image
    })
});

export { instance as axios };

export const API_BASE_URL: string = process.env.API_BASE_URL || "https://localhost:51368/";

export const BAD_VERSION: string = "AAAAAAAAB9I=";

export const EMPTY_GUID: string = "00000000-0000-0000-0000-000000000000";