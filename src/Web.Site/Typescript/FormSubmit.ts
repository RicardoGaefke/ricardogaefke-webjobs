import Swal from "sweetalert2";
import { AxiosResponse } from 'axios';
import { IForm } from './IForm';
import MyAxios from './MyAxios';
import { IBasicReturn } from './IBasicReturn';

export default (event: JQuery.TriggeredEvent<HTMLElement, any, any, any>, fields: IForm) => {
  MyAxios()
    .interceptors.request.use(
      (config) => {
        $('.ui.form').addClass('loading');
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    )
  ;

  MyAxios()
    .post<IBasicReturn>("SendXML")
    .then((response: AxiosResponse): void => {
      if (response.data.Success) {
        Swal.fire("Success", "Your file was sent!", "success");
      } else {
        Swal.fire("Oops...", "There was a problem!", "error");
      }
      $(".ui.form").removeClass("loading");
    })
    .catch(err => Swal.fire("Oops...", "There was a problem!", "error"))
  ;
};
