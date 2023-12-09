<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompInfoImages.aspx.cs" Inherits="CompInfoImages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Muat Naik Gambar</title>

    <link href="https://photoswipe.com/site-assets/site.css?v=4.1.3-1.0.4" rel="stylesheet" />
    <link href="https://photoswipe.com/dist/photoswipe.css?v=4.1.3-1.0.4" rel="stylesheet" />
    <link href="https://photoswipe.com/dist/default-skin/default-skin.css?v=4.1.3-1.0.4" rel="stylesheet" />

    <script src="https://photoswipe.com/dist/photoswipe.min.js?v=4.1.3-1.0.4"></script>
    <script src="https://photoswipe.com/dist/photoswipe-ui-default.min.js?v=4.1.3-1.0.4"></script>
    <!-- jQuery -->
    <script src="vendors/jquery/dist/jquery.min.js"></script>

</head>
<body>

    <div class="section section--head">

        <div class="row row--heading">
            <h1><%=sItemNo %></h1>
            <p><%=oModItem.GetSetitemdesc %></p>
        </div>

        <div class="row">
            <div id="demo-test-gallery" class="demo-gallery">
                <%
                    if (lsItemImage.Count > 0)
                    {
                        for (int i = 0; i < lsItemImage.Count; i++)
                        {
                            MainModel itemImage = (MainModel)lsItemImage[i];
                %>
                
                <a href='./Attachment/Slider/<%=sCurrComp %>/<%=itemImage.GetSetfilename %>' data-author="<%=itemImage.GetSetfilename %>" data-size='<%=itemImage.GetSetimgwidth %>x<%=itemImage.GetSetimgheight %>' data-med='./Attachment/Slider/<%=sCurrComp %>/<%=itemImage.GetSetfilename %>' data-med-size='<%=itemImage.GetSetimgwidth %>x<%=itemImage.GetSetimgheight %>' <%=i.Equals(0)?"class='demo-gallery__img--main'":"" %>>
                    <img src='./Attachment/Slider/<%=sCurrComp %>/<%=itemImage.GetSetfilename %>' alt='' />
                    <figure><%=sItemNo %></figure>
                </a>
                <%
                        }
                    }
                    else
                    {
                %>
                <a href="./Attachment/empty_424x500.png" data-size="424x500" data-med="#" data-med-size="424x500" data-author="Kosong" class="demo-gallery__img--main">
                    <img src="./Attachment/empty_424x500.png" alt="" />
                    <figure></figure>
                </a>
                <%
                    }
                %>
            </div>
        </div>
    </div>

    <div id="gallery" class="pswp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="pswp__bg"></div>

        <div class="pswp__scroll-wrap">

            <div class="pswp__container">
                <div class="pswp__item"></div>
                <div class="pswp__item"></div>
                <div class="pswp__item"></div>
            </div>

            <div class="pswp__ui pswp__ui--hidden">

                <div class="pswp__top-bar">

                    <div id="pswp_counter" class="pswp__counter"></div>

                    <button class="pswp__button pswp__button--close" title="Close (Esc)"></button>

                    <button class="pswp__button pswp__button--share" title="Share"></button>

                    <button class="pswp__button pswp__button--fs" title="Toggle fullscreen"></button>

                    <button class="pswp__button pswp__button--zoom" title="Zoom in/out"></button>

                    <div class="pswp__preloader">
                        <div class="pswp__preloader__icn">
                            <div class="pswp__preloader__cut">
                                <div class="pswp__preloader__donut"></div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- <div class="pswp__loading-indicator"><div class="pswp__loading-indicator__line"></div></div> -->

                <div class="pswp__share-modal pswp__share-modal--hidden pswp__single-tap">
                    <div class="pswp__share-tooltip">
                        <!-- <a href="#" class="pswp__share--facebook"></a>
				            <a href="#" class="pswp__share--twitter"></a>
				            <a href="#" class="pswp__share--pinterest"></a>
				            <a href="#" download class="pswp__share--download"></a> -->
                    </div>
                </div>

                <button class="pswp__button pswp__button--arrow--left" title="Previous (arrow left)"></button>
                <button class="pswp__button pswp__button--arrow--right" title="Next (arrow right)"></button>

                <div id="pswp_caption" class="pswp__caption">
                    <div id="pswp_caption_center" class="pswp__caption__center">
                    </div>
                </div>
            </div>

        </div>


    </div>

    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="row row--heading">
            <input id="FileUpload" name="FileUpload" accept="image/*;capture=camera" type="file" style="width: 180px" />
            <input type="button" id="btnUpload" name="btnUpload" value="Muat Naik" onclick="storeFile();" disabled="disabled" />
            <div id="div_upload" style="display: none;">
                <img id="myUploadedImg" alt="Photo" style="width: 180px;" src="#" />
            </div>
        </div>
        <div style="display: none;">
            <input type="text" id="filename" name="filename" />
            <input type="text" id="imgwidth" name="imgwidth" />
            <input type="text" id="imgheight" name="imgheight" />
        </div>
    </form>

    <script type="text/javascript">
        (function () {

            var initPhotoSwipeFromDOM = function (gallerySelector) {

                var parseThumbnailElements = function (el) {
                    var thumbElements = el.childNodes,
                        numNodes = thumbElements.length,
                        items = [],
                        el,
                        childElements,
                        thumbnailEl,
                        size,
                        item;

                    for (var i = 0; i < numNodes; i++) {
                        el = thumbElements[i];

                        // include only element nodes 
                        if (el.nodeType !== 1) {
                            continue;
                        }

                        childElements = el.children;

                        size = el.getAttribute('data-size').split('x');

                        // create slide object
                        item = {
                            src: el.getAttribute('href'),
                            w: parseInt(size[0], 10),
                            h: parseInt(size[1], 10),
                            author: el.getAttribute('data-author')
                        };

                        item.el = el; // save link to element for getThumbBoundsFn

                        if (childElements.length > 0) {
                            item.msrc = childElements[0].getAttribute('src'); // thumbnail url
                            if (childElements.length > 1) {
                                item.title = childElements[1].innerHTML; // caption (contents of figure)
                            }
                        }


                        var mediumSrc = el.getAttribute('data-med');
                        if (mediumSrc) {
                            size = el.getAttribute('data-med-size').split('x');
                            // "medium-sized" image
                            item.m = {
                                src: mediumSrc,
                                w: parseInt(size[0], 10),
                                h: parseInt(size[1], 10)
                            };
                        }
                        // original image
                        item.o = {
                            src: item.src,
                            w: item.w,
                            h: item.h
                        };

                        items.push(item);
                    }

                    return items;
                };

                // find nearest parent element
                var closest = function closest(el, fn) {
                    return el && (fn(el) ? el : closest(el.parentNode, fn));
                };

                var onThumbnailsClick = function (e) {
                    e = e || window.event;
                    e.preventDefault ? e.preventDefault() : e.returnValue = false;

                    var eTarget = e.target || e.srcElement;

                    var clickedListItem = closest(eTarget, function (el) {
                        return el.tagName === 'A';
                    });

                    if (!clickedListItem) {
                        return;
                    }

                    var clickedGallery = clickedListItem.parentNode;

                    var childNodes = clickedListItem.parentNode.childNodes,
                        numChildNodes = childNodes.length,
                        nodeIndex = 0,
                        index;

                    for (var i = 0; i < numChildNodes; i++) {
                        if (childNodes[i].nodeType !== 1) {
                            continue;
                        }

                        if (childNodes[i] === clickedListItem) {
                            index = nodeIndex;
                            break;
                        }
                        nodeIndex++;
                    }

                    if (index >= 0) {
                        openPhotoSwipe(index, clickedGallery);
                    }
                    return false;
                };

                var photoswipeParseHash = function () {
                    var hash = window.location.hash.substring(1),
                        params = {};

                    if (hash.length < 5) { // pid=1
                        return params;
                    }

                    var vars = hash.split('&');
                    for (var i = 0; i < vars.length; i++) {
                        if (!vars[i]) {
                            continue;
                        }
                        var pair = vars[i].split('=');
                        if (pair.length < 2) {
                            continue;
                        }
                        params[pair[0]] = pair[1];
                    }

                    if (params.gid) {
                        params.gid = parseInt(params.gid, 10);
                    }

                    return params;
                };

                var openPhotoSwipe = function (index, galleryElement, disableAnimation, fromURL) {
                    var pswpElement = document.querySelectorAll('.pswp')[0],
                        gallery,
                        options,
                        items;

                    items = parseThumbnailElements(galleryElement);

                    // define options (if needed)
                    options = {

                        galleryUID: galleryElement.getAttribute('data-pswp-uid'),

                        getThumbBoundsFn: function (index) {
                            // See Options->getThumbBoundsFn section of docs for more info
                            var thumbnail = items[index].el.children[0],
                                pageYScroll = window.pageYOffset || document.documentElement.scrollTop,
                                rect = thumbnail.getBoundingClientRect();

                            return { x: rect.left, y: rect.top + pageYScroll, w: rect.width };
                        },

                        addCaptionHTMLFn: function (item, captionEl, isFake) {
                            if (!item.title) {
                                captionEl.children[0].innerText = '';
                                return false;
                            }
                            captionEl.children[0].innerHTML = item.title + '<br/><small>Photo: ' + item.author + '</small><br/><button class="btn btn-sm btn-warning" onclick="confirmDelete(' + '\'' + item.author + '\'' + ');">Hapus</button>';
                            //captionEl.children[0].innerHTML = item.title;
                            return true;
                        },

                    };


                    if (fromURL) {
                        if (options.galleryPIDs) {
                            // parse real index when custom PIDs are used 
                            // https://photoswipe.com/documentation/faq.html#custom-pid-in-url
                            for (var j = 0; j < items.length; j++) {
                                if (items[j].pid == index) {
                                    options.index = j;
                                    break;
                                }
                            }
                        } else {
                            options.index = parseInt(index, 10) - 1;
                        }
                    } else {
                        options.index = parseInt(index, 10);
                    }

                    // exit if index not found
                    if (isNaN(options.index)) {
                        return;
                    }


                    //for minimal control
                    options.mainClass = 'pswp--minimal--dark';
                    options.barsSize = { top: 0, bottom: 0 };
                    options.captionEl = true;
                    options.fullscreenEl = false;
                    options.shareEl = false;
                    options.bgOpacity = 0.85;
                    options.tapToClose = true;
                    options.tapToToggleControls = false;
                    /*
                    var radios = document.getElementsByName('gallery-style');
                    for (var i = 0, length = radios.length; i < length; i++) {
                        if (radios[i].checked) {
                            if (radios[i].id == 'radio-all-controls') {

                            } else if (radios[i].id == 'radio-minimal-black') {
                                options.mainClass = 'pswp--minimal--dark';
                                options.barsSize = { top: 0, bottom: 0 };
                                options.captionEl = false;
                                options.fullscreenEl = false;
                                options.shareEl = false;
                                options.bgOpacity = 0.85;
                                options.tapToClose = true;
                                options.tapToToggleControls = false;
                            }
                            break;
                        }
                    }
                    */

                    if (disableAnimation) {
                        options.showAnimationDuration = 0;
                    }

                    // Pass data to PhotoSwipe and initialize it
                    gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);

                    // see: http://photoswipe.com/documentation/responsive-images.html
                    var realViewportWidth,
                        useLargeImages = false,
                        firstResize = true,
                        imageSrcWillChange;

                    gallery.listen('beforeResize', function () {

                        var dpiRatio = window.devicePixelRatio ? window.devicePixelRatio : 1;
                        dpiRatio = Math.min(dpiRatio, 2.5);
                        realViewportWidth = gallery.viewportSize.x * dpiRatio;


                        if (realViewportWidth >= 1200 || (!gallery.likelyTouchDevice && realViewportWidth > 800) || screen.width > 1200) {
                            if (!useLargeImages) {
                                useLargeImages = true;
                                imageSrcWillChange = true;
                            }

                        } else {
                            if (useLargeImages) {
                                useLargeImages = false;
                                imageSrcWillChange = true;
                            }
                        }

                        if (imageSrcWillChange && !firstResize) {
                            gallery.invalidateCurrItems();
                        }

                        if (firstResize) {
                            firstResize = false;
                        }

                        imageSrcWillChange = false;

                    });

                    gallery.listen('gettingData', function (index, item) {
                        if (useLargeImages) {
                            item.src = item.o.src;
                            item.w = item.o.w;
                            item.h = item.o.h;
                        } else {
                            item.src = item.m.src;
                            item.w = item.m.w;
                            item.h = item.m.h;
                        }
                    });

                    gallery.init();
                };

                // select all gallery elements
                var galleryElements = document.querySelectorAll(gallerySelector);
                for (var i = 0, l = galleryElements.length; i < l; i++) {
                    galleryElements[i].setAttribute('data-pswp-uid', i + 1);
                    galleryElements[i].onclick = onThumbnailsClick;
                }

                // Parse URL and open gallery if it contains #&pid=3&gid=1
                var hashData = photoswipeParseHash();
                if (hashData.pid && hashData.gid) {
                    openPhotoSwipe(hashData.pid, galleryElements[hashData.gid - 1], true, true);
                }
            };

            initPhotoSwipeFromDOM('.demo-gallery');

        })();
        function getAppPath() {
            var pathArray = location.pathname.split('/');
            var appPath = "/";
            for (var i = 1; i < pathArray.length - 1; i++) {
                appPath += pathArray[i] + "/";
            }
            return appPath;
        }

        function uploadFile(file, i) {
            var pagePath = window.location.hostname;
            //alert(window.location.protocol + "//" + pagePath + getAppPath() + "WebService.asmx/uploadFile");
            var formData = new FormData();
            formData.append('inputitem[0]', '<%=sCurrComp%>');
            formData.append('inputitem[1]', '<%=sUserId%>');
            formData.append('inputitem[2]', '<%=sItemNo%>');
            formData.append("inputitem[3]", file);
            formData.append('inputitem[4]', 'Slider/' + '<%=sCurrComp%>' + '/');

            if ("<%=sUserId%>" != "") {
                $.ajax({
                    type: 'POST',
                    url: "WebService.asmx/uploadFile2",
                    //url: 'http://localhost:62709/WebService.asmx/uploadFile',
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-upload: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            if (result.filename != "") {
                                $("#filename").val(result.filename);
                                $("#myUploadedImg").attr("src", result.imageurl);
                                $("#div_upload").show();
                                $("#btnUpload").prop("disabled", false);
                            }
                            else {
                                $("#filename").val("");
                                $("#myUploadedImg").attr("src", "#");
                                $("#div_upload").hide();
                                $("#btnUpload").prop("disabled", true);
                            }
                        }
                        else {
                            $("#filename").val("");
                            $("#myUploadedImg").attr("src", "#");
                            $("#div_upload").hide();
                            $("#btnUpload").prop("disabled", true);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function () {
                        alert("Internal Server Error!");
                        $("#filename").val("");
                        $("#myUploadedImg").attr("src", "#");
                        $("#div_upload").hide();
                        $("#btnUpload").prop("disabled", true);
                    }
                });
            } else {
                alert("Internal Server Error!");
                $("#filename").val("");
                $("#myUploadedImg").attr("src", "#");
                $("#div_upload").hide();
                $("#btnUpload").prop("disabled", true);
            }
        }

        var _URL = window.URL || window.webkitURL;
        $("#FileUpload").on('change', function () {
            //alert("ayam");
            var file, img;
            $("#imgwidth").val("0");
            $("#imgheight").val("0");

            if ((file = this.files[0])) {
                img = new Image();
                img.onload = function () {
                    $("#imgwidth").val(this.width);
                    $("#imgheight").val(this.height);
                    uploadFile(file);
                };
                img.onerror = function () {
                    alert("Format Fail Tidak Sah!");
                    $('input[type=file]').val('');
                    $("#filename").val('');
                    $("#myUploadedImg").attr("src", "#");
                    $("#div_upload").hide();
                    $("#btnUpload").prop("disabled", true);
                };
                img.src = _URL.createObjectURL(file);
            }
        });

        function storeFile() {
            var pagePath = window.location.hostname;
            var paramList = "{'comp':'<%=sCurrComp%>', 'userid':'<%=sUserId%>', 'itemno':'<%=sItemNo%>','filename':" + document.getElementById("filename").value + "}";
            var formData = new FormData();
            formData.append('inputitem[0]', '<%=sCurrComp%>');
            formData.append('inputitem[1]', '<%=sUserId%>');
            formData.append('inputitem[2]', '<%=sItemNo%>');
            formData.append("inputitem[3]", document.getElementById("filename").value);
            formData.append("inputitem[4]", document.getElementById("imgwidth").value);
            formData.append("inputitem[5]", document.getElementById("imgheight").value);
            formData.append('inputitem[6]', 'Slider/' + '<%=sCurrComp%>' + '/');

            //alert('<%=sCurrComp%>' + '<%=sUserId%>' + '<%=sItemNo%>' + document.getElementById("filename").value + document.getElementById("imgwidth").value + document.getElementById("imgheight").value);

            if ("<%=sUserId%>" != "") {
                $.ajax({
                    type: "POST",
                    url: "WebService.asmx/storeFile2?",
                    //url: 'http://localhost:62709/WebService.asmx/storeFile',
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-store: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            alert("Proses muat naik gambar berjaya!");
                            window.location.href = "SliderImageUpload.aspx?action=OPEN&itemno=<%=sItemNo %>";
                        }
                        else {
                            alert(result.message);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function (status) {
                        alert("Internal Server Error!" + status);
                    }
                });
            }
        }

        function deleteFile(filename) {
            var pagePath = window.location.hostname;
            var formData = new FormData();
            formData.append('inputitem[0]', '<%=sCurrComp%>');
            formData.append('inputitem[1]', '<%=sUserId%>');
            formData.append('inputitem[2]', '<%=sItemNo%>');
            formData.append("inputitem[3]", filename);
            formData.append('inputitem[4]', 'Slider/' + '<%=sCurrComp%>' + '/');

            if ("<%=sUserId%>" != "") {
                $.ajax({
                    type: "POST",
                    url: "WebService.asmx/deleteFile2",
                    //url: 'http://localhost:62709/WebService.asmx/deleteFile',
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-delete: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            window.location.href = "SliderImageUpload.aspx?action=OPEN&itemno=<%=sItemNo %>";
                        }
                        else {
                            alert(result.message);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function (status) {
                        alert("Internal Server Error!" + status);
                    }
                });
            }
        }

        function confirmDelete(filename) {
            if (confirm("Anda pasti untuk hapuskan Imej ini?"))
            {
                deleteFile(filename);
            }    
        }
    </script>

</body>
</html>
