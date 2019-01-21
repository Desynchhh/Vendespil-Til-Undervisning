<?php
include_once (__DIR__ . '/_header.php');

$teamArray = $Api->getAllTeamIdsAndNames();
$teamArray[null] = "Ingen team";

$admin = array("Nej", "Ja");

$selectedTeam = null;
if (isset($_GET['selectedTeam']))
    $selectedTeam = $_GET['selectedTeam'];


?>
<div class="col-sm-12 main-text">
    <div class="col-sm-6">
        <form action="" method="get">
            Hold:
            <select name="selectedTeam" id="selectedTeam">
                <!-- <option value="null">- vælg -</option> -->
                <option value="all">Alle</option>
                <option value="noTeam" <?php if ($selectedTeam == "noTeam") echo "selected"; ?> >Brugere uden team</option>
                <?php



                foreach ($Api->getAllTeams() as $team)
                {
                    if ($selectedTeam == $team['id'])
                        echo "<option value='{$team['id']}' selected>{$team['name']}</option>";
                    else
                        echo "<option value='{$team['id']}'>{$team['name']}</option>";
                }

                ?>
            </select>
            <input type="submit" value="Se brugere">
        </form>
    </div>
    <div class="col-sm-6 text-right">
        <button title="Denne bruger du til at opret nogle brugere til en/flere brugere.">Opret bruger(e)</button>
        <button onclick="popitup('printUsers.php?team=<?=$selectedTeam?>', 'Print brugere', '700', '700')" title="Print en list med de brugere du ser på listen lige nu.">Print liste med brugere ud</button>
    </div>
    <div class="col-sm-12" style="padding-top: 10px;">
        <table id="usernameTable" class="table">
            <tr>
                <th>ID</th>
                <th>Navn</th>
                <th>Brugernavn</th>
                <th>Kode</th>
                <th>Team</th>
                <th>Admin</th>
                <th></th>
            </tr>
        <?php

        if (isset($_GET['selectedTeam']))
        {


            if ($selectedTeam == "all")
            {
                $users = $Api->getAllUsers_Admin();
            }
            elseif ($selectedTeam == "noTeam")
            {
                $users = $Api->getUsersByTeamId_Admin(null);
            }
            else
            {
                $users = $Api->getUsersByTeamId_Admin($selectedTeam);
            }

            if ($selectedTeam != null)
            {
                foreach ($users as $user)
                {

                    echo "
                    <tr>
                        <td>{$user['id']}</td>
                        <td>{$user['name']}</td>
                        <td>{$user['username']}</td>
                        <td>{$user['password']}</td>
                        <td>{$teamArray[$user['teamId']]}</td>
                        <td>{$admin[$user['isAdmin']]}</td>
                        <td>
                            <a onclick='popitup(\"editUser.php?id={$user['id']}\", \"Rediger bruger\", 400, 400)'' href=''><span class='glyphicon glyphicon-pencil' title='Rediger'></span></a>
                            <a onclick='popitup(\"deleteUser.php?id={$user['id']}\", \"Slet bruger\", 400, 100)'' href=''><span class='glyphicon glyphicon-remove' title='Slet bruger'></span></a>
                        </td>
                    </tr>
                ";


                }
            }
        }



        ?>
        </table>
    </div>
</div>
<?php
include_once (__DIR__ . '/_footer.php');
?>

