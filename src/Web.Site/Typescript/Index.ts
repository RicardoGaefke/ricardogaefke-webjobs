import FormValidation from './FormValidation';
import UploadButtonClick from './UploadButtonClick';
import FileChange from './InputFileChange';
import FormSubmit from './FormSubmit';

$((): void => {
  $('.ui.checkbox').checkbox();

  UploadButtonClick;
  FileChange;

  $('.ui.form')
    .form({
      on: 'blur',
      inline: true,
      fields: FormValidation,
      onSuccess: FormSubmit,
    });
});
