<?php
include_once (__DIR__ . '/../autoload.php');

Identification::needToBeLoggedIn();

$Api = new ApiClass();
$userId = $_GET['id'];
$user = $Api->getUserById($userId);

// Mare sure post requests has been made
if (isset($_GET['button']))
{
    // Yes to delete user
    if ($_GET['button'] == 1)
    {
        // Make sure that the delete has been done correct
        $done = $Api->deleteUserByUserId($userId);

        if (!$done)
        {
            die("Der gik noget galt da vi prøvede at slet denne bruger. Prøv lige igen, ellers kontakt en udvikler.");
        }

    }

}

?>
<!DOCTYPE html>

<html>
<head>
    <title>Slet bruger</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
<?php if (!isset($_GET['button'])) : ?>
    <center>
        Er du sikker på du vil slet "<?=$user['name']?>" fra databasen?<br>
        <a href="<?=$_SERVER['REQUEST_URI']?>&button=1">Ja</a> |
        <a href="<?=$_SERVER['REQUEST_URI']?>&button=0">Nej</a>
    </center>
<?php elseif ($_GET['button'] == 1): ?>
    <center>
        Denne bruger er nu slettet!<br>
        <a onclick="window.close()">Luk</a>
    </center>
<?php elseif ($_GET['button'] == 0): ?>
<script>
    window.close();
</script>";
<?php endif; ?>



</body>
</html>