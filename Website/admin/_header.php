<?php
include_once (__DIR__ . '/../config.php');

// Check if user is logged in
if (!isset($_SESSION['loggedIn']))
{
    header('Location:login.php');
}

include_once (__DIR__ . '/../classes/WebStatics.php');

$WebStatics = new WebStatics();

?>
<!DOCTYPE html>

<html>
<head>
    <title>Admin Panel</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <style>
        .main-text {
            padding-top: 10px;
            padding-bottom: 10px;
        }
    </style>
</head>
<body>
<div class="container">
    <div class="col-sm-12 border-bottom">
        <a href="index.php">Hjem</a> | <a href="questions.php">Spørgsmål</a> | <a href="teams.php">Hold</a> | <a href="users.php">Brugere</a> | <a href="logout.php">Log ud</a>
    </div>
    <div class="col-sm-12">
