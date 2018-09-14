<?php
include_once (__DIR__ . '/_header.php');
$api = new ApiClass();
?>
<div class="col-sm-12 main-text ">
    Her kan du se alle de teams som er oprettet i databasen.
    <ul>
        <?php
            // loop all the teams
            $teams = $api->getAllTeams();

            if (!empty($teams))
            {
                // get users with no team first
                $total_users_with_no_team = count($api->getAllUsersWithNoTeam());
                echo "<li>Brugere uden team (antal brugere: {$total_users_with_no_team}) <a href=''>se brugere</a></li>";

                // now we loop all the teams and so on
                foreach ($teams as $team)
                {
                    // TODO: Make it so it only make one SQL request and not the number of teams... optimize!
                    $users = $api->getUsersByTeamId($team['id']);
                    $total_users = count($users);

                    // check if team id is the "no team"
                    echo "<li>{$team['name']} (antal brugere: {$total_users}) <a href=''>se brugere</a> | <a href=''>rediger</a> | <a href=''>slet</a></li>";
                }
            }
            else
            {
                echo "Der er ikke oprettet nogle teams.";
            }


        ?>
    </ul>
</div>
<?php
include_once (__DIR__ . '/_footer.php');
?>

