<?php
    // 建立MySQL的資料庫連接 
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

    $name =  $_POST['addName'];
    $account =  $_POST['addAccount'];
    $pwd =  $_POST['addpwd'];
    $identify =  $_POST['addIdentify'];
    $ondate =  $_POST['addOndate'];
    $salaryType =  $_POST['addSalaryType'];
    $dailyWage =  $_POST['addDayWage'];
    $timeWage =  $_POST['addTimeWage'];

    // insert into table "staffinfo"
    $sql = "insert into staffinfo(name, account, pwd, identify, ondate, salaryType, dailyWage, timeWage) values ('".$name."','".$account."','".$pwd."','".$identify."','".$ondate."','".$salaryType."','".$dailyWage."','".$timeWage."')";
    $result = mysqli_query($conn, $sql);

?>