<?php
include_once (__DIR__ . '/../config.php');
include_once (__DIR__ . '/../classes/LoginClass.php');

$Login = new LoginClass();

// Check if user is logged in
if (isset($_SESSION['loggedIn']))
{
    header('Location:index.php');
}

if (isset($_POST['login']))
{
    $Login->handleLogin($_POST['username'], $_POST['password'], 'index.php');
}

?>
<!DOCTYPE html>

<html>
<head>
    <title>Login</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
</head>
<body>
<div class="container">
    <center>
        <?php
        $Login->printErrors();
        ?>
        <img src="img/ICONBIG.png" width="300px"/>

        <form action="" method="post">
            <table class="text-right">
                <tr>
                    <td>Brugernavn:</td>
                    <td><input name="username" type="text"></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><input name="password" type="password"></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input type="submit" value="Log ind" name="login"/></td>
                </tr>
            </table>

        </form>
    </center>
</div>


</body>
</html>