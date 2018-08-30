<?php
/**
 * API file
 *
 * This file is used to get and return data from the
 * database, and other services as well. It will be
 * used for a Quiz Game made by SKP.
 * @author Simeon Lucas Dahl <simeondahl@gmail.com>
 */
// Only allow POST requests to the API
if (empty($_POST)) {
    die("Denne website er kun API requests. Læs dokumentation hvis du er i tvivl hvordan man bruger den.");
}

// Require needed files for the API to work
require (__DIR__ . '/classes/dbObject.php'); // Used for the MySQLIDB object
require (__DIR__ . '/classes/MysqliDb.php'); // Class used for database
require (__DIR__ . '/classes/ApiClass.php'); // API Class with all the methods

// Database object used for the database
$db = new MysqliDb (Array (
    'host' => 'localhost',
    'username' => 'root',
    'password' => '',
    'db'=> 'skp-quiz',
    'port' => 3306,
    'prefix' => '',
    'charset' => 'utf8'));

$api_object = new ApiClass();
$api_object->handlePostRequest();
echo $api_object->getReturnDataAsJson();
