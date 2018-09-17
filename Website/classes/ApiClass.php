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
        if (is_bool($this->_returnData)) {
            $this->_returnData = Array(
                'status' => $this->_returnData
            );
        }

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
     * @param $id
     * @return array
     */
    public function getTeamById($id)
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
    public function getTeamByName($name)
    {
        $this->_db->where('name', $name);
        $data = $this->_db->get('teams');
        return $data;
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
     * @param $id
     * @return array
     */
    public function getQuestionsByTeamId($id)
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
    public function getQuestionsByUserId($id)
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
    public function getQuestionById($id)
    {
        $this->_db->where('id', $id);
        $question = $this->_db->get('questions');
        return $question;
    }

    /**
     * Delete question from the database based on ID
     * @param $id
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
     * @param $id
     * @return bool
     */
	public function deleteAllQuestionsByUserId($id)
	{
		$this->_db->where('userId', $id);
        return $this->_db->delete('users') ? true : false;
	}

    /**
     * This method edits a question in the database based on the given ID.
     * The TeamId and UserID is not needed to edit a question, just keep
     * this in mind.
     * @param $id
     * @param $question
     * @param $correctAnswer
     * @param $wrongAnswerOne
     * @param $wrongAnswerTwo
     * @param $wrongAnswerThree
     * @param null $teamId
     * @param null $userId
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
     * Insert question into the database. The ID will be given in the database table, no need to worry about that.
     * @param $question
     * @param $correctAnswer
     * @param $wrongAnswerOne
     * @param $wrongAnswerTwo
     * @param $wrongAnswerThree
     * @param $teamId
     * @param $userId
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
     * @param $id
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
        $returnUsers = $this->_db->get('users');

        return $returnUsers;
    }

    /**
     * Return array of user based on username.
     * @param $name
     * @return array
     */
    public function getUserByUsername($name)
    {
        $this->_db->where ('username', $name);
        return $this->_db->getOne('users');
    }

    /**
     * Return array of users whre teamId
     * is equal to the given parameter.
     * @param $id
     * @return array
     */
    public function getUsersByTeamId($id)
    {
        $this->_db->where ('teamId', $id);
        return $this->_db->get('users');
    }

    /**
     * Return array of all users that has no team id
     * @return array
     */
    public function getAllUsersWithNoTeam()
    {
        $this->_db->where('teamId', null);
        return $this->_db->get('users');
    }

    /**
     * Return array of user based on userId.
     * @param $id
     * @return array
     */
    public function getUserById($id)
    {
        $this->_db->where ('id', $id);
        return $this->_db->getOne('users');
    }

    /**
     * Return array with all users in the database.
     * @return array
     */
    public function getAllUsers()
    {
        return $this->_db->get('users');
    }

    /**
     * Return bool on if username and password match in the database.
     * @param $username
     * @param $password
     * @return bool
     */
    public function checkUsernameAndPassword($username, $password)
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