// eslint-disable-next-line no-unused-vars
import { IFile } from './IFile';

export default (file: File): Promise<string | ArrayBuffer | null> => new Promise((resolve, reject) => {
  const reader = new FileReader();
  reader.readAsDataURL(file);
  reader.onload = (): void => resolve(reader.result);
  reader.onerror = (error): void => reject(error);
});
