<?php

// Require needed files for the API to work
require_once (__DIR__ . '/classes/MysqliDb.php'); // Class used for database
include_once (__DIR__ . '/classes/ApiClass.php'); // API Class with all the methods

session_start();

// Database object used for the database
$db = new MysqliDb (Array (
	'host' => '10.11.4.190',
    'username' => 'test',
    'password' => 'test',
    'db'=> 'skp-quiz',
    'port' => 3306,
    'prefix' => '',
    'charset' => 'utf8'));