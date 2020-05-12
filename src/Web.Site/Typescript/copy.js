const ncp = require("ncp").ncp;

console.info(`
====================
Copying static files
====================`);

const jQueryFiles = [
  'jquery.min.js',
  'jquery.min.map',
];

for (const file of jQueryFiles) {
  ncp(`node_modules/jquery/dist/${file}`, `wwwroot/${file}`, function (
    err
  ) {
    if (err) {
      return console.error(err);
    }
    console.info(`${file} ✓`);
  });
}

const SemanticFiles = [
  'semantic.min.css',
  'semantic.min.js',
];

for (const file of SemanticFiles) {
  ncp(`node_modules/semantic-ui-css/${file}`, `wwwroot/${file}`, function (
    err
  ) {
    if (err) {
      return console.error(err);
    }
    console.info(`${file} ✓`);
  });
}

const SweetAlertFiles = ["sweetalert2.min.css"];

for (const file of SweetAlertFiles) {
  ncp(`node_modules/sweetalert2/dist/${file}`, `wwwroot/${file}`, function (
    err
  ) {
    if (err) {
      return console.error(err);
    }
    console.info(`${file} ✓`);
  });
}

ncp('node_modules/semantic-ui-css/themes', 'wwwroot/themes', function (err) {
  if (err) {
    return console.error(err);
  }
  console.info(`
==============================
Semantic themes folders copied
==============================`);
});
