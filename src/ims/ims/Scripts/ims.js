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
            errorEl = $("#error-adding-image"),
            textButtonTag;


        $(document).on("click",
            selectorBtnAddImageFile,
            function() {
                inputAddImageFile.click();
            });

        $(document).on("change",
            selectorInputAddImageFile,
            function() {
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

        $("#modal-add-image").on("show.bs.modal", function() {
            clearAllFields();
            setTags();
        });

        function clearAllFields() {
            clearAddImageFile();
            $("#add-image-name").val("");
            $("#add-image-description").val("");
        }

        function clearAddImageFile() {
            clearError();
            btnAddImageFile.text("choose file");
            inputAddImageFile.val("");
        }
    }

    function setTags() {
        var url = "/api/tags/names";
        var type = "Get";
        var promise = query(url, type);
        promise.then(resolve => {
                $("#search-image-tag").autocomplete({
                    open: function () {
                        setTimeout(function () {
                            $('.ui-autocomplete').css('z-index', 2000);
                        }, 0);
                    },
                    source: resolve
                });
            }
        );
    }

    function query(url, type) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: type,
                async: false,
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    resolve(data);
                },
                error: function (jqXhr, status, error) {
                    alert(error);
                }
            });
        });
    }

})(window.Ims = window.Ims || {}, jQuery);

$(function () {
    var availableTags = [
        "ActionScript",
        "ActionScript1",
        "ActionScript2",
        "ActionScript3",
        "ActionScript4",
        "ActionScript5",
        "ActionScript6",
        "ActionScript7",
        "AppleScript8",
        "Asp",
        "BASIC",
        "C",
        "C++",
        "Clojure",
        "COBOL",
        "ColdFusion",
        "Erlang",
        "Fortran",
        "Groovy",
        "Haskell",
        "Java",
        "JavaScript",
        "Lisp",
        "Perl",
        "PHP",
        "Python",
        "Ruby",
        "Scala",
        "Scheme"
    ];
    $("#search-tag").autocomplete({
        source: availableTags
    });
});