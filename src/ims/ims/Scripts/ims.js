$(function () {
    Ims.InitCommon();
    Ims.InitUser();
    Ims.InitImage();
    Ims.InitTag();
});

(function (ims, $) {
    ims.InitCommon = function () {
        
    };

    ims.InitTag = function () {
        var containerTagPopular = $("#container-tag-popular");

        (function() {
            var url = `/api/tags/popular`;
            var type = "Get";
            var tagsPopularPromise = ims.query(url, type);
            tagsPopularPromise.then(result => {
                setTagPopular(result);
            });
        })();

        function setTagPopular(tags) {
            //containerTagPopular.empty();
            $.each(tags,
                function(index, item) {
                    var a = $("<a></a>").addClass("btn btn-link btn-md").text(item.name);
                    containerTagPopular.append(a);
                });
        }

        (function setTagsAutocomplete() {
            var url = "/api/tags/names";
            var type = "Get";
            var async = false;
            var promise = ims.query(url, type, async);
            promise.then(resolve => {
                    $("#search-tag").autocomplete({
                        source: resolve
                    });
                }
            );
        })();
    }

    ims.InitUser = function() {
        ims.userId = getUserId();

        function getUserId() {
            var inputIdentity = $("#identity-user-id");
            if (inputIdentity !== undefined && inputIdentity !== null) {
                return inputIdentity.val();
            } else {
                return null;
            }
        };
    };

    ims.InitImage = function() {
        var btnAddImage = $("#add-new-image"),
            selectorInputAddImageFile = "#add-image-file",
            inputAddImageFile = $(selectorInputAddImageFile),
            selectorBtnAddImageFile = "#clone-add-image-file",
            btnAddImageFile = $(selectorBtnAddImageFile),
            maxSizeImage = 3000000,
            msgErrorSize = "Image size exceeds the max. size. Choose other image!",
            errorEl = $("#error-adding-image"),
            btnAddTagInContainer = $("#add-tag-in-container"),
            inputNewTag = $("#search-image-tag"),
            tagContainer = $("#add-image-tag"),
            imageContainer = $("#show-images"),
            nameImageNew = $("#add-image-name"),
            scrollContainer = document.querySelector(".row.images"),
            descriptionImageNew = $("#add-image-description"),
            inputSearchByTag = $("#search-tag"),
            btnRunSearchImageByTag = $("#run-search-image-by-tag");

        btnRunSearchImageByTag.click(function() {
            var tagName = inputSearchByTag.val();
            if (tagName.trim() === "") return;
            getImages(1, true, tagName);
            imageContainer.data("paginationFilter", tagName);
        });

        $(".row.images").scroll(function (evt) {

            var top = scrollContainer.scrollTop;
            var cH = scrollContainer.clientHeight;
            var sH = scrollContainer.scrollHeight;

            if (sH <= top + cH) {
                console.log("END!!!!!!!!!!!!!!");
                var data = imageContainer.data();
                if (data.paginationCurrentNumber < data.paginationCountPages) {
                    var filter = undefined;
                    if (data.paginationFilter !== undefined && data.paginationFilter !== "") {
                        filter = data.paginationFilter;
                    }
                    getImages(data.paginationCurrentNumber + 1, false, filter);
                    console.log("images added");
                }

            }
        });

        $(document).on("click",
            selectorBtnAddImageFile,
            function() {
                inputAddImageFile.click();
            });

        $(document).on("change",
            selectorInputAddImageFile,
            function() {
                var fileList = this.files;
                if (fileList.length === 0) return;
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
            setTagsAutocomplete();
        });

        btnAddTagInContainer.click(function() {
            addTagInContainer();
        });

        $(document).on("click", ".added-tag span", function() {
            var clickBtn = $(this).parent();
            clickBtn.remove();
            checkCapacityOfTagContainer();
        });

        function addTagInContainer() {
            var tagName = inputNewTag.val();
            if (checkIfTagExistInContainer(tagName)) {
                var errorMsg = `tag: ${tagName} has already added!`;
                ims.showError(errorMsg);
                return;
            }
            if (tagName === "") {
                return;
            }
            var badge = $("<span></span>").addClass("badge").text("x");
            var attrBtn= {
                type: "button",
                "class": "btn btn-primary added-tag",
                "data-tag-name": tagName
            }
            var btn = $("<button></button>").attr(attrBtn).text(tagName);
            badge.appendTo(btn);
            tagContainer.append(btn);
            inputNewTag.val("");
            checkCapacityOfTagContainer();
        }

        function clearAllFields() {
            clearAddImageFile();
            $("#add-image-name").val("");
            $("#add-image-description").val("");
            tagContainer.empty();
            inputNewTag.val("");
            btnAddTagInContainer.attr("disabled", false);
        };

        btnAddImage.click(function() {
            var isValidation = checkRequiredFields();
            if (!isValidation) {
                return;
            }
            var name = nameImageNew.val();
            var description = descriptionImageNew.val();
            var tags = getTagsFromTagContainer();
            var image = document.querySelector(selectorInputAddImageFile).files[0];

            var imageFormNew = new FormData();
            imageFormNew.append("Name", name);
            imageFormNew.append("Description", description);
            imageFormNew.append("Tags", tags);
            imageFormNew.append("Image", image);
            imageFormNew.append("UserId", ims.userId);

            var url = "/api/images";
            var contentType = false;

            var promise = ims.queryPost(url, contentType, imageFormNew);
            promise.then(resolve => {
                    $("#modal-add-image").modal("hide");
                    getImages(1, true);
                    imageContainer.data("paginationFilter", "");
                    inputSearchByTag.val("");
                }
            );

        });

        function getImages(pageNumber, isClear, tagName) {
            var url = `/api/users/${ims.userId}/images/page/${pageNumber}`;
            if (tagName !== undefined) {
                url = `/api/users/${ims.userId}/images/page/${pageNumber}/tag/${tagName}`;
            }
            var type = "Get";
            var imagesPromise = ims.query(url, type);
            imagesPromise.then(resolve => {
                setImagesInContainer(resolve, isClear);
            });

        }

        function setImagesInContainer(data, isClear) {
            if (isClear) {
                imageContainer.empty();
            }
            var images = data.images;
            var pagination = data.pagination;
            imageContainer.data("paginationCurrentNumber", pagination.currentPageNumber);
            imageContainer.data("paginationCountPages", pagination.countPages);
           
            $.each(images, function (index, item) {
                var div = $("<div></div>").addClass("col-md-3 col-sm-4 col-xs-6");
                var a = $("<a></a>").attr("href", "#").addClass("thumbnail");
                var img = $("<img></img>").attr("src", item.path);
                a.append(img);
                div.append(a);
                imageContainer.append(div);
            });
        }

        function getTagsFromTagContainer() {
            var tagNames = [];
            var tags = tagContainer.find("button");
            $.each(tags, function (index, item) {
                var name = $(item).data("tagName");
                tagNames.push(name);
            });
            return tagNames;
        }

        function checkCapacityOfTagContainer() {
            var maxCapacity = 6;
            var count = tagContainer.find("button").length;
            if (count >= maxCapacity) {
                btnAddTagInContainer.attr("disabled", "disabled");
            }
            else{
                btnAddTagInContainer.attr("disabled", false);
            }
            if (count >= 1) {
                return true;
            } else {
                return false;
            }
        }

        function checkIfTagExistInContainer(tagName) {
            var tags = tagContainer.find("button");
            var isExist = false;
            $.each(tags, function (index, item) {
                var existName = $(item).data("tagName");
                if (existName.toLowerCase() === tagName.toLowerCase()) {
                    isExist = true;
                }
            });
            return isExist;
        };

        function checkRequiredFields() {
            var errorMsg = "Fill in required fields";
            var file = document.querySelector(selectorInputAddImageFile).files.length;
            if (!checkCapacityOfTagContainer() || file === 0) {
                ims.showError(errorMsg);
                return false;
            }
            return true;
        }

        function clearAddImageFile() {
            clearError();
            btnAddImageFile.text("choose file");
            inputAddImageFile.val("");
        }

        function setTagsAutocomplete() {
            var url = "/api/tags/names";
            var type = "Get";
            var async = false;
            var promise = ims.query(url, type, async);
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
    }

    ims.query = function (url, type, async) {
        if (async === undefined) {
            async = true;
        }
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: type,
                async: async,
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

    ims.queryPost = function (url, contType, data) {
        var contentType = "application/json;charset=utf-8";
        if (contType !== undefined) {
            contentType = contType;
        }
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: "Post",
                contentType: contentType,
                processData: false,
                data: data,
                success: function (data) {
                    resolve(data);
                },
                error: function (jqXhr, status, error) {
                    ims.showError(error);
                }
            });
        });
    }

    ims.showError = function(msgError) {
        $("p#msg-modal-error").text(msgError);
        $("#modal-error").modal("show");
    }

})(window.Ims = window.Ims || {}, jQuery);

