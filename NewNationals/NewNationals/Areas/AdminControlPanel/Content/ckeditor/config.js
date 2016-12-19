/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.removeButtons = 'Cut,Undo,Redo,Copy,Paste,PasteText,PasteFromWord,Find,SelectAll,Scayt,Replace,Form,TextField,Textarea,Select,Button,ImageButton,HiddenField,Radio,Checkbox';
    config.filebrowserBrowseUrl = '/Areas/AdminControlPanel/Content/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Areas/AdminControlPanel/Content/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/Areas/AdminControlPanel/Content/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Areas/AdminControlPanel/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Areas/AdminControlPanel/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Areas/AdminControlPanel/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
