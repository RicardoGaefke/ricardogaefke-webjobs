type IValidation = { [name: string]: string | string[] | SemanticUI.Form.Field; };

export default {
  Name: {
    identifier: "Name",
    rules: [
      {
        type: "minLength[3]",
        prompt: "Please enter your name",
      },
    ],
  },
  Email: {
    identifier: "Email",
    rules: [
      {
        type: "email",
        prompt: "Please enter your email",
      },
    ],
  },
  file: {
    identifier: "file",
    rules: [
      {
        type: "empty",
        prompt: "Please pick an XML file",
      },
    ],
  },
  attachment: {
    identifier: "attachment",
    rules: [],
  }
} as IValidation;
