import axios, { AxiosInstance } from "axios";

export default (): AxiosInstance => {
  const myAxios: AxiosInstance = axios.create({
    baseURL: '/api/',
    withCredentials: true,
  });

  return myAxios;
};
