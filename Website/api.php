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

include_once (__DIR__ . '/config.php');

$api_object = new ApiClass();
$api_object->handlePostRequest();
echo $api_object->getReturnDataAsJson();
