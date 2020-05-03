import { SweetAlertResult } from 'sweetalert2';

export interface IFile {
  name?: string;
  type?: string;
  size?: number;
  base64?: string | ArrayBuffer | null | Error | SweetAlertResult;
}