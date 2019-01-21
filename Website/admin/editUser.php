<?php
include_once (__DIR__ . '/../autoload.php');

Identification::needToBeLoggedIn();

$Api = new ApiClass();
$userId = $_GET['id'];
$user = $Api->getUserById($userId);
$teamArray = $Api->getAllTeamIdsAndNames();

if (isset($_POST['submit']))
{
    // $name = null, $username = null, $password = null, $teamId = null, $isAdmin = null
    $teamId = $_POST['selectedTeam'];

    if ($teamId == 0)
        $teamId = NULL;

    $done = $Api->editUserById(
      $userId,
      $_POST['name'],
      $_POST['username'],
      $_POST['password'],
      $teamId,
      $_POST['selectedAdmin']
    );
}

?>
<!DOCTYPE html>

<html>
<head>
    <title>Rediger bruger</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        #fieldInput {
            width: 100%;
        }
    </style>
</head>
<body>
<div class="col-sm-12">
    <?php if (!isset($done)): ?>
    <form action="" method="post">
        <table class="table text-right">
            <tbody>
                <tr>
                    <td>Navn:</td>
                    <th><input id="fieldInput" type="text" value="<?=$user['name']?>" name="name"/></th>
                </tr>
                <tr>
                    <td>Brugernavn:</td>
                    <th><input id="fieldInput" type="text" value="<?=$user['username']?>" name="username"/></th>
                </tr>
                <tr>
                    <td>Kodeord:</td>
                    <th><input id="fieldInput" type="text" value="<?=$user['password']?>" name="password"/></th>
                </tr>
                <tr>
                    <td>Team:</td>
                    <th>
                        <select id="fieldInput" name="selectedTeam">
                            <option value="0">Ingen team</option>
                            <?php
                            foreach($teamArray as $key => $value)
                            {
                                if ($user['teamId'] == $key)
                                    echo "<option selected value='{$key}'>{$value} (nuværende)</option>";
                                else
                                    echo "<option value='{$key}'>{$value}</option>";
                            }
                            ?>
                        </select>
                    </th>
                </tr>
                <tr>
                    <td>Admin:</td>
                    <th>
                        <select id="fieldInput" name="selectedAdmin">
                            <option <?php if ($user['isAdmin']) echo "selected"; ?> value="1">Ja <?php if ($user['isAdmin']) echo "(nuværende)"; ?></option>
                            <option <?php if (!$user['isAdmin']) echo "selected"; ?> value="0">Nej <?php if (!$user['isAdmin']) echo "(nuværende)"; ?></option>
                        </select>
                </tr>
                <tr>
                    <td></td>
                    <td align="right">
                        <input type="submit" name="submit" value="Gem" />
                    </td>
                </tr>
            </tbody>
        </table>

    </form>
    <?php else: ?>
        <center>
            Denne bruger er nu redigeret!<br>
            <a onclick="window.close()">Luk</a>
        </center>
    <?php endif; ?>
</div>


</body>
</html>