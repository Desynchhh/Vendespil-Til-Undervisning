<?php
/**
 * Created by PhpStorm.
 * User: Simme
 * Date: 10/09/2018
 * Time: 13.16
 */

session_start();
session_destroy();
header('Location:login.php');