<?php
/**
 * Created by PhpStorm.
 * User: Simme
 * Date: 25/09/2018
 * Time: 09.55
 */

class Identification
{
    /**
     * Check if user is logged in or not by using a session
     * @return bool
     */
    public static function isUserLoggedIn()
    {
        return isset($_SESSION['loggedIn']) ? true : false;
    }

    /**
     * Check if client is logged in. If not, then it will redirect client
     * to a specific path. By default "login.php"
     * @param string $path
     */
    public static function needToBeLoggedIn($path = "login.php")
    {
        if (self::isUserLoggedIn() == false) {
            header('Location:' . $path);
        }
    }

    /**
     * Return username of the client using the application and is logged in.
     * @return string
     */
    public static function getUsername()
    {
        if (self::isUserLoggedIn()) {
            return $_SESSION['loggedIn']['username'];
        } else {
            return null;
        }
    }
}