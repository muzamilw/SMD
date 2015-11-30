/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

   // config.toolbar = 'Full';
    //config.toolbar_Full =
    //[
    //    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
    //    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
    //    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
    //    {
    //        name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton']
    //    },
    //    '/',
    //    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
    //    {
    //        name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
    //        '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
    //    },
    //    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
    //    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
    //    '/',
    //    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
    //    { name: 'colors', items: ['TextColor', 'BGColor'] },
    //    { name: 'tools', items: ['Maximize', 'ShowBlocks', '-'] }
    //];

    
    config.toolbarCanCollapse = true;
    //config.fullPage = true;
    //config.allowedContent = true;
    config.baseFloatZIndex = 10000;
    config.removeDialogTabs = 'image:advanced';
    //config.extraPlugins = 'wysiwygarea';
    config.removeDialogTabs = 'link:target;link:advanced;image:Link;image:advanced';
    config.allowedContent = {
        script: true,
        $1: {
            // This will set the default set of elements
            elements: CKEDITOR.dtd,
            attributes: true,
            styles: true,
            classes: true
        }
    };
};


