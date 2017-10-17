$(function() {
    Ims.InitImage();
});

(function(ims, $) {
    ims.InitImage = function() {
        var btnAddImage = $("#add-new-image"),
            selectorInputAddImageFile = "#add-image-file",
            inputAddImageFile = $(selectorInputAddImageFile),
            selectorBtnAddImageFile = "#clone-add-image-file",
            btnAddImageFile = $(selectorBtnAddImageFile),
            maxSizeImage = 3000000,
            msgErrorSize = "Image size exceeds the max. size. Choose other image!",
            errorEl = $("#error-adding-image");

        $(document).on("click", selectorBtnAddImageFile, function() {
            inputAddImageFile.click();
        });

        $(document).on("change", selectorInputAddImageFile, function() {
            var fileList = this.files;
            var name = fileList[0].name;
            var size = fileList[0].size;
            var isSizeAllowed = checkSize(size);
            if (isSizeAllowed) {
                clearError();
            } else {
                clearError();
                addError(msgErrorSize);
            }
            setNameBtnAddImageFile(name);
        });

        function setNameBtnAddImageFile(name) {
            var length = name.length;
            if (length > 26) {
                var nameBegin = name.substring(0, 16);
                var nameEnd = name.substring(length - 9);
                var nameNew = `${nameBegin}...${nameEnd}`;
                btnAddImageFile.text(nameNew);
            } else {
                btnAddImageFile.text(name);
            }
           
        }

        function checkSize(size) {
            if (size > maxSizeImage) {
                return false;
            } else {
                return true;
            }
        }

        function addError(msg) {
            errorEl.text(msg);
        }

        function clearError() {
            errorEl.empty();
        }

    }
})(window.Ims = window.Ims || {}, jQuery)