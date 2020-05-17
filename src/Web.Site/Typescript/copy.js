const { ncp } = require('ncp');

// eslint-disable-next-line no-console
console.info(`
====================
Copying static files
====================`);

const jQueryFiles = [
  'jquery.min.js',
  'jquery.min.map',
];

// eslint-disable-next-line no-restricted-syntax
for (const file of jQueryFiles) {
  ncp(`node_modules/jquery/dist/${file}`, `wwwroot/${file}`, (
    err,
  ) => {
    if (err) {
      // eslint-disable-next-line no-console
      return console.error(err);
    }
    // eslint-disable-next-line no-console
    return console.info(`${file} ✓`);
  });
}

const SemanticFiles = [
  'semantic.min.css',
  'semantic.min.js',
];

// eslint-disable-next-line no-restricted-syntax
for (const file of SemanticFiles) {
  ncp(`node_modules/semantic-ui-css/${file}`, `wwwroot/${file}`, (
    err,
  ) => {
    if (err) {
      // eslint-disable-next-line no-console
      return console.error(err);
    }
    // eslint-disable-next-line no-console
    return console.info(`${file} ✓`);
  });
}

const SweetAlertFiles = ['sweetalert2.min.css'];

// eslint-disable-next-line no-restricted-syntax
for (const file of SweetAlertFiles) {
  ncp(`node_modules/sweetalert2/dist/${file}`, `wwwroot/${file}`, (
    err,
  ) => {
    if (err) {
      // eslint-disable-next-line no-console
      return console.error(err);
    }
    // eslint-disable-next-line no-console
    return console.info(`${file} ✓`);
  });
}

ncp('node_modules/semantic-ui-css/themes', 'wwwroot/themes', (err) => {
  if (err) {
    // eslint-disable-next-line no-console
    return console.error(err);
  }
  // eslint-disable-next-line no-console
  return console.info(`
==============================
Semantic themes folders copied
==============================`);
});
