// .eslintrc.js
const typescriptEslintRecommended = require('@typescript-eslint/eslint-plugin').configs.recommended;

module.exports = {
  "extends": "airbnb",

  "parserOptions": {
    "ecmaVersion": 8,
    "sourceType": "module",
    "ecmaFeatures": {
      "impliedStrict": true
    }
  },

  "env": {
    "node": true,
    "es6": true,
    "mocha": true,
    "browser": true,
  },

  settings: {
    'import/resolver': {
      node: {
        extensions: ['.js', '.ts'],
      }
    }
  },
  
  overrides: [
    {
      files: ['**/*.ts'],
      parser: '@typescript-eslint/parser',
      parserOptions: {
        sourceType: 'module',
        project: './tsconfig.json',
        "parserOptions": {
          "jsx": true,
        }
      },
      plugins: [ 
        '@typescript-eslint',
        'import',
      ],
      rules: Object.assign(typescriptEslintRecommended.rules, {
        '@typescript-eslint/no-unused-vars': 'off',
        '@typescript-eslint/interface-name-prefix': 'off',
        '@typescript-eslint/no-explicit-any': 'off',
        'import/no-unresolved': 'off',
        'import/extensions': [0, '.js', '.json', '.ts']
      })
    }
  ],
};
