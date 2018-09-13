<?php

// Require needed files for the API to work
include_once (__DIR__ . '/classes/dbObject.php'); // Used for the MySQLIDB object
include_once (__DIR__ . '/classes/MysqliDb.php'); // Class used for database

session_start();

// Database object used for the database
$db = new MysqliDb (Array (
    'host' => 'localhost',
    'username' => 'root',
    'password' => '',
    'db'=> 'skp-quiz',
    'port' => 3306,
    'prefix' => '',
    'charset' => 'utf8'));