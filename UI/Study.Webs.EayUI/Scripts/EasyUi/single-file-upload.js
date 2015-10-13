function NotificationArea($container) {
    this.showProgressNotification = function ($progress, $isVisible) {
        $container.html("<span>Progress : " + $progress + " %</span>");

        if ($isVisible == false) {
            $container.fadeIn();
        }
    };

    this.showErrorNotification = function () {
        $container.removeAttr("class");
        $container.addClass("alert error pull-right");
        $container.html("<span>Upload error.</span>");
    };

    this.showSuccessNotification = function () {
        $container.removeAttr("class");
        $container.addClass("alert info pull-right");
        $container.html("<span>Uploaded successfully.</span>");
    };
}

function FileUpload() {
    this.guid = "";
    this.onUploadProgress = false;
    this.notificationObject = null;
    this.trackUrl = "";

    this.uploadSingleFile = function ($form, $guid, $url, $notificationObject, $trackUrl) {
        if ($form != null) {
            this.guid = $guid;
            //this.notificationObject = $notificationObject;
            this.trackUrl = $trackUrl;
            var trackTimer = setInterval(function () {
                trackUploadProgress($trackUrl, $notificationObject, $guid);
            }, 1000);

            $form.ajaxSubmit({
                url: $url,
                data: {
                    guid: $guid
                },
                beforeSend: function () {
                    $notificationObject.showProgressNotification(0, false);
                },
                success: function (data) {
                    console.log("sukses");

                    if (data == true) {
                        clearTimeout(trackTimer);
                        $notificationObject.showSuccessNotification();
                    }
                    else {
                        $notificationObject.showErrorNotification();
                    }
                },
                error: function (xhr, ajaxOptions, error) {
                    $notificationObject.showErrorNotification();
                },
                complete: function () {
                    clearTimeout(trackTimer);
                }
            });
        }
    };

    //this.trackUploadProgress = function () {
    //    console.log("Upload progress");
    //    //var notifObj = this.notificationObject;
    //    //$.ajax({
    //    //    url: this.trackUrl,
    //    //    type: "post",
    //    //    data: {
    //    //        guid: this.guid
    //    //    },
    //    //    success: function (data) {
    //    //        notifObj.showProgressNotification(data, true);
    //    //    }
    //    //});

    //    setInterval(this.trackUploadProgress(), 1500);
    //}
}

function trackUploadProgress($url, $notificationObject, $guid) {
    console.log("Upload progress");

    $.ajax({
        url: $url,
        type: "post",
        data: {
            guid: $guid
        },
        success: function (data) {
            $notificationObject.showProgressNotification(data, true);
        }
    });
}

