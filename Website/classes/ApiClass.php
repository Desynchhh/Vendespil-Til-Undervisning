<?php

/**
 * This is the class used to get and return data to database
 */
class ApiClass {
    protected $_db;
    protected $_returnData;

    /**
     * ApiClass constructor.
     */
    public function __construct()
    {
        $this->_db = MysqliDb::getInstance();
        $this->_returnData = null;
    }

    /**
     * Return the data og _returnData that is getting set after each method has been
     * run in the handlePostRequest. When you get the data form this getReturnDataAsJson() method
     * then it will already be json format.
     * @return string json
     */
    public function getReturnDataAsJson()
    {
        if (is_bool($this->_returnData))
        {
            $this->_returnData = Array(
                'status' => $this->_returnData
            );
        }

        if ($this->_returnData == null || $this->_returnData == "null" )
        {
            $this->_returnData = $this->errorMessage("There was no data to show.");
        }

        return $this->formatToJson($this->_returnData);
    }

    /**
     * Handles the POST requests send to the API class to make sure that
     * the correct actions runs the correct values. If you want the data
     * after the API as done it's job, then you need to call "getReturnDataAsJson".
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
            case "getUsersByTeamId":
                $this->_returnData = $this->getUsersByTeamId(@$_POST['id']);
                break;
            case "getAllUsersWithQuestionsByTeamId":
                $this->_returnData = $this->getAllUsersWithQuestionsByTeamId(@$_POST['id']);
                break;

            /**
             * Question API methods
             */
            case 'getAllQuestions':
                $this->_returnData = $this->getAllQuestions();
                break;
            case 'getQuestionsByTeamId':
                $this->_returnData = $this->getQuestionsByTeamId(@$_POST['id']);
                break;
            case 'getQuestionsByUserId':
                $this->_returnData = $this->getQuestionsByUserId(@$_POST['id']);
                break;
            case 'getQuestionById':
                $this->_returnData = $this->getQuestionById(@$_POST['id']);
                break;
            case 'deleteQuestionById':
                $this->_returnData = $this->deleteQuestionById(@$_POST['id']);
                break;
            case 'deleteAllQuestionsByUserId':
                $this->_returnData = $this->deleteAllQuestionsByUserId(@$_POST['id']);
                break;
            case 'editQuestionById':
                $this->_returnData = $this->editQuestionById(
                  @$_POST['id'],
                  @$_POST['question'],
                  @$_POST['correctAnswer'],
                  @$_POST['wrongAnswerOne'],
                  @$_POST['wrongAnswerTwo'],
                  @$_POST['wrongAnswerThree'],
                  @$_POST['teamId'],
                  @$_POST['userId']
                );
                break;
            case 'insertNewQuestion':
                $this->_returnData = $this->insertNewQuestion(
                    @$_POST['question'],
                    @$_POST['correctAnswer'],
                    @$_POST['wrongAnswerOne'],
                    @$_POST['wrongAnswerTwo'],
                    @$_POST['wrongAnswerThree'],
                    @$_POST['teamId'],
                    @$_POST['userId']
                );
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
    public function getAllTeams()
    {
        $data = $this->_db->get('teams');
        return $data;
    }

    /**
     * Return array of team form database based on teamId.
     * @param $id int
     * @return array
     */
    public function getTeamById($id)
    {
        $this->_db->where('id', $id);
        $data = $this->_db->get('teams');
        return $data;
    }

    /**
     * Return simple array with all teams. The key of the array is the teamId
     * and value of keys is the teamName.
     * For an example:
     * [1] => "Team 1"
     * [2] => "Team 2"
     * etc.
     * @return array
     */
    public function getAllTeamIdsAndNames()
    {
        $teams = $this->getAllTeams();
        $returnArray = array();

        foreach ($teams as $key => $value)
        {
            $returnArray[$value['id']] = $value['name'];
        }

        return $returnArray;
    }

    /**
     * Return array of teams form database based on name.
     * @param $name string
     * @return array
     */
    public function getTeamByName($name)
    {
        $this->_db->where('name', $name);
        return $this->_db->get('teams');
    }

    /**
     * Return array of all questions from the database.
     * @return array
     */
    public function getAllQuestions()
    {
        $questions = $this->_db->get('questions');
        return $questions;
    }

    /**
     * Return array of questions form the database where TeamId is equal
     * to the $id parameter.
     * @param $id int
     * @return array
     */
    public function getQuestionsByTeamId($id)
    {
        $this->_db->where('teamId', $id);
        return $this->_db->get('questions');
    }

    /**
     * Return array of questions form the database where UserId is equal
     * to the $id parameter.
     * @param $id int
     * @return array
     */
    public function getQuestionsByUserId($id)
    {
        $this->_db->where('userId', $id);
        return $this->_db->get('questions');
    }

    /**
     * Return array of a question based on the id.
     * @param $id int
     * @return array
     */
    public function getQuestionById($id)
    {
        $this->_db->where('id', $id);
        return $this->_db->get('questions');
    }

    /**
     * Delete question from the database based on ID
     * @param $id int
     * @return bool
     */
    public function deleteQuestionById($id)
    {
        $this->_db->where('id', $id);
        return $this->_db->delete('questions') ? true : false;
    }

    /**
     * Delete all questions from database where userId is equal to the
     * ID parameter. It will then return bool on if the delete has been
     * done or not.
     * True = Deleted
     * False = Error happened
     * @param $id int
     * @return bool
     */
	public function deleteAllQuestionsByUserId($id)
	{
		$this->_db->where('userId', $id);
        return $this->_db->delete('questions') ? true : false;
	}

    /**
     * This method edits a question in the database based on the given ID.
     * The TeamId and UserID is not needed to edit a question, just keep
     * this in mind.
     * @param $id int
     * @param $question string
     * @param $correctAnswer string
     * @param $wrongAnswerOne string
     * @param $wrongAnswerTwo string
     * @param $wrongAnswerThree string
     * @param null $teamId int
     * @param null $userId int
     * @return bool
     */
	public function editQuestionById($id, $question, $correctAnswer, $wrongAnswerOne, $wrongAnswerTwo, $wrongAnswerThree, $teamId = null, $userId = null)
    {
        $data = Array (
            'question'       => $question,
            'correctAnswer'  => $correctAnswer,
            'wrongAnswer1'   => $wrongAnswerOne,
            'wrongAnswer2'   => $wrongAnswerTwo,
            'wrongAnswer3'   => $wrongAnswerThree
        );

        if ($userId != null)
        {
            $data['userId'] = $userId;
        }

        if ($teamId != null)
        {
            $data['teamId'] = $teamId;
        }

        $this->_db->where ('id', $id);

        return $this->_db->update('questions', $data) ? true : false;
    }

    /**
     * Update the user table in the database.
     * @param $id
     * @param null $name
     * @param null $username
     * @param null $password
     * @param null $teamId
     * @param null $isAdmin
     * @return bool
     */
    public function editUserById($id, $name = null, $username = null, $password = null, $teamId = "setNull", $isAdmin = null)
    {
        $data = array();

        if ($name     != null) $data['name']     = $name;
        if ($username != null) $data['username'] = $username;
        if ($password != null) $data['password'] = $password;
        if ($teamId   != "setNull") $data['teamId']   = $teamId;
        if ($isAdmin  != null) $data['isAdmin']  = $isAdmin;

        $this->_db->where ('id', $id);

        return $this->_db->update('users', $data) ? true : false;
    }

    /**
     * Insert question into the database. The ID will be given in the database table, no need to worry about that.
     * @param $question string
     * @param $correctAnswer string
     * @param $wrongAnswerOne string
     * @param $wrongAnswerTwo string
     * @param $wrongAnswerThree string
     * @param $teamId int
     * @param $userId int
     * @return bool
     */
    public function insertNewQuestion($question, $correctAnswer, $wrongAnswerOne, $wrongAnswerTwo, $wrongAnswerThree, $teamId, $userId)
    {
        $data = Array (
            'question'       => $question,
            'correctAnswer'  => $correctAnswer,
            'wrongAnswer1'   => $wrongAnswerOne,
            'wrongAnswer2'   => $wrongAnswerTwo,
            'wrongAnswer3'   => $wrongAnswerThree,
            'teamId'         => $teamId,
            'userId'         => $userId
        );

        $id = $this->_db->insert ('questions', $data);

        return $id ? true : false;
    }

    /**
     * Return array of users based on teamId where
     * users have questions in the question table in database.
     * So only users that questions will be returned by this
     * function.
     * @param $id int
     * @return array
     */
    public function getAllUsersWithQuestionsByTeamId($id)
    {
        // Make sure that id is not null
        if ($id == null)
        {
            return $this->errorMessage("There needs to be an ID parameter for this to work!");
        }

        // Here we: SELECT * FROM questions WHERE teamId = $id GROUP BY userId
        $this->_db->where('teamId', $id);
        $this->_db->groupBy('userId');
        $userIDsWithQuestions = $this->_db->get('questions', null, 'userId');

        $whereArray = array();
        foreach ($userIDsWithQuestions as $key => $value)
        {
            array_push($whereArray, $value['userId']);
        }

        if (empty($whereArray))
        {
            return $whereArray;
        }

        $this->_db->where('id', $whereArray, 'IN');

        $select = Array('id', 'name', 'username', 'isAdmin', 'teamId');
        $returnUsers = $this->_db->get('users', null, $select);

        return $returnUsers;
    }

    /**
     * Update the password on a user row in the database based on
     * the user id. It will then return bool on if it has been done
     * or if some error happened.
     * @param $id int
     * @param $newPassword string
     * @return bool
     */
    public function editUserPasswordByUserId($id, $newPassword)
    {
        $data = Array ('password' => $newPassword);
        $this->_db->where ('id', $id);
        return $this->_db->update('users', $data) ? true : false;
    }

    /**
     * Delete user in the database based on user ID.
     * @param $id int
     * @return bool
     */
    public function deleteUserByUserId($id)
    {
        $this->_db->where('id', $id);
        return $this->_db->delete('users') ? true : false;
    }

    /**
     * Return array of user based on username.
     * @param $name string
     * @return array
     */
    public function getUserByUsername($name)
    {
        $this->_db->where ('username', $name);
        $select = Array('id', 'name', 'username', 'isAdmin', 'teamId');
        return $this->_db->getOne('users', $select);
    }

    /**
     * Return array of users where teamId
     * is equal to the given parameter.
     * @param $id int
     * @return array
     */
    public function getUsersByTeamId($id)
    {
        $this->_db->where ('teamId', $id);
        $select = Array('id', 'name', 'username', 'isAdmin', 'teamId');
        return $this->_db->get('users', null, $select);
    }

    /**
     * Return array of users where teamId
     * is equal to the given parameter.
     * @return array
     */
    public function getAllUsers_Admin()
    {
        try
        {
            $this->_db->orderBy('id');
            return $this->_db->get('users');
        }
        catch (exception $e)
        {
            die($e);
        }
    }


    /**
     * Return array of users where teamId
     * is equal to the given parameter.
     * @param $id int
     * @return array
     */
    public function getUsersByTeamId_Admin($id)
    {
        if ($id == null || $id == "nulL")
        {
            $this->_db->where('teamId', NULL, 'IS');
        }
        else
        {
            $this->_db->where ('teamId', $id);
        }

        return $this->_db->get('users');
    }

    /**
     * Return array of all users that has no team id
     * @return array
     */
    public function getAllUsersWithNoTeam()
    {
        $this->_db->where('teamId', NULL, 'IS');
        $select = Array('id', 'name', 'username', 'password', 'isAdmin', 'teamId');
        return $this->_db->get('users', null, $select);
    }

    /**
     * Return array of user based on userId.
     * @param $id int
     * @return array
     */
    public function getUserById($id)
    {
        $this->_db->where ('id', $id);
        $select = Array('id', 'name', 'username', 'isAdmin', 'teamId');
        return $this->_db->getOne('users', null, $select);
    }

    /**
     * Return array with all users in the database.
     * @return array
     */
    public function getAllUsers()
    {
        $select = Array('id', 'name', 'username', 'isAdmin', 'teamId');
        return $this->_db->get('users', null, $select);
    }

    /**
     * Return password of user based on username.
     * @param $name
     * @return mixed
     */
    private function getUserPasswordByUsername($name)
    {
        $this->_db->where('username', $name);
        $user = $this->_db->getOne('users', Array('password'));
        return $user['password'];
    }

    /**
     * Return bool on if username and password match in the database.
     * @param $username string
     * @param $password string
     * @return bool
     */
    public function checkUsernameAndPassword($username, $password)
    {
        if ($username == null || $password == null)
        {
            return false;
        }


        $userPassword = $this->getUserPasswordByUsername($username);

        return $userPassword == $password ? true : false;
    }

    /**
     * Return a json string based on array input.
     * @param $data string
     * @return string
     */
    public function formatToJson($data)
    {
        return json_encode($data, JSON_UNESCAPED_UNICODE);
    }

    /**
     * Return a json string formatted as the error message we use.
     * @param $errorMessage
     * @return array
     */
    public function errorMessage($errorMessage)
    {
        return (Array( "error" => $errorMessage ));
    }

    public function checkUsernameAndPasswordJson($username, $password)
    {
        $login = $this->checkUsernameAndPassword($username,$password);

        if ($login)
        {
            $user = $this->getUserByUsername($username);
            return Array(
                "login" => $login,
                "userdata" => $user
            );
        }
        else
        {

            return Array( "login" => $login );
        }
    }

}