<?php
include_once (__DIR__ . '/_header.php');
?>
<div class="col-sm-12 main-text ">
    Velkommen. Her kan du opret nye hold, se spørgsmål, slet spørgmål og brugere.
</div>
<div class="col-sm-6 border float-left">
    <h4>Statestik</h4>
    <table class="text-right">
        <tr>
            <td>Antal spørgsmål:</td>
            <td><?=$WebStatics->getTotalQuestionCount()?></td>
        </tr>
        <tr>
            <td>Antal brugere:</td>
            <td><?=$WebStatics->getTotalUserCount()?></td>
        </tr>
        <tr>
            <td>Antal teams:</td>
            <td><?=$WebStatics->getTotalTeamCount()?></td>
        </tr>
    </table>
</div>
<!--
<div class="col-sm-6 border float-left">
    <h4>App-Indstillinger</h4>
    <table class="text-right">
        <tr>
            <td>APP-Login:</td>
            <td><span style="color:lime">Tillad</span></td>
        </tr>
    </table>
    Her kan du tillade og forbyde login på selve appen. Du kan f.eks. slå login fra hvis nu du ikke ønsker nogen skal log ind
    imens at der bliver tjekker spørgsmål eller andet.
</div>
-->
<?php
include_once (__DIR__ . '/_footer.php');
?>

