<?php
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

    $name =  $_POST['editName'];
    $ondate =  $_POST['addOndate'];
    $salaryType =  $_POST['addSalaryType'];
    $dailyWage =  $_POST['addDayWage'];
    $timeWage =  $_POST['addTimeWage'];

    $query = " SELECT staff_id FROM staffinfo WHERE name = '".$name."' ";
    $result = mysqli_query($conn, $query);
    $row = mysqli_fetch_assoc($result);
    $staff_id = $row['staff_id'];

    $sql = " UPDATE staffinfo SET ondate='".$ondate."', salaryType='".$salaryType."', dailyWage='".$dailyWage."', timeWage='".$timeWage."' WHERE staff_id='".$staff_id."' ";
    $result = mysqli_query($conn, $sql);

    $conn->close();
?>
