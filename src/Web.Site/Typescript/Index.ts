import FormValidation from './FormValidation';
import UploadButtonClick from './UploadButtonClick';
import FileChange from './InputFileChange';
import FormSubmit from './FormSubmit';

$((): void => {
  $('.ui.checkbox').checkbox();

  // eslint-disable-next-line no-unused-expressions
  UploadButtonClick;
  // eslint-disable-next-line no-unused-expressions
  FileChange;

  $('.ui.form')
    .form({
      on: 'blur',
      inline: true,
      fields: FormValidation,
      onSuccess: FormSubmit,
    });
});
