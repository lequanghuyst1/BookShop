import Resource from "../resource/resource";

let returnResult = () => {
  return Resource["VN"].VALIDATE_STATUS;
};
const validateEmail = (value) => {
  try {
    let check = "";
    if (value) {
      check = value.match(
        /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/
      )
        ? undefined
        : returnResult().email;
    } else {
      check = undefined;
    }
    return check;
  } catch (e) {
    console.log(e);
  }
};

function requiredValidate(value, label) {
  return value ? undefined : returnResult().required(label);
}

export const validateValue = {
  required: requiredValidate,
  email: validateEmail,
};