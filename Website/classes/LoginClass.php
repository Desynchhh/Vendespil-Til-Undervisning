<?php
/**
 * Created by PhpStorm.
 * User: Simme
 * Date: 10/09/2018
 * Time: 12.28
 */

class LoginClass
{
    private $_db;
    private $_errors = array();

    /**
     * LoginClass constructor.
     */
    public function __construct()
    {
        $this->_db = MysqliDb::getInstance();
    }

    /**
     * Print error messages with the bootstrap alert.
     */
    public function printErrors()
    {
        // do nothing if there are no errors
        if (empty($this->_errors))
        {
            return;
        }

        echo '<div class="alert alert-danger" role="alert">';

        foreach ($this->_errors as $error)
        {
            echo $error . '<br>';
        }

        echo '</div>';
    }

    /**
     * This method handles login. If it success then it will
     * redirect to index page.
     * @param $username
     * @param $password
     * @param null $redirectPage
     */
    public function handleLogin($username,$password, $redirectPage = null)
    {
        // make sure that username and password is set
        if (empty($username) || empty($password))
        {
            $this->_errors[] = "Du mangler at indtast brugernavn eller kodeord.";
            return;
        }

        // check if user exists
        if (!$this->doUsernameExist($username))
        {
            $this->_errors[] = "Denne bruger findes ikke!";
            return;
        }

        // check if username and password match
        if (!$this->doUsernameAndPasswordExist($username,$password))
        {
            $this->_errors[] = "Dit brugernavn/kodeord er forkert. Prøv igen.";
            return;
        }

        // check if user is administrator
        if (!$this->isAdmin($username))
        {
            $this->_errors[] = "Denne bruger er ikke administrator.";
            return;
        }

        // save username in loggedIn session for later use
        $_SESSION['loggedIn'] = array(
            'username' => $username
        );

        // redirect user to file if specified
        if ($redirectPage != null)
        {
            header('Location:' . $redirectPage);
        }
    }

    /**
     * Return array of all errors.
     * @return array
     */
    public function getErrors()
    {
        return $this->_errors;
    }

    /**
     * Check if username do exist in database.
     * @param $username
     * @return bool
     */
    private function doUsernameExist($username)
    {
        $this->_db->where('username', $username);
        $user = $this->_db->getOne('users');

        return empty($user) ? false : true;
    }

    /**
     * Check if username and password match to a user
     * in the database.
     * @param $username
     * @param $password
     * @return bool
     */
    private function doUsernameAndPasswordExist($username,$password)
    {
        $this->_db->where('username', $username);
        $this->_db->where('password', $password);
        $user = $this->_db->getOne('users');

        return empty($user) ? false : true;
    }

    /**
     * Return bool on if username is an administrator.
     * @param $username
     * @return bool
     */
    private function isAdmin($username)
    {
        $this->_db->where('username', $username);
        $user = $this->_db->getOne('users');
        return $user['isAdmin'] == 1 ? true : false;
    }

}