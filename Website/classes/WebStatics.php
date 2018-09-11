<?php

class WebStatics
{
    private $_db;

    /**
     * LoginClass constructor.
     */
    public function __construct()
    {
        $this->_db = MysqliDb::getInstance();
    }

    /**
     * Get a total count of all questions in the database.
     * @return int
     */
    public function getTotalQuestionCount()
    {
        $this->_db->get('questions');
        return $this->_db->count;
    }

    /**
     * Get a total count of all users in the database.
     * @return int
     */
    public function getTotalUserCount()
    {
        $this->_db->get('users');
        return $this->_db->count;
    }

    /**
     * Get a total count of all teams in the database.
     * @return int
     */
    public function getTotalTeamCount()
    {
        $this->_db->get('teams');
        return $this->_db->count;
    }

}