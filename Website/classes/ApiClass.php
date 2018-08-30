<?php

/**
 * This is the class used to get and return data to database
 */
class ApiClass {
    private $_db;
    private $_returnData;

    /**
     * ApiClass constructor.
     */
    public function __construct() {
        $this->_db = MysqliDb::getInstance();
        $this->_returnData = null;
    }

    /**
     * Return the data og _returnData that is getting set after each method has been
     * run in the handlePostRequest. When you get the data form this getReturnData() method
     * then it will already be json format.
     * @return string Json
     */
    public function getReturnDataAsJson()
    {
        if ($this->_returnData == null || $this->_returnData == "null" ) {
            $this->_returnData = $this->errorMessage("There was no data to show.");
        }

        return $this->formatToJson($this->_returnData);
    }

    /**
     * Handles the POST requests send to the API class to make sure that
     * the correct actions runs the correct values. If you want the data
     * after the API as done it's job, then you need to call "getReturnData".
     * This variable will contain the requested data.
     */
    public function handlePostRequest()
    {
        $requested_action = $_POST['action'];

        switch ($requested_action)
        {
            /**
             * User API methods
             */
            case "getAllUsers":
                $this->_returnData = $this->getAllUsers();
                break;
            case "getUserById":
                $this->_returnData = $this->getUserById(@$_POST['id']);
                break;
            case "getUserByUsername":
                $this->_returnData = $this->getUserByUsername(@$_POST['name']);
                break;

            /**
             * Question API methods
             */
            case "getAllQuestions":
                $this->_returnData = $this->getAllQuestions();
                break;
            case "getQuestionsByTeamId":
                $this->_returnData = $this->getQuestionsByTeamId(@$_POST['id']);
                break;
            case "getQuestionsByUserId":
                $this->_returnData = $this->getQuestionsByUserId(@$_POST['id']);
                break;
            case "getQuestionById":
                $this->_returnData = $this->getQuestionById(@$_POST['id']);
                break;

            /**
             * Team API methods
             */
            case "getAllTeams":
                $this->_returnData = $this->getAllTeams();
                break;
            case "getTeamById":
                $this->_returnData = $this->getTeamById(@$_POST['id']);
                break;
            case "getTeamByName":
                $this->_returnData = $this->getTeamByName(@$_POST['name']);
                break;

            /**
             * Other methods
             */
            case "loginCheck":
                $this->_returnData = $this->checkUsernameAndPasswordJson(@$_POST['username'], @$_POST['password']);
                break;

            default:
                $this->_returnData = $this->errorMessage("Don't know this action, please try again.");
                return;
        }
    }

    /**
     * Return all teams in the database.
     * @return array
     */
    function getAllTeams()
    {
        $data = $this->_db->get('teams');
        return $data;
    }

    /**
     * Return array of team form database based on teamId.
     * @param $id
     * @return array
     */
    function getTeamById($id)
    {
        $this->_db->where('id', $id);
        $data = $this->_db->get('teams');
        return $data;
    }

    /**
     * Return array of teams form database based on name.
     * @param $name
     * @return array
     */
    function getTeamByName($name)
    {
        $this->_db->where('name', $name);
        $data = $this->_db->get('teams');
        return $data;
    }

    /**
     * Return array of all questions from the database.
     * @return array
     */
    function getAllQuestions()
    {
        $questions = $this->_db->get('questions');
        return $questions;
    }

    /**
     * Return array of questions form the database where TeamId is equal
     * to the $id parameter.
     * @param $id
     * @return array
     */
    function getQuestionsByTeamId($id)
    {
        $this->_db->where('teamId', $id);
        $questions = $this->_db->get('questions');
        return $questions;
    }

    /**
     * Return array of questions form the database where UserId is equal
     * to the $id parameter.
     * @param $id
     * @return array
     */
    function getQuestionsByUserId($id)
    {
        $this->_db->where('userId', $id);
        $questions = $this->_db->get('questions');
        return $questions;
    }

    /**
     * Return array of a question based on the id.
     * @param $id
     * @return array
     */
    function getQuestionById($id)
    {
        $this->_db->where('id', $id);
        $question = $this->_db->get('questions');
        return $question;
    }

    /**
     * Return array of user based on username.
     * @param $username
     * @return array
     */
    function getUserByUsername($name)
    {
        $this->_db->where ('username', $name);
        return $this->_db->getOne('users');
    }

    /**
     * Return array of user based on userId.
     * @param $id
     * @return array
     */
    function getUserById($id)
    {
        $this->_db->where ('id', $id);
        return $this->_db->getOne('users');
    }

    /**
     * Return array with all users in the database.
     * @return array
     */
    function getAllUsers()
    {
        return $this->_db->get('users');
    }

    /**
     * Return bool on if username and password match in the database.
     * @param $username
     * @param $password
     * @return bool
     */
    function checkUsernameAndPassword($username, $password)
    {
        if ($username == null || $password == null)
            return false;

        $user = $this->getUserByUsername($username);
        if ($user['password'] == $password) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * Return a json string based on array input.
     * @param $data
     * @return string
     */
    function formatToJson($data)
    {
        return json_encode($data, JSON_UNESCAPED_UNICODE);
    }

    /**
     * Return a json string formatted as the error message we use.
     * @param $errorMessage
     * @return array
     */
    function errorMessage($errorMessage)
    {
        return ([ "error" => $errorMessage ]);
    }

    function message($array)
    {
        return $array;
    }

    function checkUsernameAndPasswordJson($username, $password)
    {

        $login = $this->checkUsernameAndPassword($username,$password);
        if ($login)
        {
            $user = $this->getUserByUsername($username);

            return $this->message([
                "login" => $login,
                "userdata" => $user
            ]);
        }
        else
        {
            return $this->message(["login" => $login]);
        }
    }


}