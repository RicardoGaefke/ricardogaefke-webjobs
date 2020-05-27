import axios, { AxiosInstance } from 'axios';

export default (): AxiosInstance => {
  const myAxios: AxiosInstance = axios.create({
    baseURL: '/api/',
    withCredentials: true,
  });

  myAxios.interceptors.request.use(
    (config) => {
      $('.ui.form').addClass('loading');
      return config;
    },
  );

  return myAxios;
};
