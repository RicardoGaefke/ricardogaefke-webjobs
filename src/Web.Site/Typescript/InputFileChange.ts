import Swal from "sweetalert2";
import { IFile } from './IFile';
import ConvertFile from './ConvertFile';

export default $("#file").change(async function (e) {
  e.preventDefault();

  const Acceptable: string[] = ["text/xml", "application/xml"];
  const Input = <HTMLInputElement>e.target;
  let File: File | null = null;

  if (Input.files) {
    File = Input.files[0];
  }

  if (!File) {
    Swal.fire("Oops...", "Not a file!", "error");
  }

  const FileType = File?.type;

  if ($.inArray(FileType, Acceptable) < 0) {
    Swal.fire("Oops...", "Not an XML file!", "error");
    $("#file").val("");
    return;
  }

  const FileSize = File?.size || 0;
  if (FileSize > 500 * 1024) {
    Swal.fire("Oops...", "Files bigger then 500kb are not allowed!", "error");
    $("#file").val("");
    return false;
  }

  Swal.fire({
    position: "top-right",
    icon: "success",
    title: "Your file was read!",
    showConfirmButton: false,
    timer: 6000,
    toast: true,
    timerProgressBar: true,
  });

  let MyFile: IFile = {
    name: File?.name,
    size: File?.size,
    type: File?.type,
  };

  MyFile.base64 = await ConvertFile(File as File).catch((e) =>
    Swal.fire("Oops...", e.message, "error")
  );

  $(".ui.form").form("set values", {
    FileName: MyFile.name,
    FileSize: MyFile.size,
    FileType: MyFile.type,
    FileBase64: MyFile.base64,
  });
});
