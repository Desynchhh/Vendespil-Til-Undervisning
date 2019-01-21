<?php

// Start session
session_start();

// Database object used for the database
$db = new MysqliDb (Array (
    'host' => 'localhost',
    'username' => 'root',
    'password' => '',
    'db'=> 'skp-quiz',
    'port' => 3306,
    'prefix' => '',
    'charset' => 'utf8')
);
