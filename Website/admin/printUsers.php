<?php
if (empty($_GET))
{
    die("Du kan ikke download noget. Der mangler nogle parameter i URL'en.");
}

// This is needed
require_once (__DIR__ . '/../autoload.php');

$api = new ApiClass();
$team = $_GET['team'];

$teamArray = $api->getAllTeamIdsAndNames();
$teamArray[null] = "Ingen team";

// to fix the problem from the users.php
if ($team == "noTeam")
{
    $team = null;
}

if ($team == "all")
    $users = $api->getAllUsers_Admin();
else
    $users = $api->getUsersByTeamId_Admin($team);

?>
<!DOCTYPE html>

<html>
<head>
    <link href="css/bootstrap.css" rel="stylesheet" />
</head>
<body onload="window.print()">
    <table class="table">
        <tr>
            <th>Id</th>
            <th>Navn</th>
            <th>Brugernavn</th>
            <th>Kodeord</th>
            <th>Team</th>
        </tr>
        <?php
        foreach ($users as $user)
        {
            echo "<tr><td>{$user['id']}</td><td>{$user['name']}</td><td>{$user['username']}</td><td>{$user['password']}</td><td>{$teamArray[$user['teamId']]}</td></tr>";
        }
        ?>
    </table>
</body>
</html>








