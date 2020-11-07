export const editorConfig = {
    toolbar: {
      items: [
        'bold',
        'italic',
        'underline',
        'link',
        'bulletedList',
        'numberedList',
        '|',
        'indent',
        'outdent',
        '|',
        'imageUpload',
        'blockQuote',
        'insertTable',
        'undo',
        'redo',
      ]
    },
    image: {
      toolbar: [
        'imageStyle:full',
        'imageStyle:side',
        '|',
        'imageTextAlternative'
      ]
    },
    table: {
      contentToolbar: [
        'tableColumn',
        'tableRow',
        'mergeTableCells'
      ]
    },
    // This value must be kept in sync with the language defined in webpack.config.js.
    language: 'en',
    simpleUpload: {
      uploadUrl: 'https://your_post_request_api_to your server',
      withCredentials: true,
      headers: {
        'X-CSRF-TOKEN': 'CSFR-Token',
         Authorization: 'Bearer <JSON Web Token>',
      }
  },
  };  