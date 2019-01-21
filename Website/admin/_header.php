<?php
require_once (__DIR__ . '/../autoload.php');
include_once (__DIR__ . '/../classes/WebStatics.php');

Identification::needToBeLoggedIn();
$WebStatics = new WebStatics();
$Api = new ApiClass();
?>
<!DOCTYPE html>

<html>
<head>
    <title>Admin Panel</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="js/bootstrap.js"></script>
    <script src="js/jquery.js"></script>
    <style>
        .main-text {
            padding-top: 10px;
            padding-bottom: 10px;
        }
    </style>
    <script>



        function popitup(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();

                $(newWindow).on("beforeunload", function() {
                    newWindow.opener.location.reload();
                });


            }
        }
    </script>
</head>
<body>
<div class="container">
    <div class="col-sm-12 border-bottom">
        <a href="index.php">Hjem</a> | <a href="questions.php">Spørgsmål</a> | <a href="teams.php">Hold</a> | <a href="users.php?selectedTeam=all">Brugere</a> | <a href="logout.php">Log ud</a> | Du er logget ind med brugernavnet: <?= Identification::getUsername() ?>
    </div>
    <div class="col-sm-12">
