<?php
    $host = "localhost";
    $dbuser = "root";
    $dbpassword = "1218";
    $dbname = "hrapp";
    $conn = new mysqli($host,$dbuser,$dbpassword,$dbname);

    if ($conn->connect_error) 
    {
        die("Connection failed: " . $conn->connect_error);
    }

    $sql = "select staff_id from staffinfo";
    $result = mysqli_query($connect, $sql); 

    if (mysqli_num_rows($result) > 0) 
    {
        while ($row = mysqli_fetch_array($result)) 
        {
            $account = $row['account'];
            $sql = "select * from staffinfo where account = $account";
            $result = mysqli_query($connect, $sql);
        }
    }
?>